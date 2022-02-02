using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoiseController : MonoBehaviour
{
    PlayerInputActions inputActions;
    Rigidbody rb;
    Animator animator;
    public Camera cam;

    [Header("Input")]
    Vector2 movementInput;
    bool isHoldingJump;
    bool isHoldingDash;

    [Header("Movement")]
    public bool canMove = true;
    public float movementSpeed;
    public float jumpHeight;
    public bool isGrounded;
    public bool canJetpack;

    [Header("Jetpack")]
    public bool usingJetpack;
    public float fuel = 100;
    public float jetpackForce;
    public float dashForce;
    public bool canDash = true;
    public GameObject[] jetSystem;
    
    [Header("Physics")]
    public float gravity = 1;
    public float fallMultiplier = 5;

    [Header("GameObjects")]
    public GameObject jetPack;
    public GameObject cape;

    Canvas canvas;
    TextMeshPro fuelText;
    
    private void Awake() {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        HandleInput();
        HandleJump();
        DoDash();
    }

    private void FixedUpdate() {
        Movement();
        GroundCheck();
        Jetpack();
    }

    void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    void HandleInput(){
        movementInput = inputActions.Noise.Move.ReadValue<Vector2>();
        movementInput.Normalize();
        isHoldingJump = inputActions.Noise.Jump.IsPressed();
        isHoldingDash = inputActions.Noise.Dash.IsPressed();
    }

    void Movement()
    {
        Vector3 camF = cam.transform.forward;
        Vector3 camR = cam.transform.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //Move noise in the direction of the camera
        Vector3 movementDir = camR *  movementInput.x * movementSpeed + camF * movementInput.y * movementSpeed;
        movementDir.y = rb.velocity.y;

        if(canMove)rb.velocity = movementDir;


        //Rotate Noise to movement
        if(new Vector3(movementInput.x, 0 , movementInput.y) != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(movementDir.x, 0, movementDir.z)), Time.deltaTime * 20);
        }
    }

    void HandleJump()
    {
        if(isHoldingJump && isGrounded)
        {
            Jump();
            Debug.Log("Jump");
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    void Jetpack()
    {
        if(!isGrounded && isHoldingJump && fuel > 0)
        {
            usingJetpack = true;
            fuel -= Time.fixedDeltaTime * 50;
            rb.AddForce(Vector3.up * jetpackForce);
            foreach (GameObject system in jetSystem)
            {
                system.SetActive(true);
            }
            jetPack.SetActive(true);
            cape.SetActive(false);
        }else
        {
            usingJetpack = false;
            jetPack.SetActive(false);
            cape.SetActive(true);
            foreach (GameObject system in jetSystem)
            {
                system.SetActive(false);
            }
        }

        if(isGrounded)
        {
            fuel = 100;
            canDash = true;
        } 
    }

    void DoDash()
    {
        if(canDash == true && isHoldingDash)StartCoroutine("Dash");
    }

    IEnumerator Dash()
    {
        Animator jetPackAnimator = jetPack.GetComponent<Animator>();
        canDash = false;
        canMove = false;
        fuel -= 30;
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        jetPackAnimator.SetBool("Dashing?", true);
        jetPack.SetActive(true);
        cape.SetActive(false);
        yield return new WaitForSeconds(.25f);
        jetPackAnimator.SetBool("Dashing?", false);
        jetPack.SetActive(false);
        cape.SetActive(true);
        rb.velocity = Vector3.zero;
        canMove = true;
    }

    void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.1f);
    }
    


    void UpdateUI()
    {
        
    }
}

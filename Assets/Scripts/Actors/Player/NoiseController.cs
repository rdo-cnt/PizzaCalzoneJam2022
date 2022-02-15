using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoiseController : CharacterBase
{
    PlayerInputActions inputActions;
    Rigidbody rb;
    Animator animator;
    public Camera cam;

    [Header("Input")]
    Vector2 movementInput;
    bool isHoldingJump;
    bool isHoldingDash;
    bool isJumpLockingJetpack;

    [Header("Movement")]
    public bool canMove = true;
    public float movementSpeed;
    public float jumpHeight;
    public bool isGrounded;
    public bool canJetpack;
    float jumpReleaseVelocityThreshold = 0.5f; 

    [Header("Jetpack")]
    public bool usingJetpack;
    public float initialFuelCharges = 1;
     [HideInInspector] public float fuel = 1;
    public float jetpackCooldown = 0.5f; //in seconds
    public float jetpackForce;
    public float dashForce;
    public bool canDash = true;
    public GameObject[] jetSystem;
    public bool isHoldingJetpack;
    
    [Header("Physics")]
    public float gravity = 1;
    public float fallMultiplier = 5;

    [Header("GameObjects")]
    public GameObject jetPack;
    public GameObject cape;

    [Header("Combat")]
    public float knockbackStrength;

    Canvas canvas;
    TextMeshPro fuelText;
    
    private void Awake() {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        initializeVariables();
    }

    private void initializeVariables(){
        fuel = initialFuelCharges;
    }

    private void Update() {
        HandleInput();
        DoDash();
    }

    private void FixedUpdate() {
        Movement();
        GroundCheck();
        HandleJump();
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
        isHoldingJetpack = inputActions.Noise.Jetpack.IsPressed();
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
            isJumpLockingJetpack = true;
            Debug.Log("Jump");

        }

        //Enable shorthops and allow noise to jetpack
        if(!isHoldingJump && !isGrounded && !usingJetpack)
        {
            Debug.Log("released post jump");
            if (isJumpLockingJetpack)
            {
                isJumpLockingJetpack = false;
                if (rb.velocity.y > jumpReleaseVelocityThreshold)
                {
                    Debug.Log("erm");
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/5, rb.velocity.z);
                    rb.AddForce(Vector3.up * jumpReleaseVelocityThreshold, ForceMode.Impulse);
                }
            }
            
        }

    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    void Jetpack()
    {
        if(!isGrounded && !isJumpLockingJetpack && isHoldingJump && fuel > 0)
        {
            showJetpack();
            fuel -= 1;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jetpackForce, ForceMode.Impulse);
            foreach (GameObject system in jetSystem)
            {
                system.SetActive(true);
            }
            
        }else
        {
            
            foreach (GameObject system in jetSystem)
            {
                system.SetActive(false);
            }
        }

        if(isGrounded)
        {
            fuel = initialFuelCharges;
            canDash = true;
            hideJetpack();
        } 
    }

    void showJetpack()
    {
        usingJetpack = true;
        jetPack.SetActive(true);
        cape.SetActive(false);
    }

    void hideJetpack()
    {
        if(usingJetpack)
        {
            usingJetpack = false;
            jetPack.SetActive(false);
            cape.SetActive(true);
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
        yield return new WaitForSeconds(jetpackCooldown);
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

    public IEnumerator Knockback(Vector3 direction)
    {
        rb.AddForce(direction * knockbackStrength, ForceMode.Impulse);
        canMove = false;
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    public IEnumerator Invulnerability()
    {
        //Make player invulnerable
        
        yield return new WaitForSeconds(3);

        //Make player vulnerable

    }

    protected void Hurt()
    {
        Debug.Log("Ouchie by noise!!");
        
    }
    


    void UpdateUI()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboPeppino : MonoBehaviour
{
    Animator animator;
    public GameObject hand;
    public GameObject Noise;
    bool putNoiseOnHand;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Noise = GameObject.Find("Noise");
    }

    // Update is called once per frame
    void Update()
    {
        if(putNoiseOnHand)
        {
            Noise.transform.position = hand.transform.position + Vector3.down;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "Noise")
        {
            animator.SetBool("Attack", true);
            Noise.GetComponent<NoiseController>().canMove = false;
            Noise.GetComponent<Rigidbody>().isKinematic = true;
            putNoiseOnHand = true;
        }
    }

    public void ResetAttack()
    {
        animator.SetBool("Attack", false);
        Noise.gameObject.GetComponent<NoiseController>().canMove = true;
        Noise.GetComponent<Rigidbody>().isKinematic = false;
        putNoiseOnHand = false;
        Noise.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
        Noise.GetComponent<Rigidbody>().AddForce(transform.forward * 7, ForceMode.Impulse);
    }
}

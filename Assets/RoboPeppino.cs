using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboPeppino : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "Noise")
        {
            animator.SetBool("Attack", true);
        }
    }

    public void ResetAttack()
    {
        animator.SetBool("Attack", false);
    }
}

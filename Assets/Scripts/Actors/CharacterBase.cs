using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{

    internal Vector3 gravitationalPull = new Vector3(0, -1.0F, 0);
    internal CharacterHealthManager characterHealth;

    
    protected void Hurt()
    {
        Debug.Log("Ouchie!!");
    }

    protected void Death()
    {
        Debug.Log("Just died...");
    }

    // Start is called before the first frame update
    protected void Start()
    {
        gravitationalPull =  Physics.gravity;
        characterHealth = gameObject.GetComponent<CharacterHealthManager>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}

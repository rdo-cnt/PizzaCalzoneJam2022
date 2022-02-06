using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Destroy destroyScript;
    public GameObject shadow;
    public LayerMask Enviorenment;
    public GameObject fireballHitParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        destroyScript = GetComponent<Destroy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        destroyScript.DestroyObject(gameObject, 1.4f);
        Instantiate(fireballHitParticleSystem, transform);
    }

}

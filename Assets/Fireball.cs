using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Destroy destroyScript;
    public LayerMask Enviorenment;
    public GameObject fireballHitParticleSystem;
    public RaycastHit hit;
    SphereCollider col;

    void Start()
    {
        destroyScript = this.GetComponent<Destroy>();
        col = this.GetComponent<SphereCollider>();
        Physics.Raycast(transform.position + Vector3.down, Vector3.down, out hit, 10000);
    }

    void OnTriggerEnter(Collider other)
    {
        col.enabled = false;
        Instantiate(fireballHitParticleSystem, this.hit.point, Quaternion.identity);
        destroyScript.DestroyObject(gameObject, destroyScript.time);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position + Vector3.down, hit.point);
    }

}

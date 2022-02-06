using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTosser : MonoBehaviour
{
    public GameObject noise;
    public GameObject rock;
    public Transform rockSpawnPoint;
    public float throwStrength;
    public float attackRange;
    public bool playerInAttackRange;
    public bool canAttack = true;
    public LayerMask whatIsPlayer;
    public AudioClip[] voicelines;
    private AudioSource source;

    private void Start() {
        noise = GameObject.Find("Noise");
        source = GetComponent<AudioSource>();
    }

    

    private void Update() {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerInAttackRange)
        {
            transform.LookAt(noise.transform.position);
            if(canAttack)StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        Instantiate(rock, rockSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(transform.forward * throwStrength, ForceMode.Impulse);
        source.PlayOneShot(voicelines[Random.Range(0, 2)]);
        yield return new WaitForSeconds(3);
        canAttack = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

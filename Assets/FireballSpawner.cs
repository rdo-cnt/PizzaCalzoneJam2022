using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    [SerializeField]private GameObject fireball;
    public float spawnRate;
    public GameObject noise;
    public float range;
    public float height;
    public bool canSpawnFireball;
    public float fireballSpeed;
    public float xPos;
    public float yPos;
    public float timeBetweenEachFireball;
    public GameObject fireballHitParticleSystem;
    // Start is called before the first frame update

    void Start()
    {
        noise = GameObject.Find("Noise");
        StartCoroutine(SpawnFireballs());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnFireball()
    {
        Debug.Log("Spawned Fireball");
        GameObject newFireball = Instantiate(fireball, new Vector3(Random.insideUnitSphere.x * range + noise.transform.position.x, height, Random.insideUnitSphere.z * range + noise.transform.position.z), Quaternion.identity);
        newFireball.GetComponent<Rigidbody>().velocity = new Vector3(0, -fireballSpeed, 0);
    }

    IEnumerator SpawnFireballs()
    {
        while (canSpawnFireball)
        {
            canSpawnFireball = false;
            SpawnFireball();
            yield return new WaitForSeconds(timeBetweenEachFireball);
            canSpawnFireball = true; 
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    [SerializeField]private GameObject fireball;
    public GameObject noise;
    [Header("Fireball")]
    public bool spawnFireball = true;
    public float timeBetweenEachFireball;
    public float fireballSpeed;
    public float height;
    public float range;
    public bool canSpawnFireball = true;

    void OnEnable()
    {
        noise = GameObject.Find("Noise");
        StartCoroutine(SpawnFireballs());
    }

    void OnDisable()
    {

    }

    void Update()
    {

    }

    void SpawnFireball()
    {
        Debug.Log("Spawned Fireball");
        GameObject newFireball = Instantiate(fireball, new Vector3(Random.insideUnitSphere.x * range + noise.transform.position.x, noise.transform.position.y + height, Random.insideUnitSphere.z * range + noise.transform.position.z), Quaternion.identity);
        newFireball.GetComponent<Rigidbody>().velocity = new Vector3(0, -fireballSpeed, 0);
    }

    IEnumerator SpawnFireballs()
    {
        while (canSpawnFireball)
        {
            if(spawnFireball == true)
            {
                canSpawnFireball = false;
                SpawnFireball();
                yield return new WaitForSeconds(timeBetweenEachFireball);
                canSpawnFireball = true; 
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }

    }
}

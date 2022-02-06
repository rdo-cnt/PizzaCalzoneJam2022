using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject destroyParticle;
    public int hitCount;
    public int bouncesBeforeBreaking;
    AudioSource source;
    public AudioClip[] sounds;
    public GameObject breakSound;
    Rigidbody rb;

    private void OnEnable() {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
            hitCount++;
            source.PlayOneShot(sounds[Random.Range(0,1)]);
            if(hitCount > bouncesBeforeBreaking)
            {
                Break();
            }
        if(other.gameObject.name == "Noise")
        {
            Vector3 moveDirection = rb.transform.position - other.transform.position;
            other.gameObject.GetComponent<NoiseController>().StartCoroutine("Knockback", moveDirection);
            Break();
        }
    }

    void Break()
    {
        Instantiate(breakSound, transform.position, transform.rotation);
        GameObject.Destroy(gameObject);
        Instantiate(destroyParticle, transform.position, transform.rotation).GetComponentInChildren<Rigidbody>().velocity = rb.velocity * 3;
    }

    
}

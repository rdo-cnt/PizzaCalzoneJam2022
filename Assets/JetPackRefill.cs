using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackRefill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Noise")
        {
            other.GetComponent<NoiseController>().fuel += 100;
            Destroy(gameObject);
        }
    }
}

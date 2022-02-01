using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public GameObject Noise;
    public CameraFollow cameraFollow;
    public Camera cam;
    public bool followTarget;
    public bool lookAtTarget;
    public Vector3 cameraOffset;
    public Transform cameraPos;

    private void Awake() {
        Noise = GameObject.Find("Noise");
        cameraFollow = GameObject.FindObjectOfType<CameraFollow>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == Noise)
        {
            cameraFollow.OnNoiseEnter(lookAtTarget, followTarget, cameraOffset, cameraPos);
        }
    }
}

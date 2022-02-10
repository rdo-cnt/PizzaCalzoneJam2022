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
    public float gizmoAlpha;
    private Collider col;

    private void Awake()
    {
        Noise = GameObject.Find("Noise");
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Noise)
        {
            cameraFollow.OnNoiseEnter(lookAtTarget, followTarget, cameraOffset, cameraPos);
        }
    }

    private void OnDrawGizmos()
    {
        col = GetComponent<Collider>();
        Gizmos.color = new Color32(0, 225, 0, ((byte)gizmoAlpha));
        Gizmos.DrawCube(col.bounds.center, col.bounds.size);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public GameObject Noise;
    public CameraTrigger cameraTrigger;
    public bool lookAtTarget;
    public bool followTarget;
    [Range(0, 1)]
    public float smoothSpeed;
    public Vector3 offset;
    public Transform cameraPos;

    private Vector3 desiredPos;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAt();
    }

    void LookAt()
    {
        desiredPos = target.position + offset;

        if(lookAtTarget)transform.LookAt(target);
        if(followTarget)
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed);
            if(!lookAtTarget)transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.zero),smoothSpeed);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, cameraPos.position, ref velocity, smoothSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraPos.rotation, smoothSpeed);
        }
        
    }

    public void OnNoiseEnter(bool doLookAtTarget, bool DoFollowTarget, Vector3 cameraOffset, Transform cameraPosition)
    {
        lookAtTarget = doLookAtTarget;
        followTarget = DoFollowTarget;
        offset = cameraOffset;
        cameraPos = cameraPosition;
        Debug.Log("Entered a Camera Trigger");
    }
}

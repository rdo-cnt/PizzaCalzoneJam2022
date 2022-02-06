using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCandyCannon : MonoBehaviour
{
    public GameObject noise;
    public float speed;
    public GameObject laserEnd;
    public LineRenderer laser;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        noise = GameObject.Find("Noise");
    }

    // Update is called once per frame
    void Update()
    {
        var targetRotation = Quaternion.LookRotation(noise.transform.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

        if(Physics.Raycast(transform.position, transform.forward , out RaycastHit hit, 90000))
        {
            float desiredDistance = Vector3.Distance(transform.position, hit.point);
            laserEnd.transform.position = hit.point;
            laser.SetPosition(1, new Vector3(0, 0, desiredDistance));
        }
    }
}

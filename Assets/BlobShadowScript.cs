using UnityEngine;
using System.Collections;
 
public class BlobShadowScript : MonoBehaviour {
 
    [Header("Settings")]
    public GameObject parent;

    public Vector3 _parentOffset = new Vector3(0f, 0.01f, 0f);
    public LayerMask _layerMask;
    RaycastHit hit;

    void Update()
    {
        Physics.Raycast(parent.transform.position, Vector3.down, out hit, 10000, _layerMask);

        transform.position = hit.point + _parentOffset;
    }
 
}
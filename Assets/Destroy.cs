using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float time;
    public bool destroyOnStart = true;

    private void OnEnable()
    {
        if(destroyOnStart) DestroyObject(gameObject, time);
    }
    public void DestroyObject(GameObject objToDestroy, float timeToDestroy)
    {
        GameObject.Destroy(objToDestroy, timeToDestroy);
    }
}

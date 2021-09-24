using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRunCamera : MonoBehaviour
{
    public float v = 15;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * v; 
    }
}

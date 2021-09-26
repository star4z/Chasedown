using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Speedometer : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 velocity;


    void Awake()
    {
        pos = transform.position;
    }

    void Update()
    {
        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;
    }
}
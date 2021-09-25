using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Speedometer: MonoBehaviour
private float pos;


{
    void Awake()
    { pos = transform.position;
    }

    void Update()
    {
        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;
    }
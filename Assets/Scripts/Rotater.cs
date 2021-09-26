using System;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public int velocity = 5;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * velocity);
    }
}
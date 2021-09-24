using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRunCamera : SwipeDetector
{
    public float v = 15;
    public float laneSize;
    public int currentLane = 0;
    public int minLane = -1;
    public int maxLane = 1;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.forward * v;
    }

    public override void OnSwipeLeft()
    {
        Debug.Log("swiping left");
        if (currentLane > minLane)
        {
            _rigidbody.position += Vector3.left * laneSize;
            currentLane -= 1;
        }
    }

    public override void OnSwipeRight()
    {
        Debug.Log("swiping right");
        if (currentLane < maxLane)
        {
            _rigidbody.position += Vector3.right * laneSize;
            currentLane += 1;
        }
    }
}
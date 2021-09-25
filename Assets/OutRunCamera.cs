using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRunCamera : SwipeDetector
{
    public float laneSize;
    public int currentLane = 0;
    public int minLane = -1;
    public int maxLane = 1;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnSwipeLeft()
    {
        // Debug.Log("swiping left");
        if (currentLane > minLane)
        {
            transform.localPosition += Vector3.left * laneSize;
            currentLane -= 1;
        }
    }

    public override void OnSwipeRight()
    {
        // Debug.Log("swiping right");
        if (currentLane < maxLane)
        {
            transform.localPosition += Vector3.right * laneSize;
            currentLane += 1;
        }
    }
}
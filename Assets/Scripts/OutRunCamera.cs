using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class OutRunCamera : SwipeDetector
{
    public float laneSize;
    public int currentLane = 0;
    public int minLane = -1;
    public int maxLane = 1;
    public PlayerState playerState = PlayerState.DRIVING;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Jump.JumpReached += JumpOnJumpReached;
    }

    private void JumpOnJumpReached(object sender, EventArgs e)
    {
        playerState = PlayerState.JUMPING;
        
        //TODO: play animation
        
        //TODO: on animation end, playerstate = driving
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
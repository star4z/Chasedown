using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OutRunCamera : SwipeDetector
{
    public float laneSize;
    public int currentLane = 0;
    public int minLane = -1;
    public int maxLane = 1;
    public PlayerState playerState = PlayerState.Driving;
    public double jumpDurationInMillis;

    private Rigidbody _rigidbody;
    private DateTime _jumpTime;
    private TimeSpan JumpDuration => TimeSpan.FromMilliseconds(jumpDurationInMillis);

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Jump.JumpReached += JumpOnJumpReached;
        Jump.JumpFinished += JumpOnJumpFinished;
        var d =(DateTime.Now - DateTime.Now);
    }

    private void JumpOnJumpFinished(object sender, EventArgs e)
    {
        StopJump();
    }

    public override void Update()
    {
        base.Update();
        // if (playerState == PlayerState.Jumping && (DateTime.Now - _jumpTime) >= JumpDuration)
        // {
        //     StopJump();
        // }
    }

    private void StopJump()
    {
        Debug.Log("Stopping jump");
        playerState = PlayerState.Driving;
    }

    private void JumpOnJumpReached(object sender, EventArgs e)
    {
        if (playerState != PlayerState.Jumping)
        {
            Debug.Log("Starting jump");
            playerState = PlayerState.Jumping;
            _jumpTime = DateTime.Now;
        }
        //TODO: play animation

        //TODO: on animation end, playerstate = driving
    }

    public override void OnSwipeLeft()
    {
        // Debug.Log("swiping left");
        if (playerState == PlayerState.Driving && currentLane > minLane)
        {
            transform.localPosition += Vector3.left * laneSize;
            currentLane -= 1;
        }
    }

    public override void OnSwipeRight()
    {
        // Debug.Log("swiping right");
        if (playerState == PlayerState.Driving && currentLane < maxLane)
        {
            transform.localPosition += Vector3.right * laneSize;
            currentLane += 1;
        }
    }
}
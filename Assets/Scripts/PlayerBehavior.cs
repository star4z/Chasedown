using System;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public PathCreator[] pathCreators;
    public float velocity = 25;
    public SwipeDetector swipeDetector;
    public int currentPath = 1;
    public PlayerState playerState = PlayerState.Driving;
    public VelocityModifier[] velocityModifiers;
    public int minJumpDurationMillis;
    public int maxFramesBetweenJumpTriggers = 2;

    private float _distanceTraveled;
    private Dictionary<PlayerState, float> _velocityModifiers;
    // private DateTime _jumpTime;
    private int _framesSinceLastOnJump = 0;
    

    private void Start()
    {
        swipeDetector.SwipeLeft += SwipeDetectorOnSwipeLeft;
        swipeDetector.SwipeRight += SwipeDetectorOnSwipeRight;
        // Jump.JumpStarted += JumpOnJumpStarted;
        // Jump.JumpFinished += JumpOnJumpFinished;
        Jump.Jumping += JumpOnJumping;
        _velocityModifiers = velocityModifiers.ToDictionary(vm => vm.playerState, vm => vm.velocityModifier);
    }

    private void JumpOnJumping(object sender, Collider e)
    {
        if (playerState != PlayerState.Jumping)
        {
            Debug.Log("jumping");
        }

        playerState = PlayerState.Jumping;
        _framesSinceLastOnJump = 0;
    }

    // private void JumpOnJumpFinished(object sender, Collider collider1)
    // {
    //     if (playerState != PlayerState.Jumping || DateTime.Now - _jumpTime < TimeSpan.FromMilliseconds(minJumpDurationMillis)) return;
    //     Debug.Log("jumped");
    //     playerState = PlayerState.Driving;
    // }
    //
    // private void JumpOnJumpStarted(object sender, Collider collider1)
    // {
    //     if (playerState == PlayerState.Jumping) return;
    //     Debug.Log("jumping!");
    //     playerState = PlayerState.Jumping;
    //     _jumpTime = DateTime.Now;
    // }

    private void SwipeDetectorOnSwipeLeft(object sender, EventArgs e)
    {
        if (playerState == PlayerState.Driving && currentPath > 0)
        {
            currentPath--;
        }
    }

    private void SwipeDetectorOnSwipeRight(object sender, EventArgs e)
    {
        if (playerState == PlayerState.Driving && currentPath < pathCreators.Length - 1)
        {
            currentPath++;
        }
    }

    private void Update()
    {
        _distanceTraveled += GetStep();
        var pathPosition = pathCreators[currentPath].path
            .GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
        transform.position = new Vector3(pathPosition.x, transform.position.y, pathPosition.z);
        // transform.position = pathPosition;
        var pathRotation = pathCreators[currentPath].path
            .GetRotationAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathRotation;

        if (playerState == PlayerState.Jumping)
        {
            // Debug.Log(framesSinceLastOnJump);
            if (_framesSinceLastOnJump > maxFramesBetweenJumpTriggers)
            {
                playerState = PlayerState.Driving;
            }
            else
            {
                _framesSinceLastOnJump++;
            }
        }
    }

    private float GetStep()
    {
        if (_velocityModifiers.TryGetValue(playerState, out var modifier))
        {
            return modifier * velocity * Time.deltaTime;
        }

        return velocity * Time.deltaTime;
    }
}
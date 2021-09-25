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
    public int maxFramesBetweenJumpTriggers = 2;
    public float modifierIncrPerSec = 1.1f;
    public int boostDurationMillis = 1500;

    private float _distanceTraveled;
    private Dictionary<PlayerState, float> _velocityModifiers;
    private int _framesSinceLastOnJump;
    private float _timeModifier = 1;
    private DateTime _boostStarted;

    private void Start()
    {
        swipeDetector.SwipeLeft += SwipeDetectorOnSwipeLeft;
        swipeDetector.SwipeRight += SwipeDetectorOnSwipeRight;
        Jump.Jumping += JumpOnJumping;
        Boost.BoostStarted += BoostOnBoostStarted;
        _velocityModifiers = velocityModifiers.ToDictionary(vm => vm.playerState, vm => vm.velocityModifier);
    }

    private void BoostOnBoostStarted(object sender, Collider e)
    {
        playerState = PlayerState.Boosting;
        _boostStarted = DateTime.Now;
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
        CheckIfStopped();
        UpdatePathPositionAndRotation();

        CheckIfJumping();
        CheckIfBoosting();
    }

    private void CheckIfBoosting()
    {
        if (playerState == PlayerState.Boosting &&
            DateTime.Now - _boostStarted >= TimeSpan.FromMilliseconds(boostDurationMillis))
        {
            playerState = PlayerState.Driving;
        }
    }

    private void CheckIfJumping()
    {
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

    private void CheckIfStopped()
    {
        if (_distanceTraveled >= pathCreators[currentPath].path.length)
        {
            playerState = PlayerState.Stopped;
        }
    }

    private void UpdatePathPositionAndRotation()
    {
        var pathPosition = pathCreators[currentPath].path
            .GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
        var transform1 = transform;
        transform1.position = new Vector3(pathPosition.x, transform1.position.y, pathPosition.z);
        var pathRotation = pathCreators[currentPath].path
            .GetRotationAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathRotation;
    }

    private float GetStep()
    {
        _timeModifier += Time.deltaTime * modifierIncrPerSec;
        var actualVelocity = velocity * _timeModifier * Time.deltaTime;
        if (_velocityModifiers.TryGetValue(playerState, out var stateModifier))
        {
            actualVelocity *= stateModifier;
        }
        return actualVelocity;
    }
}
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
    public int passThroughCrashDuration = 1500;
    public int stopCrashDuration = 1500;

    private float _distanceTraveled;
    private int _framesSinceLastOnJump;
    private float _timeModifier = 1;
    private DateTime _boostStarted;

    private Dictionary<PlayerState, DateTime> _effectStartTimes = Enum.GetValues(typeof(PlayerState))
        .Cast<PlayerState>()
        .ToDictionary(s => s, s => (DateTime)default);

    private void Start()
    {
        swipeDetector.SwipeLeft += SwipeDetectorOnSwipeLeft;
        swipeDetector.SwipeRight += SwipeDetectorOnSwipeRight;
        Jump.Jumping += JumpOnJumping;
        Boost.BoostStarted += BoostOnBoostStarted;
        Obstacle.OnEnter += ObstacleOnOnEnter;
    }

    private void ObstacleOnOnEnter(object sender, Collider e)
    {
        if (sender is Obstacle { stopsThePlayer: true })
        {
            playerState = PlayerState.Crashed;
            _effectStartTimes[PlayerState.Crashed] = DateTime.Now;
        }
        else
        {
            playerState = PlayerState.Crashing;
            _effectStartTimes[PlayerState.Crashing] = DateTime.Now;
        }

        _timeModifier = 1;
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
        if (currentPath > 0)
        {
            if (playerState == PlayerState.Driving)
            {
                currentPath--;
            }
            else if (playerState == PlayerState.Crashed && DateTime.Now - _effectStartTimes[PlayerState.Crashed] >=
                TimeSpan.FromMilliseconds(stopCrashDuration))
            {
                playerState = PlayerState.Driving;
                currentPath--;
            }
        }
    }

    private void SwipeDetectorOnSwipeRight(object sender, EventArgs e)
    {
        if (currentPath < pathCreators.Length - 1)
        {
            if (playerState == PlayerState.Driving)
            {
                currentPath++;
            }
            else if (playerState == PlayerState.Crashed && DateTime.Now - _effectStartTimes[PlayerState.Crashed] >=
                TimeSpan.FromMilliseconds(stopCrashDuration))
            {
                playerState = PlayerState.Driving;
                currentPath++;
            }
        }
    }

    private void Update()
    {
        _distanceTraveled += GetStep();
        CheckIfStopped();
        UpdatePathPositionAndRotation();

        CheckIfJumping();
        CheckIfBoosting();
        CheckIfCrashing();
    }

    private void CheckIfCrashing()
    {
        if (playerState == PlayerState.Crashing &&
            DateTime.Now - _effectStartTimes[PlayerState.Crashing] >=
            TimeSpan.FromMilliseconds(passThroughCrashDuration))
        {
            playerState = PlayerState.Driving;
        }
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
        if (velocityModifiers.Any(vm => vm.playerState == playerState))
        {
            var stateModifier = velocityModifiers.First(vm => vm.playerState == playerState).velocityModifier;
            actualVelocity *= stateModifier;
        }

        return actualVelocity;
    }
}
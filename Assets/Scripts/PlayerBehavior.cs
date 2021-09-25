using System;
using PathCreation;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public PathCreator[] pathCreators;
    public float velocity = 25;
    public SwipeDetector swipeDetector;
    public int currentPath = 1;
    public PlayerState playerState = PlayerState.Driving;
    
    private float _distanceTraveled;

    private void Start()
    {
        swipeDetector.SwipeLeft += SwipeDetectorOnSwipeLeft;
        swipeDetector.SwipeRight += SwipeDetectorOnSwipeRight;
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
        if (playerState == PlayerState.Driving && currentPath < pathCreators.Length)
        {
            currentPath++;
        }
    }

    private void Update()
    {
        _distanceTraveled += velocity * Time.deltaTime;
        var pathPosition = pathCreators[currentPath].path.GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
        transform.position = new Vector3(pathPosition.x, transform.position.y, pathPosition.z);
        // transform.position = pathPosition;
        var pathRotation = pathCreators[currentPath].path.GetRotationAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathRotation;
    }
}
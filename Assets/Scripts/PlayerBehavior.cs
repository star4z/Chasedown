using System;
using PathCreation;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public PathCreator pathCreator;
    public float velocity = 25;
    private float _distanceTraveled;

    private void Update()
    {
        _distanceTraveled += velocity * Time.deltaTime;
        var pathPosition = pathCreator.path.GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
        transform.position = new Vector3(pathPosition.x, transform.position.y, pathPosition.z);
        // transform.position = pathPosition;
        var pathRotation = pathCreator.path.GetRotationAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
        transform.rotation = pathRotation;
    }
}
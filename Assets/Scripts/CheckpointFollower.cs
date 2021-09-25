using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFollower : MonoBehaviour
{
    public float v = 15;
    public List<Checkpoint> checkPoints;
    public int currentCheckpoint = 0;
    public float rotationSpeed = 1;
    public OutRunCamera ps;
    public Vector3 jumpForce = Vector3.forward * 15;


    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        // _rigidbody.velocity = Vector3.forward * v;
        foreach (var checkpoint in checkPoints)
        {
            checkpoint.CheckpointReached += OnCheckPointReached;
        }

        // LookAtCheckpoint();
    }

    private void Update()
    {
        switch (ps.playerState)
        {
            case PlayerState.Driving:
                if (currentCheckpoint < checkPoints.Count)
                {
                    LookAtNextCheckpoint();
                    MoveTowardsNextCheckpoint();
                }
                break;
            case PlayerState.Jumping:
                _rigidbody.AddForce(jumpForce);
                break;
            case PlayerState.Crashing:
                break;
            case PlayerState.Stopped:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void MoveTowardsNextCheckpoint()
    {
        var step = v * Time.deltaTime;
        var targetPos = checkPoints[currentCheckpoint].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    private void LookAtNextCheckpoint()
    {
        var targetPos = checkPoints[currentCheckpoint].transform.position;
        // targetPos.x = targetPos.z = uint.MinValue;
        // var lookRotation = Quaternion.LookRotation(targetPos);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
        var direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            //create the rotation we need to be in to look at the target
            var lookRotation = Quaternion.LookRotation(direction);
            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnCheckPointReached(object sender, EventArgs e)
    {
        if (ps.playerState == PlayerState.Driving)
        {
            Debug.Log($"reached checkpoint {currentCheckpoint}");
            currentCheckpoint++;
            if (currentCheckpoint >= checkPoints.Count)
            {
                _rigidbody.velocity = Vector3.zero;
                ps.playerState = PlayerState.Stopped;
            }
            // else
            // {
            //     LookAtCheckpoint();
            // }
        }
    }
}
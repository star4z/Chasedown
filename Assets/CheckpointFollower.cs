using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFollower : MonoBehaviour
{
    public float v = 15;
    public List<Checkpoint> checkPoints;
    public int currentCheckpoint = 0;
    public float RotationSpeed = 1;

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
        if (currentCheckpoint < checkPoints.Count)
        {
            LookAtNextCheckpoint();
            MoveTowardsNextCheckpoint();
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
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, damping * Time.deltaTime);
        var _direction = (targetPos - transform.position).normalized;
 
        //create the rotation we need to be in to look at the target
        var _lookRotation = Quaternion.LookRotation(_direction);
 
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

    private void OnCheckPointReached(object sender, EventArgs e)
    {
        Debug.Log($"reached checkpoint {currentCheckpoint}");
        currentCheckpoint++;
        if (currentCheckpoint >= checkPoints.Count)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        // else
        // {
        //     LookAtCheckpoint();
        // }
    }
}
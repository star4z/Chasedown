using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFollower : MonoBehaviour
{
    public float v = 15;
    public List<Checkpoint> checkPoints;
    public int currentCheckpoint = 0;
    public float damping = 1;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.forward * v;
        foreach (var checkpoint in checkPoints)
        {
            checkpoint.CheckpointReached += OnCheckPointReached;
        }
        LookAtCheckpoint();
    }

    private void LookAtCheckpoint()
    {
        var targetPos = checkPoints[currentCheckpoint].transform.position;
        targetPos.x = targetPos.z = uint.MinValue;
        var lookRotation = Quaternion.LookRotation (targetPos);
        transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, damping * Time.deltaTime);
    }

    private void OnCheckPointReached(object sender, EventArgs e)
    {
        Debug.Log($"reached checkpoint {currentCheckpoint}");
        currentCheckpoint++;
        if (currentCheckpoint >= checkPoints.Count)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            LookAtCheckpoint();
        }
    }
}

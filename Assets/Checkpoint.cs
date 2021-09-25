using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public event EventHandler CheckpointReached;

    private void OnTriggerEnter(Collider other)
    {
        CheckpointReached?.Invoke(this, null);
    }
}

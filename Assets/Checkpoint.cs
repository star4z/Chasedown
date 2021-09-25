using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public event EventHandler CheckpointReached;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(GetComponent<Collider>());
        CheckpointReached?.Invoke(this, null);
    }
}

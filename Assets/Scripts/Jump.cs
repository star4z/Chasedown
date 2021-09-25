using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public static event EventHandler JumpReached;
    public static event EventHandler JumpFinished;

    private void OnTriggerEnter(Collider other)
    {
        JumpReached?.Invoke(this, null);
    }

    private void OnTriggerExit(Collider other)
    {
        JumpFinished?.Invoke(this, null);
    }
}

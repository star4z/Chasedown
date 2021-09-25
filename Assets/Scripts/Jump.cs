using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public static event EventHandler JumpReached;

    private void OnTriggerEnter(Collider other)
    {
        JumpReached?.Invoke(this, null);
    }
}

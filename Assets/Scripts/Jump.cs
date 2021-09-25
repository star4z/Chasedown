using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public static event EventHandler<Collider> JumpStarted;
    public static event EventHandler<Collider> JumpFinished;
    public static event EventHandler<Collider> Jumping;

    private void OnTriggerEnter(Collider other)
    {
        JumpStarted?.Invoke(this, null);
    }

    private void OnTriggerExit(Collider other)
    {
        JumpFinished?.Invoke(this, null);
    }

    private void OnTriggerStay(Collider other)
    {
        Jumping?.Invoke(this, null);
    }
}

using System;
using UnityEngine;

public class Boost: MonoBehaviour
{
    public static event EventHandler<Collider> BoostStarted;
    public static event EventHandler<Collider> BoostFinished;
    public static event EventHandler<Collider> Boosting;

    private void OnTriggerEnter(Collider other)
    {
        BoostStarted?.Invoke(this, null);
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        BoostFinished?.Invoke(this, null);
    }

    private void OnTriggerStay(Collider other)
    {
        Boosting?.Invoke(this, null);
    }
}
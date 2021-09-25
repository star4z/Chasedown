using System;
using UnityEngine;

public class Obstacle: MonoBehaviour
{
    public bool isDestroyedOnCollision;
    public bool stopsThePlayer;
    
    public static event EventHandler<Collider> OnEnter;
    public static event EventHandler<Collider> OnExit;
    public static event EventHandler<Collider> OnStay;

    private void OnTriggerEnter(Collider other)
    {
        OnEnter?.Invoke(this, null);
        if (isDestroyedOnCollision)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit?.Invoke(this, null);
    }

    private void OnTriggerStay(Collider other)
    {
        OnStay?.Invoke(this, null);
    }
}
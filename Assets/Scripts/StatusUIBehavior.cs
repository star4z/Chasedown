using System;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIBehavior: MonoBehaviour
{
    public Text errorIndicator;
    public float duration = 1;

    private float _time;
    private void Start()
    {
        Obstacle.OnEnter += ObstacleOnOnEnter;
    }

    private void ObstacleOnOnEnter(object sender, Collider e)
    {
        errorIndicator.gameObject.SetActive(true);
        _time = Time.time;
    }

    private void Update()
    {
        if (Time.time - _time > duration)
        {
            errorIndicator.gameObject.SetActive(false);
        }
    }
}
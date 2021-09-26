using System;
using UnityEngine;

public class SfxBehavior : MonoBehaviour
{
    public AudioSource laneChange;
    public AudioSource crashing;
    public AudioSource crashed;

    private void Start()
    {
        Obstacle.OnEnter += ObstacleOnOnEnter;
        PlayerBehavior.LaneChange += PlayerBehaviorOnLaneChange;
    }

    private void PlayerBehaviorOnLaneChange(object sender, EventArgs e)
    {
        laneChange.Play();
    }

    private void ObstacleOnOnEnter(object sender, Collider e)
    {
        if (sender is Obstacle { stopsThePlayer: true } o)
        {
            crashed.Play();
        }
        else
        {
            crashing.Play();
        }
    }
}
using System;
using UnityEngine;

public class MusicBehavior: MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {
        PlayerBehavior.StartLevel += PlayerBehaviorOnStartLevel;
    }

    private void PlayerBehaviorOnStartLevel(object sender, EventArgs e)
    {
        audioSource.Play();
    }
}
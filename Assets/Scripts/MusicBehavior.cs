using System;
using UnityEngine;

public class MusicBehavior: MonoBehaviour
{
    public AudioSource audioSource;
    public int bpm;
    public float startDelayInSeconds;
    public int firstBeat = 0;
    public int totalBeats = 8;
    public MusicUI musicUI;

    private bool _musicPlaying;
    private int _lastBeat;
    private float _lastBeatTime;
    private bool _fuckedUpThisBeat = false;
    
    private void Start()
    {
        PlayerBehavior.StartLevel += PlayerBehaviorOnStartLevel;
        PlayerBehavior.FinishLevel += PlayerBehaviorOnFinishLevel;
        Obstacle.OnEnter += ObstacleOnOnEnter;
    }

    private void ObstacleOnOnEnter(object sender, Collider e)
    {
        _fuckedUpThisBeat = true;
    }

    private void PlayerBehaviorOnFinishLevel(object sender, EventArgs e)
    {
        // audioSource.Stop();
        _musicPlaying = false;
    }

    private void PlayerBehaviorOnStartLevel(object sender, EventArgs e)
    {
        musicUI.ResetAllStatuses();
        audioSource.PlayDelayed(startDelayInSeconds);
        _musicPlaying = true;
        _lastBeat = firstBeat;
        if (musicUI != null)
        {
            musicUI.SetCurrentBeat(_lastBeat);
        }
    }

    private void Update()
    {
        if (_musicPlaying && CurrentBeat() != _lastBeat)
        {
            if (!_fuckedUpThisBeat)
            {
                HandleNextBeat();
            }
            else
            {
                RestartThisBeat();
            }
        }
    }

    private void RestartThisBeat()
    {
        _fuckedUpThisBeat = false;
        audioSource.time = _lastBeatTime;
    }

    private void HandleNextBeat()
    {
        _lastBeat = CurrentBeat();
        _lastBeatTime = audioSource.time;
        if (musicUI != null)
        {
            musicUI.SetCurrentBeat(_lastBeat);
        }
    }

    private int CurrentBeat()
    {
        return (int)Math.Floor(audioSource.time / 60 * bpm);
    }
}
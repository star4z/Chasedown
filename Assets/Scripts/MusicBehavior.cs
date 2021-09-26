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
    
    private void Start()
    {
        PlayerBehavior.StartLevel += PlayerBehaviorOnStartLevel;
        PlayerBehavior.FinishLevel += PlayerBehaviorOnFinishLevel;
    }

    private void PlayerBehaviorOnFinishLevel(object sender, EventArgs e)
    {
        audioSource.Stop();
        _musicPlaying = false;
    }

    private void PlayerBehaviorOnStartLevel(object sender, EventArgs e)
    {
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
        if (_musicPlaying)
        {
            if (CurrentBeat() != _lastBeat)
            {
                _lastBeat = CurrentBeat();
                if (musicUI != null)
                {
                    musicUI.SetCurrentBeat(_lastBeat);
                }
            }
        }
    }

    private int CurrentBeat()
    {
        return (int)Math.Floor(audioSource.time / 60 * bpm) % totalBeats;
    }

    private int NextBeat(int currentBeat)
    {
        return currentBeat < totalBeats - 1 ? ++currentBeat : 0;
    }

    private int PrevBeat(int currentBeat)
    {
        return currentBeat > 0 ? --currentBeat : totalBeats - 1;
    }
}
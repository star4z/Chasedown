using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timeLimit = 60;

    private bool _running = false;
    

    void Start()
    {
        PlayerBehavior.StartLevel += PlayerBehaviorOnStartLevel;
        PlayerBehavior.FinishLevel += PlayerBehaviorOnFinishLevel;
    }

    private void PlayerBehaviorOnFinishLevel(object sender, EventArgs e)
    {
        _running = true;
    }

    private void PlayerBehaviorOnStartLevel(object sender, EventArgs e)
    {
        _running = true;
    }

    void Update()
    {
        if (!_running) return;
        timeLimit -= Time.deltaTime;

        var minutes = ((int)timeLimit / 60).ToString();
        var seconds = (timeLimit % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }
}
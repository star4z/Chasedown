using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text timerText;
    public Button finishButton;
    public float timeLimit = 60;

    private bool _running;

    public static event EventHandler TimedOut;

    void Start()
    {
        PlayerBehavior.StartLevel += PlayerBehaviorOnStartLevel;
        PlayerBehavior.FinishLevel += PlayerBehaviorOnFinishLevel;
        finishButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }

    private void PlayerBehaviorOnFinishLevel(object sender, EventArgs e)
    {
        EndLevel();
    }

    private void EndLevel()
    {
        _running = false;
        finishButton.gameObject.SetActive(true);
    }

    private void PlayerBehaviorOnStartLevel(object sender, EventArgs e)
    {
        _running = true;
    }

    void Update()
    {
        if (!_running) return;
        timeLimit -= Time.deltaTime;
        if (timeLimit <= 0)
        {
            timeLimit = 0;
            TimedOut?.Invoke(this, EventArgs.Empty);
            EndLevel();
        }

        var minutes = ((int)timeLimit / 60).ToString();
        var seconds = (timeLimit % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StopWatch : MonoBehaviour
{
    bool _stopwatchActive = false;
    float _currentTime;
    public Text currentTimeText;

    void Start()
    {
        _currentTime = 0;
    }

    void Update()
    {
        if (_stopwatchActive == true)
        {
            _currentTime = _currentTime + Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }
}
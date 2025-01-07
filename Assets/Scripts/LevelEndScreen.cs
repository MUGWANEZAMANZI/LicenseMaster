using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using Unity.VisualScripting;

public class LevelEndScreen : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelTimeText;
    [SerializeField] public TextMeshProUGUI violationsText;
    [SerializeField] public TextMeshProUGUI finalScoreText;

    private Dictionary<Violations, int> violationsCount = new Dictionary<Violations, int>();
    void Start()
    {
        Time.timeScale = 0;
        // Set Level Time Text
        float time = LevelManager.instance.levelTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        levelTimeText.text = string.Format("Level Time - {0:00}:{1:00}", minutes, seconds);

        // Set Violations
        foreach (Violations violation in LevelManager.instance.incuredViolations)
        {
            if (violationsCount.ContainsKey(violation)) {
                violationsCount[violation]++;
            } else {
                violationsCount[violation] = 1;
            }
        }

        foreach (var pair in violationsCount)
        {
           violationsText.text += pair.Key + " Violations - " + pair.Value + "\n";
        }

        // Set Final Score
        finalScoreText.text = "Final Score - " + LevelManager.instance.points;
    }
}
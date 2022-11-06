using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private Text levelCompleted;
    [SerializeField]
    private Text numberOfGoals;
    [SerializeField]
    private Text NumberOfMisses;
    [SerializeField]
    private Text playtime;

    private void OnEnable()
    {
        levelCompleted.text = "LEVELS COMPLETED: " + PlayerPrefs.GetInt("LevelUnlock") + "/51";
        numberOfGoals.text = "NUMBER OF GOALS: " + PlayerPrefs.GetInt("NumberOfGoals");
        NumberOfMisses.text = "NUMBER OF MISSES: " + PlayerPrefs.GetInt("NumberOfMisses");
        TimeSpan t = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("playtime"));
        string playtimeCalc = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                t.Hours,
                t.Minutes,
                t.Seconds);
        playtime.text = "PLAYTIME: " + playtimeCalc;
    }
}

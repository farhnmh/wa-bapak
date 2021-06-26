using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [System.Serializable]
    public class DateDetail
    {
        public int day;
        public int month;
        public int year;
    }

    public DateDetail date;
    public int timeMod, scoreMod;
    public Image pausePanel, missionPanel, alertPanel;
    public TextMeshProUGUI timerText, dateText;
    public bool missionActive;
    public GameObject[] houseArray;

    bool timerActive = true;
    int score, timerInt;
    float curTime;
    bool scoreInc;
    bool isPaused;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Start()
    {
        curTime = 0;
        score = -scoreMod;
        scoreInc = false;

        houseArray = GameObject.FindGameObjectsWithTag("House");
        RandomDate();
    }

    void Update()
    {
        if(timerActive)
        {
            curTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(curTime);
            timerText.text = time.ToString(@"mm\:ss");
            timerInt = Mathf.RoundToInt(curTime);
        }

        if (timerInt % timeMod == 0 && !scoreInc)
        {
            score += scoreMod;
            scoreInc = true;
        }

        if (timerInt % timeMod != 0)
        {
            scoreInc = false;
        }

        dateText.text = $"Today's Date\n{date.day}/{date.month}/{date.year}";
    }

    public void RandomDate()
    {
        date.day = UnityEngine.Random.Range(1, 31);
        date.month = UnityEngine.Random.Range(1, 13);
        date.year = 2021;
    }

    public void RandomHouse()
    {
        int i = UnityEngine.Random.Range(0, 10);
        MissionManager.instance.AssignMission(i);
        missionActive = true;

        Debug.Log($"Rumah Ke-{i}");
    }

    public void PauseOrResumeGame()
    {
        if(!isPaused)
        {
            Time.timeScale = 0.0f;
            pausePanel.gameObject.SetActive(true);
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.gameObject.SetActive(false);
            isPaused = false;
        }
    }

    public void OpenDetailMission()
    {
        if (!isPaused)
        {
            Time.timeScale = 0.0f;

            if (missionActive)
                missionPanel.gameObject.SetActive(true);
            else if (!missionActive)
                alertPanel.gameObject.SetActive(true);

            isPaused = true;
        }
        else if (isPaused)
        {
            Time.timeScale = 1.0f;

            if (missionActive)
                missionPanel.gameObject.SetActive(false);
            else if (!missionActive)
                alertPanel.gameObject.SetActive(false);

            isPaused = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class PlayerTimer : NetworkBehaviour
{
    [Header("Timer Values")]
    [SerializeField] double setTimer = 20;
    [SerializeField] TMP_Text timerText;
    [SerializeField] Image colorBox;

    [Header("Timer Bools")]
    public bool startTimer;

    double timerCount;

    private void Start()
    {
        timerCount = setTimer;
    }

    private void Update()
    {
        if (!startTimer)
            return;
        else
        {   
            if (timerCount > 0)
                timerCount -= Time.deltaTime;

            else
                timerCount = 0;

            Timer(timerCount);
        }
    }


    void Timer(double currTime)
    {
        if(isClient && currTime <= 0)
        {
            currTime = 0;
            startTimer = false;
            colorBox.color = Color.red;
            StartCoroutine(WaitForLight());
        }

        else
        {
            float minutes = Mathf.FloorToInt((float)currTime / 60);
            float seconds = Mathf.FloorToInt((float)currTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            colorBox.color = Color.green;
        }

    }

    IEnumerator WaitForLight()
    {
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        startTimer = true;
        timerCount = setTimer;
    }

}

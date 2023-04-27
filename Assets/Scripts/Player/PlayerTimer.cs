using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class PlayerTimer : NetworkBehaviour
{
    [Header("Timer Values")]
    [SerializeField] TMP_Text displayTimer;
    [SerializeField] Image timerBox;
    public bool startTimer;

    [Header("SyncVar Values")]
    [SyncVar] [SerializeField] double setTimer = 20;

    [SyncVar(hook = nameof(HandleTimerText))]
    [SerializeField] string timerText = "00 : 00";

    [SyncVar(hook = nameof(HandleBoxColor))]
    [SerializeField] Color colorBox;

    double timerCount;

    #region SERVER

    private void Start()
    {
        if (!isOwned)
            return;

        timerCount = setTimer;
    }

    [Server]
    public void SetTimerText(string currTime)
    {
        timerText = currTime;
    }

    [Server]
    public void SetColorTimer(Color currColor)
    {
        colorBox = currColor;
    }

    [Server]
    IEnumerator WaitForLight()
    {
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        startTimer = true;
        timerCount = setTimer;
    }

    [Command]
    void UpdateTimer(double currTime)
    {
        if (currTime <= 0)
        {
            currTime = 0;
            startTimer = false;
            SetColorTimer(Color.red);
            StartCoroutine(WaitForLight());
        }

        else
        {
            float minutes = Mathf.FloorToInt((float)currTime / 60);
            float seconds = Mathf.FloorToInt((float)currTime % 60);

            SetTimerText(string.Format("{0:00}:{1:00}", minutes, seconds));
            SetColorTimer(Color.green);
        }

    }

    #endregion

    #region CLIENTS

    [ClientCallback]
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

            UpdateTimer(timerCount);
        }
    }

    void HandleBoxColor(Color currColor, Color changeColor)
    {
        timerBox.color = changeColor;
    }

    void HandleTimerText(string currTime, string changeTime)
    {
        displayTimer.text = changeTime;
    }

    #endregion

}

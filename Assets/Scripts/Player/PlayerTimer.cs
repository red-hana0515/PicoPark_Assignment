using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class PlayerTimer : NetworkBehaviour
{
    [Header("Timer Values")]
    [SerializeField] PlayerMovementTest playerMovement;
    [SerializeField] TMP_Text displayTimer;
    [SerializeField] Image timerBox;
    [SerializeField] double setTimer = 20;
    [SerializeField] bool startTimer = false;

    public bool mustStop = false;

    [Header("SyncVar Values")]
    public static bool enableTimer;
    //[SyncVar(hook = nameof(HandleTimeSet))] 
    //[SerializeField] double syncTime = 20;

    //[SyncVar(hook = nameof(HandleTimerText))]
    //[SerializeField] string timerText = "???";

    //[SyncVar(hook = nameof(HandleBoxColor))]
    //[SerializeField] Color colorBox;

    double timerCount;

    #region SERVER

    private void Awake()
    {
        timerCount = setTimer;
    }

    public void CmdCountdown(bool change)
    {
        timerBox.gameObject.SetActive(change);
        startTimer = change;
    }

    IEnumerator WaitForLight()
    {
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        startTimer = true;
        timerCount = setTimer;
    }

    void UpdateTimer(double currTime)
    {
        if (currTime <= 0)
        {
            currTime = 0;
            startTimer = false;
            timerBox.color = Color.red;
            StartCoroutine(WaitForLight());

            mustStop = true;
        }

        else
        {
            float minutes = Mathf.FloorToInt((float)currTime / 60);
            float seconds = Mathf.FloorToInt((float)currTime % 60);

            mustStop = false;

            Debug.Log("Current time = " + minutes + " : " + seconds);
            displayTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerBox.color = Color.green;
        }

    }


    #endregion

    #region CLIENTS

    [ClientCallback]
    private void Update()
    {
        if(enableTimer)
        {
            startTimer = true;
        }
        else
        {
            startTimer = false;
            mustStop = false;
        }


        if (!startTimer)
        {
            return;
        }
            
        else
        {
            if (timerCount > 0)
            {
                timerCount -= Time.deltaTime;
            }   

            else
            {
                timerCount = 0;
            }

            UpdateTimer(timerCount);
        }

    }

    //void HandleBoxColor(Color currColor, Color changeColor)
    //{
    //    timerBox.color = changeColor;
    //}

    //void HandleTimerText(string currTime, string changeTime)
    //{
    //    displayTimer.text = changeTime;
    //}

    //void HandleTimeSet(double oldTime, double currTime)
    //{
    //    setTimer = currTime;
    //}

    #endregion

}

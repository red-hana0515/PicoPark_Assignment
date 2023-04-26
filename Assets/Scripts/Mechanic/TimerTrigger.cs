using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimerTrigger : NetworkBehaviour
{
    [Header("Timer Reference")]
    [SerializeField] PlayerTimer playerTimer;

    [Header("Trigger Status")]
    [SerializeField] bool startPoint;
    [SerializeField] bool endPoint;

    private void Start()
    { 
        playerTimer.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(startPoint)
            {
                playerTimer.gameObject.SetActive(true);
                playerTimer.startTimer = true;
            }

            else if(endPoint)
            {
                playerTimer.gameObject.SetActive(false);
                playerTimer.startTimer = false;
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject victoryFont;
    [SerializeField] int playersInGoal;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            victoryFont.SetActive(true);
        }
    }
}

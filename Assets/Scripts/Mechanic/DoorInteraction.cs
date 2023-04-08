using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DoorInteraction : NetworkBehaviour
{
    [Header("Door Properties")]
    [SerializeField] GameObject blockedDoor;
    [SerializeField] string openDoor;
    Animator doorAnim;

    [Header("Buttons Properties")]
    [SerializeField] PressureButtonMechanic[] buttons;

    #region SERVER

    [Server]
    bool CheckAllButtonsPressed()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].isPressed == false)
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region CLIENT

    void Awake()
    {
        doorAnim = blockedDoor.GetComponent<Animator>();
    }
    
    // I'm not sure which property to use
    // Like [Command] or use SyncVar for the animation
    // You can help be my second pair of eyes, Euan ;w;
    private void Update()
    {
        if (CheckAllButtonsPressed())
        {
            doorAnim.Play(openDoor);
        }
    }

    #endregion
}

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

    [SyncVar]
    public bool isOpen = false;

    [Header("Buttons Properties")]
    [SerializeField] PressureButtonMechanic[] buttons;

    #region SERVER

    void CheckAllButtonsPressed()
    {
        if (isOpen == false)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].isPressed == false)
                {
                    isOpen = false;
                    break;
                }

                else if (i == buttons.Length - 1 && buttons[i].isPressed)
                {
                    isOpen = true;
                }
            }
        }
    }


    void DeleteDoor()
    {
        Destroy(this.gameObject);
    }    

    #endregion

    #region CLIENT
    
    public void Update()
    {
        CheckAllButtonsPressed(); 
        if (isOpen)
        {
            DeleteDoor();
        }
    }

    #endregion
}

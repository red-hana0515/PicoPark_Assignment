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
                    Debug.Log("Not Open");
                    break;
                }

                else if (i == buttons.Length - 1 && buttons[i].isPressed)
                {
                    isOpen = true;
                }
            }
        }
    }


    void deleteDoor()
    {
        Destroy(this.gameObject);
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
    // LOL np~
    // The answer was to use none??? LOL
    // if it dies it dies :pray:
    public void Update()
    {
        CheckAllButtonsPressed(); 
        if (isOpen)
        {
            //doorAnim.Play(openDoor);
            deleteDoor();
        }
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PressureButtonMechanic : NetworkBehaviour
{
    [Header("Button Properties")]
    [SerializeField] GameObject buttons;
    [SerializeField] string buttonPressed;
    public bool isPressed;

    Animator buttonAnim;

    #region SERVER


    #endregion


    #region CLIENT
    void Awake()
    {
        buttonAnim = buttons.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            buttonAnim.SetBool(buttonPressed, true);
            isPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            buttonAnim.SetBool(buttonPressed, false);
            isPressed = false;
        }
    }

    #endregion
}

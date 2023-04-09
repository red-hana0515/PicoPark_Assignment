using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerMovementTest pM;

    void isJumping()
    {
        pM.isGrounded = false;
    }

    void isLanded()
    {
        pM.isGrounded = true;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isLanded();
        }

        if(collision.gameObject.layer == 7)
        {
            pM.gameObject.transform.position = new Vector2(0, 0);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isLanded();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping();
        }
    }
}

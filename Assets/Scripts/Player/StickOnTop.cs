using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StickOnTop : NetworkBehaviour
{

    #region SERVER

    #endregion

    #region CLIENT
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }


    #endregion
}

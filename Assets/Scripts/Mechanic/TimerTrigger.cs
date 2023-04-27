using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimerTrigger : NetworkBehaviour
{
    [Header("Trigger Status")]
    [SerializeField] bool startPoint;
    [SerializeField] bool endPoint;

    #region SERVER

    //[Command]
    //public void CmdAssignAuthority(NetworkIdentity collID, NetworkIdentity clientID)
    //{
    //    Debug.Log("Assigned");
    //    collID.AssignClientAuthority(clientID.connectionToClient);
    //}

    #endregion

    #region CLIENT

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isClient)
        {
            //CmdAssignAuthority(playerTimer.gameObject.GetComponent<NetworkIdentity>(), collision.transform.GetComponent<NetworkIdentity>());

            if (startPoint)
            {
                PlayerTimer.instance.CmdCountdown(true);
            }

            else if(endPoint)
            {
                PlayerTimer.instance.CmdCountdown(false);
            }

        }
    }

    #endregion
}

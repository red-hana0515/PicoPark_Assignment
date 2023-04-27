using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimerTrigger : NetworkBehaviour
{
    [Header("Trigger Status")]
    [SerializeField] bool startPoint;
    [SerializeField] bool endPoint;

    [Command]
    public void CmdAssignAuthority(NetworkIdentity targetId, NetworkIdentity clientId)
    {
        if (!targetId.isOwned)
        {
            targetId.AssignClientAuthority(clientId.connectionToClient);
        }
    }

    #region CLIENT

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //CmdAssignAuthority(collision.transform.GetComponent<NetworkIdentity>(), collision.transform.GetComponent<NetworkIdentity>());

            if (startPoint)
            {
                PlayerTimer.enableTimer = true;
            }

            else if (endPoint)
            {
                PlayerTimer.enableTimer = false;
            }

        }
    }

    #endregion
}

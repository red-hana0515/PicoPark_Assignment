using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColorRenderer = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdate))]
    [SerializeField] private string displayName = "Missing Name";

    // SyncVar will sync all variables to all connected players
    [SyncVar(hook = nameof(HandleDisplayColorUpdate))]
    [SerializeField] private Color displayColor = Color.black;

    #region SERVER ONLY FUNCS

    // Clients will not access these functions if [SERVER] is included
    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColor(Color newDisplayColor)
    {
        displayColor = newDisplayColor;
    }

    // Client calls a function from server
    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        //server authority to limit displayeName into 2-20 letters length
        if(newDisplayName.Length < 2 || newDisplayName.Length > 20)
            return;

        RpcDisplayNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region CLIENT FUNCS

    private void HandleDisplayColorUpdate(Color oldCol, Color newCol)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newCol);
    }

    private void HandleDisplayNameUpdate(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set This Name")]
    private void SetThisName()
    {
        CmdSetDisplayName("My New Name");
    }

    // Client and server communicates to each other
    // Server will call a function from ALL clients
    [ClientRpc]
    private void RpcDisplayNewName(string newDisplayeName)
    {
        Debug.Log(newDisplayeName);
    }

    // Server targets to a sepcific client
    // E.g. banning account
    // [TargetRpc]


    #endregion

}

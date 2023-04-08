using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    // Normally responds during loading and calls back to the server after a client is connected
    public override void OnClientConnect()
    {
        base.OnClientConnect();

        Debug.Log("Connected to the server");
    }

    // An ID or identity is connected to the server
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        Debug.Log($"Current number of players = {numPlayers}");
        player.SetDisplayName($"Player {numPlayers}");

        Color displayColor = new Color(Random.Range(0.3f, 1f), Random.Range(0.3f, 1f), Random.Range(0.3f, 1f));
        player.SetDisplayColor(displayColor);
    }

}

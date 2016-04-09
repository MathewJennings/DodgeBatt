using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManagerScript : NetworkManager {

    GameObject player1;
    GameObject player2;
    Color orange = new Color(232, 118, 0);
    int numPlayers = 0;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, GetStartPosition().position, Quaternion.identity);
        if (numPlayers == 0)
        {
            player1 = player;
            Test script = player.GetComponent<Test>();
            script.setColor(Color.blue);
            //script.CmdDestroyFloatingHand();
        }
        else
        {
            player2 = player;
            Test script = player.GetComponent<Test>();
            script.setColor(orange);
            //script.CmdDestroyFloatingHand();
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        numPlayers++;
    }
}

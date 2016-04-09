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
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        if (numPlayers == 0)
        {
            player1 = player;
            player.GetComponent<Test>().setColor(Color.blue);
        }
        else
        {
            player2 = player;
            player.GetComponent<Test>().setColor(orange);
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        numPlayers++;
    }
}

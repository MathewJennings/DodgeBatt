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
            Test script = player.GetComponent<Test>();
            script.setColor(Color.blue);
            script.destroyFloatingHand();
        }
        else
        {
            player2 = player;
            Test script = player.GetComponent<Test>();
            script.setColor(orange);
            script.destroyFloatingHand();
        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        numPlayers++;
    }

    private void destroyFloatingHand(GameObject player)
    {
        Transform l = player.transform.FindChild("CenterEyeAnchor").FindChild("LeapSpace")
                .FindChild("LeapHandController").FindChild("CapsuleHand_L");
        foreach (Transform child in l)
        {
            GameObject.Destroy(child.gameObject);
        }
        Transform r = player.transform.FindChild("CenterEyeAnchor").FindChild("LeapSpace")
            .FindChild("LeapHandController").FindChild("CapsuleHand_R");
        foreach (Transform child in r)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

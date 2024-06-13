using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerTester : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    void Awake()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Oculus_Photon_Player", new Vector3(0, 5, 0), Quaternion.identity);
    }
}

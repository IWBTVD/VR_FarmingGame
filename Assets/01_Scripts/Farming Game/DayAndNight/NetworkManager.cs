using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
 
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        PhotonNetwork.Instantiate("Player", new Vector3(-2f, 0.5f, 3f), rotation);

    }
}

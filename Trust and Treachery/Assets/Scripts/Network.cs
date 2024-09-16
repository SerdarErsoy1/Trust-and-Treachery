using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Network : MonoBehaviourPunCallbacks
{
    public Text statusText;

    public Camera playerCamera;
    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main; // Ana kamerayı otomatik olarak bul
        }

        statusText.text = "Connecting";
        PhotonNetwork.NickName = "Player" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        statusText.text = "Connected to Master / joining room";
        PhotonNetwork.JoinOrCreateRoom("Room" + Random.Range(0, 5000), new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Connected";
        GameObject player = PhotonNetwork.Instantiate("Player",
            new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10)),
            Quaternion.identity);

        playerCamera.GetComponent<FirstPersonCamera>().player = player.transform;
    }
}
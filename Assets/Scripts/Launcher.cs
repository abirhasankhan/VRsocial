using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_InputField createInput;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListItem;
    [SerializeField] Transform playerListItem;
    [SerializeField] GameObject roomListPreFap;
    [SerializeField] GameObject playerListPreFap;
    [SerializeField] GameObject startPlayBtn;



    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {

        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("Commecting to Master");
    }


    public override void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby();

        Debug.Log("Commected to Master");

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
  
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(createInput.text)) // if input field is empty, then just return
        {
            return;
        }
        PhotonNetwork.CreateRoom(createInput.text);
        MenuManager.Instance.OpenMenu("loading");
    }


    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListItem) 
        {
            Destroy(child.gameObject); // when we join the room , it will delete all the previous content
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListPreFap, playerListItem).GetComponent<PlayerListItem>().SetUp(players[i]);

        }

        startPlayBtn.SetActive(PhotonNetwork.IsMasterClient); // Only host can start room
    }

    // If host leave the room then one of the client become host for that room
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startPlayBtn.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");

    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform transform in roomListItem) 
        {
            Destroy(transform.gameObject);
        }

        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListPreFap, roomListItem).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPreFap, playerListItem).GetComponent<PlayerListItem>().SetUp(newPlayer);

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance) // checks if another RoomManger exists
        {
            Destroy(gameObject); // there can only be one :)
            return;
        }
        DontDestroyOnLoad(gameObject); // only one here...
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoad;

    }

    public override void OnDisable( )
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1) // we are on VR world scene
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPreFabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

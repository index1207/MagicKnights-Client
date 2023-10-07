using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;

    private static Managers Instance
    {
        get
        {
            Init();
            return _instance;
        }
    }

    private static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);

            _instance = go.GetComponent<Managers>();
            
            _instance._ui.Init();
            _instance._net.Init();
            JobDispatcher.Instance.Init();
        }
    }

    private NetworkManager _net = new();
    private UIManager _ui = new();
    
    public static NetworkManager Net { get { return Instance._net; }}
    public static UIManager UI { get { return Instance._ui; }}

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Instance._net.Update();
        Instance._ui.Update();
    }

    private void OnApplicationQuit()
    {
        _net.LeaveRoom();
        _net.Disconnect();
    }
}

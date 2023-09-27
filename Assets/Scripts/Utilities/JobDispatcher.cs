using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Google.Protobuf;
using UnityEngine;

public class JobDispatcher : MonoBehaviour
{
    private static JobDispatcher _instance;
    private object _locker = new object();
    public static JobDispatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<JobDispatcher>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("JobDispatcher");
                    _instance = go.AddComponent<JobDispatcher>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }
    
    private Queue<KeyValuePair<Action<IMessage>, IMessage>> _actions;

    public void Init()
    {
        _actions = new();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_actions.Count > 0)
        {
            var top = _actions.Dequeue();
            top.Key.Invoke(top.Value);
        }
    }

    public void Enqueue(Action<IMessage> action, IMessage packet)
    {
        lock (_locker)
        {
            _actions.Enqueue(new KeyValuePair<Action<IMessage>, IMessage>(action, packet));
        }
    }
}

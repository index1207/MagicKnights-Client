using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Packet;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : UIItem
{
    private Button _button;

    PlayButton()
    {
        Type = Define.UIType.Play;
    }
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(()=>
        {
            SceneManager.LoadScene("RoomScene");
            Managers.Net.Send(new C_RoomListReq());
        });
    }
}

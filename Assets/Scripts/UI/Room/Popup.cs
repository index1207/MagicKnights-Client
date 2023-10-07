using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Google.Protobuf.Collections;
using MagicKnights.Api.Packet;
using Network.handler;
using UnityEngine;
using UnityEngine.UI;

using Text = TMPro.TMP_Text;
using InputField = TMPro.TMP_InputField;

public class Popup : UIItem
{
    Popup()
    {
        Type = Define.UIType.Popup;
    }

    public enum PopupType
    {
        Create,
        Enter
    }
    
    public void ShowPopup(PopupType type)
    {
        gameObject.SetActive(true);
        switch (type)
        {
            case PopupType.Create:
                createRoom.gameObject.SetActive(true);
                enterRoom.gameObject.SetActive(false);
                break;
            case PopupType.Enter:
                createRoom.gameObject.SetActive(false);
                enterRoom.gameObject.SetActive(true);
                
                if(Managers.Net.EnterRoom != null)
                    enterRoom.Find("caption").GetComponent<Text>().text = Managers.Net.EnterRoom.Name;
                break;
        }
    }

    private Transform createRoom = null;
    private Transform enterRoom = null;
    
    private void Start()
    {
        transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => gameObject.SetActive(false));
        
        createRoom = transform.Find("Create");
        createRoom.Find("create").GetComponent<Button>().onClick.AddListener(() =>
        {
            C_CreateRoom newRoom = new C_CreateRoom();

            var name = createRoom.Find("name").GetComponent<InputField>().text;
            var password = createRoom.Find("password").GetComponent<InputField>().text;
            
            newRoom.ReqRoom = new FRoom
            {
                Name = name,
                Pwd = password
            };
      
            Managers.Net.Send(newRoom);
            
            gameObject.SetActive(false);
            
            Managers.Net.EnterRoom = newRoom.ReqRoom;
            Managers.UI.Room.SetActive(true);
        });

        enterRoom = transform.Find("Enter");
        enterRoom.Find("enter").GetComponent<Button>().onClick.AddListener(() =>
        {
            C_EnterRoomReq enter = new C_EnterRoomReq
            {
                RoomName = Managers.Net.EnterRoom.Name,
                SubmitPwd = enterRoom.Find("password").GetComponent<InputField>().text
            };
                
            Managers.Net.Send(enter);
            gameObject.SetActive(false);
        });
    }
}

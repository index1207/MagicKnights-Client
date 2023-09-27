using System;
using System.Collections;
using System.Collections.Generic;
using Packet;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TMP_Text;

public class RoomButton : MonoBehaviour
{
    public string Caption
    {
        get => Room.Name;
        set => transform.Find("caption").GetComponent<Text>().text = value;
    }

    public Room Room
    {
        get => _room;
        set
        {
            _room = value;
            Caption = _room.Name;
        }
    }

    private Room _room;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {   
            Managers.Net.EnterRoom = Room;
            Managers.UI.Popup.GetComponent<Popup>().ShowPopup(Popup.PopupType.Enter);
        });
    }
}

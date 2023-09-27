using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Packet;
using UnityEngine;
using Text = TMPro.TMP_Text;

public class RoomMenu : UIItem
{
    RoomMenu()
    {
        Type = Define.UIType.Room;
    }

    public string Caption
    {
        get => _caption.text;
        set => _caption.text = value;
    }
    
    private Text[] _playerBoard = new Text[2];

    private Text _caption;

    private void OnEnable()
    {
        UpdateRoomStatus();
    }

    private void Awake()
    {
        _caption = transform.Find("name").GetComponent<Text>();
        for(int i = 0; i < _playerBoard.Length; ++i)
        {
            _playerBoard[i] = transform.Find($"member{i+1}").GetChild(0).GetComponent<Text>();
        }
    }

    public void UpdateRoomStatus()
    {
        if (Managers.Net.EnterRoom != null)
        {
            var room = Managers.Net.EnterRoom;
            Caption = room.Name;

            for (int i = 0; i < 2; ++i)
            {
                try
                {
                    _playerBoard[i].text = $"Player{room.EnterPlayers[i]}";
                }
                catch (Exception)
                {
                    _playerBoard[i].text = "";
                }
            }

            // if (room.EnterPlayers.Count == 1)
            // {
            //     _playerBoard[0].text = $"Player{room.EnterPlayers[0]}";
            // }
            // else for (int i = 0; i < room.EnterPlayers.Count; ++i)
            // {
            //     _playerBoard[i].text = $"Player{room.EnterPlayers[i]}";
            // }
        }
    }
}

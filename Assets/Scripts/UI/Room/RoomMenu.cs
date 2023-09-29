using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Packet;
using UnityEngine;
using UnityEngine.UI;
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
    private Button _startBtn;
    
    private void OnEnable()
    {
        UpdateRoomStatus();
    }

    private void Awake()
    {
        _startBtn = transform.Find("start").GetComponent<Button>();
        _caption = transform.Find("name").GetComponent<Text>();
        
        _startBtn.gameObject.SetActive(false);
        _startBtn.onClick.AddListener(() =>
        {
            Managers.Net.Send(new C_StartGame());
        });
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

            // update enter user list
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
            
            // start game button
            if (room.EnterPlayers.Count == 2)
            {
                if (Managers.Net.PlayerId == room.EnterPlayers[0])
                {
                    _startBtn.gameObject.SetActive(true);
                }
            }
            else
            {
                _startBtn.gameObject.SetActive(false);
            }
        }
    }
}

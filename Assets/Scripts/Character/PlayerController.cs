using System;
using System.Collections;
using System.Collections.Generic;
using MagicKnights.Api.Packet;
using UnityEngine;

public class PlayerController : CharacterContorller
{
    private Vector3 _movePos;
    private EInputDirection _prevInput = EInputDirection.None;
    
    private void Update()
    {
        _movePos = Vector3.zero;
        EInputDirection newDir = EInputDirection.None;
        if (Input.GetKey(KeyCode.W))
        {
            newDir = EInputDirection.Up;
            _movePos += Vector3.up;
            _movePos *= _speed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            newDir = EInputDirection.Left;
            _movePos += Vector3.left;
            _movePos *= _speed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            newDir = EInputDirection.Down;
            _movePos += Vector3.down;
            _movePos *= _speed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            newDir = EInputDirection.Right;
            _movePos += Vector3.right;
            _movePos *= _speed * Time.fixedDeltaTime;
        }
        UpdateStatus(newDir);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigid.MovePosition(transform.position + _movePos);
    }

    void UpdateStatus(EInputDirection dir)
    {
        if (_prevInput != dir)
        {
            _prevInput = dir;
            
            C_Move move = new C_Move
            {
                Position = Utils.Convert(transform.position),
                Dir = dir
            };
            Managers.Net.Send(move);
        }
    }
}

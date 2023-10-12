using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using MagicKnights.Api.Packet;
using UnityEngine;

public class PlayerController : CharacterContorller
{
    private EInputDirection _prevInput = EInputDirection.None;

    private void Update()
    {
        EInputDirection newDir = EInputDirection.None;
        if (Input.GetKey(KeyCode.W))
        {
            newDir = EInputDirection.Up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            newDir = EInputDirection.Left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            newDir = EInputDirection.Down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            newDir = EInputDirection.Right;
        }

        UpdateStatus(newDir);
    }

    void UpdateStatus(EInputDirection dir)
    {
        if (_prevInput != dir)
        {
            _prevInput = dir;
            
            C_MoveInput move = new C_MoveInput
            {
                Position = new FVector3(Utils.Convert(transform.position)),
                Dir = dir
            };
            Managers.Net.Send(move);
        }
    }
}

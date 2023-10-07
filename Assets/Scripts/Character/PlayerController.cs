using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterContorller
{
    private Vector3 _movePos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        _movePos = new Vector3(horizontal, vertical, 0).normalized;
        if (_movePos == Vector3.zero)
        {
            // TODO: Send Move:None packet.
        }
        else
        {
            // TODO: Send Move:{DIR} packet.
        }

        _movePos = _movePos * (_speed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigid.MovePosition(transform.position + _movePos);
    }
}

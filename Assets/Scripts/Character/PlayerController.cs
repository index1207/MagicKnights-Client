using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterContorller
{
    private Vector3 _moveDir;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        _moveDir = new Vector3(horizontal, vertical, 0).normalized;
        if (_moveDir == Vector3.zero)
        {
            
        }

        _moveDir = _moveDir * (_speed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigid.MovePosition(transform.position + _moveDir);
    }
}

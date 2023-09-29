using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContorller : MonoBehaviour
{
    protected float _speed = 3;
    
    protected Rigidbody2D _rigid;
    protected Animator _animator;

    protected virtual void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

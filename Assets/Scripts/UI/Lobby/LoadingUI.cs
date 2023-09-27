using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Google.Protobuf;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TMP_Text;

public class LoadingUI : UIItem
{
    public Slider Bar { get; private set; }

    private Text _text;
    private Button _exitBtn;

    LoadingUI()
    {
        Type = Define.UIType.Loading;
    }
    
    private void Awake()
    {
        _text = transform.Find("Text").GetComponent<Text>();
        Bar = transform.Find("Bar").GetComponent<Slider>();
        _exitBtn = transform.Find("Button").GetComponent<Button>();
        
        _exitBtn.onClick.AddListener(() =>
        {
            Application.Quit(0);
        });
    }

    public void SetText(string msg)
    {
        _text.SetText(msg);
    }

    public void SetText(string msg, Color color)
    {
        _text.SetText(msg);
        _text.color = color;
    }

    public void SetActiveExitButton(bool isActive) => _exitBtn.gameObject.SetActive(isActive);
}

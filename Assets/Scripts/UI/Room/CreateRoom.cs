using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Managers.UI.Popup.SetActive(true);
            Managers.UI.Popup.ShowPopup(Popup.PopupType.Create);
        });
    }
}

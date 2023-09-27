using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    public Define.UIType Type { get; protected set; }

    public void SetActive(bool isActivate)
    {
        gameObject.SetActive(isActivate);
    }
}

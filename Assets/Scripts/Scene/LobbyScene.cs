using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : GameScene
{
    private async void Start()
    {
        if (!Managers.Net.IsConnected)
        {
            await Managers.Net.Connect();
        }
        else
        {
            Managers.UI.LoadingUI.SetActive(false);
            Managers.UI.PlayButton.SetActive(true);
        }
    }
}

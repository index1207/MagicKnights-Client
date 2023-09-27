using System;
using System.Threading.Tasks;
using Google.Protobuf;
using Packet;
using UnityEngine;

namespace Network.handler
{
    public static class Game
    {
        public static async void OnConnected(S_ConnectedToServer conn)
        {
            Managers.Net.IsConnected = true;
            Managers.Net.PlayerId = conn.PlayerId;
            
            float time = 0;
            float cur = Managers.UI.LoadingUI.Bar.value;
            while (time <= 1f)
            {
                time += Time.deltaTime;
                Managers.UI.LoadingUI.Bar.value = Mathf.Lerp(cur, 1f, time);
                await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            }
            Managers.UI.LoadingUI.SetText("연결됨!");
            
            await Task.Delay(TimeSpan.FromSeconds(0.3f));
            
            Managers.UI.LoadingUI.SetActive(false);
            Managers.UI.PlayButton.SetActive(true);
        }
    }
}
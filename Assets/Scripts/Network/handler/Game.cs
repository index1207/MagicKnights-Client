﻿using System;
using System.Threading.Tasks;
using MagicKnights.Api.Packet;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public static async void Start(S_NotifyStartGame startGame)
        {
            Debug.Log(Managers.Net.EnterRoom.EnterPlayers.ToString());
            
            var ap = SceneManager.LoadSceneAsync("GameScene");
            while (!ap.isDone)
            {
                await Task.Delay(1);
            }
            
            foreach (var player in Managers.Net.EnterRoom.EnterPlayers)
            {
                if (Managers.Net.PlayerId == player)
                {
                    // Spawn my player
                    GameObject pfMyPlayer = Resources.Load("Prefabs/Player/Player") as GameObject;
                    GameObject myPlayer = GameObject.Instantiate(pfMyPlayer);
                    myPlayer.transform.position = new Vector2(0, -3);
                }
                else
                {
                    // Spawn another player
                    GameObject pfPlayer = Resources.Load("Prefabs/Player/RemotePlayer") as GameObject;
                    GameObject otherPlayer = GameObject.Instantiate(pfPlayer);
                    otherPlayer.transform.position = new Vector2(0, 2);
                    
                    Player.RemotePlayer = otherPlayer;
                }
            }
        }
    }
}
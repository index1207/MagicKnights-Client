using System.Collections;
using System.Collections.Generic;
using Packet;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void Back()
    {
        if (Managers.Net.EnterRoom != null)
        {
            Managers.UI.Room.SetActive(false);
        
            Managers.Net.LeaveRoom();

            C_RoomListReq loadRoomList = new C_RoomListReq();
            Managers.Net.Send(loadRoomList);
        }
        else
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
}

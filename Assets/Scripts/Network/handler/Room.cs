using System.Collections.Generic;
using Google.Protobuf;
using Packet;
using UnityEngine;

namespace Network.handler
{
    public static class Room
    {
        public static Dictionary<string, GameObject> RoomList
        {
            get => _roomList;
        }
        private static Dictionary<string, GameObject> _roomList = new();
        
        public static void GetRoomList(S_RoomListRes roomList)
        {
            _roomList.Clear();
            if (Managers.UI.CurrentScene.name != "RoomScene")
            {
                return;
            }
            
            
            foreach (Transform child in GameObject.Find("grid").transform)
            {
                Object.Destroy(child.gameObject);
            }
            
            GameObject pfRoom = Resources.Load<GameObject>("Prefabs/UI/RoomButton");
            foreach (var room in roomList.Rooms)
            {
                if (Managers.Net.EnterRoom != null && Managers.Net.EnterRoom.Name == room.Name)
                {
                    Managers.Net.EnterRoom = room;
                    Managers.UI.Room.UpdateRoomStatus();
                }
                if (room.EnterPlayers.Count < 2)
                {
                    GameObject newRoomBtn = Object.Instantiate(pfRoom, Vector3.zero, Quaternion.identity, GameObject.Find("grid").transform);
                    newRoomBtn.GetComponent<RoomButton>().Room = room;
                    _roomList.Add(room.Name, newRoomBtn);
                }
            }
        }

        public static void CanEnter(S_EnterRoomRes enterRes)
        {
            if (enterRes.IsOk)
            {
                Managers.Net.EnterRoom = enterRes.EnterRoom;
                if (Managers.UI.Room.gameObject.activeSelf)
                {
                    Managers.UI.Room.UpdateRoomStatus();
                }
                else
                {
                    Managers.UI.Popup.SetActive(false);
                    Managers.UI.Room.SetActive(true);
                }
            }
            else
            {
                Managers.Net.EnterRoom = null;
            }
        }

        public static void LeaveRoom(S_UnicastLeaveRoom leave)
        {
            Managers.Net.EnterRoom = leave.Room;
            Managers.UI.Room.UpdateRoomStatus();
        }
    }
}
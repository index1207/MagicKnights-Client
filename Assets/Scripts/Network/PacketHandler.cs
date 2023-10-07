using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using MagicKnights.Api.Packet;
using Network.handler;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;
using Object = UnityEngine.Object;
using Room = Network.handler.Room;

namespace DefaultNamespace
{
    public static class PacketHandler
    {
        public static void Dispatch(object sender, MessageEventArgs message)
        {
            if (message.RawData == null)
                return;
            
            byte[] data = message.RawData;
            ushort pktId = BitConverter.ToUInt16(new Span<byte>(data, 0, 2));
            byte[] serializedData = message.RawData.SubArray(2, data.Length-2);
            
            switch (pktId)
            {
                case (ushort)EPacketID.SConnectedToServer:
                    JobDispatcher.Instance.Enqueue(S_ConnectedToServerHandler, S_ConnectedToServer.Parser.ParseFrom(serializedData));
                    break;
                case (ushort)EPacketID.SRoomListRes:
                    JobDispatcher.Instance.Enqueue(S_RoomListHandler, S_RoomList.Parser.ParseFrom(serializedData));
                    break;
                case (ushort)EPacketID.SEnterRoomRes:
                    JobDispatcher.Instance.Enqueue(S_EnterRoomResHandler, S_EnterRoomRes.Parser.ParseFrom(serializedData));
                    break;
                case (ushort)EPacketID.SUnicastLeaveRoom:
                    JobDispatcher.Instance.Enqueue(S_NotifyLeaveRoomHandler, S_NotifyLeaveRoom.Parser.ParseFrom(serializedData));
                    break;
                case (ushort)EPacketID.SUnicastStartGame:
                    JobDispatcher.Instance.Enqueue(S_NotifyStartGameHandler, S_NotifyStartGame.Parser.ParseFrom(serializedData));
                    break;
            }
        }

        private static void S_NotifyStartGameHandler(IMessage obj)
        {
            S_NotifyStartGame startGame = (S_NotifyStartGame)obj;
            Game.Start(startGame);
        }

        private static void S_NotifyLeaveRoomHandler(IMessage packet)
        {
            S_NotifyLeaveRoom leave = (S_NotifyLeaveRoom)packet;
            Room.LeaveRoom(leave);
        }

        private static void S_ConnectedToServerHandler(IMessage packet)
        {
            S_ConnectedToServer enter = (S_ConnectedToServer)packet;
            Game.OnConnected(enter);
        }

        private static void S_RoomListHandler(IMessage packet)
        {
            S_RoomList roomList = (S_RoomList)packet;
            Room.GetRoomList(roomList);
        }

        private static void S_EnterRoomResHandler(IMessage packet)
        {
            S_EnterRoomRes enterRes = (S_EnterRoomRes)packet;
            Room.CanEnter(enterRes);
        }
    }
}
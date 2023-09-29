using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Packet;
using Network.handler;
using Packet;
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
                case (ushort)PacketID.SConnectedToServer:
                    JobDispatcher.Instance.Enqueue(S_ConnectedToServerHandler, S_ConnectedToServer.Parser.ParseFrom(serializedData));
                    break;
                case (ushort)PacketID.SRoomListRes:
                    JobDispatcher.Instance.Enqueue(S_RoomListResHandler, S_RoomListRes.Parser.ParseFrom(serializedData));
                    break;
                case (ushort)PacketID.SEnterRoomRes:
                    JobDispatcher.Instance.Enqueue(S_EnterRoomResHandler, S_EnterRoomRes.Parser.ParseFrom(serializedData));
                    break;
                case (ushort)PacketID.SUnicastLeaveRoom:
                    JobDispatcher.Instance.Enqueue(S_UnicastLeaveRoomHandler, S_UnicastLeaveRoom.Parser.ParseFrom(serializedData));
                    break;
                case (ushort)PacketID.SUnicastStartGame:
                    JobDispatcher.Instance.Enqueue(S_UnicastStartGameHandler, S_UnicastStartGame.Parser.ParseFrom(serializedData));
                    break;
            }
        }

        private static void S_UnicastStartGameHandler(IMessage obj)
        {
            S_UnicastStartGame startGame = (S_UnicastStartGame)obj;
            Game.Start(startGame);
        }

        private static void S_UnicastLeaveRoomHandler(IMessage packet)
        {
            S_UnicastLeaveRoom leave = (S_UnicastLeaveRoom)packet;
            Room.LeaveRoom(leave);
        }

        private static void S_ConnectedToServerHandler(IMessage packet)
        {
            S_ConnectedToServer enter = (S_ConnectedToServer)packet;
            Game.OnConnected(enter);
        }

        private static void S_RoomListResHandler(IMessage packet)
        {
            S_RoomListRes roomList = (S_RoomListRes)packet;
            Room.GetRoomList(roomList);
        }

        private static void S_EnterRoomResHandler(IMessage packet)
        {
            S_EnterRoomRes enterRes = (S_EnterRoomRes)packet;
            Room.CanEnter(enterRes);
        }
    }
}
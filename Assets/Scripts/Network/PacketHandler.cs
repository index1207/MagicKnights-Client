using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using MagicKnights.Api.Packet;
using Network.handler;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;
using Object = UnityEngine.Object;

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

            JobDispatcher.Instance.Enqueue(() => DividePacket(pktId, serializedData));
        }

        
        private static void DividePacket(ushort packetId, byte[] data)
        {
            switch (packetId)
            {
                case (ushort)EPacketID.SConnectedToServer:
                    S_ConnectedToServerHandler(S_ConnectedToServer.Parser.ParseFrom(data));
                    break;
                case (ushort)EPacketID.SRoomList:
                    S_RoomListHandler(S_RoomList.Parser.ParseFrom(data));
                    break;
                case (ushort)EPacketID.SEnterRoomRes:
                    S_EnterRoomResHandler(S_EnterRoomRes.Parser.ParseFrom(data));
                    break;
                case (ushort)EPacketID.SUnicastLeaveRoom:
                    S_NotifyLeaveRoomHandler(S_NotifyLeaveRoom.Parser.ParseFrom(data));
                    break;
                case (ushort)EPacketID.SNotifyStartGame:
                    S_NotifyStartGameHandler(S_NotifyStartGame.Parser.ParseFrom(data));
                    break;
                case (ushort)EPacketID.SMoveInput:
                    S_MoveHandler(S_MoveInput.Parser.ParseFrom(data));
                    break;
            }
        }
        
        private static void S_MoveHandler(IMessage packet)
        {
            S_MoveInput remote = (S_MoveInput)packet;
            Player.Move(remote);
        }

        private static void S_NotifyStartGameHandler(IMessage packet)
        {
            S_NotifyStartGame startGame = (S_NotifyStartGame)packet;
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
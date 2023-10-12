using MagicKnights.Api.Packet;
using UnityEngine;

namespace Network.handler
{
    public class Player
    {
        public static GameObject RemotePlayer
        {
            get => _remotePlayer;
            set => _remotePlayer = value;
        }

        private static GameObject _remotePlayer;
        
        public static void Move(S_MoveInput remote)
        {
            if (remote.PlayerId != Managers.Net.PlayerId)
            {
                RemotePlayer.transform.position = Utils.Convert(remote.Position);
            }
        }
    }
}
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
        
        public static void Move(S_Move remote)
        {
            _remotePlayer.transform.position = Utils.Convert(remote.Position);
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DefaultNamespace;
using Google.Protobuf;
using Google.Protobuf.Packet;
using Packet;
using UnityEngine;
using WebSocketSharp;

public class NetworkManager : BaseManager
{
    WebSocket Connection { get; set; }
    private string _address = "";
    private ushort _port = 8081;

    public bool IsConnected { get; set; }
    public int PlayerId { get; set; }

    public Room EnterRoom { get; set; }

    public void Init()
    {
        string path = "";
        #if UNITY_EDITOR
        path = "Build/config.txt";
        #else
        path = "config.txt";
        #endif
        
        string host = "";
        using (var reader = File.Open(path, FileMode.Open))
        {
            byte[] buffer = new byte[reader.Length];
            var read = reader.Read(buffer);
            host = Encoding.UTF8.GetString(buffer);
        }
        
        Debug.Log("["+host+']');
        
        var entry = Dns.GetHostEntry(Dns.GetHostName());
        _address = entry.AddressList[1].ToString();
        
        Connection = new WebSocket($"ws://{host}:{_port}");

        Connection.OnOpen += (sender, args) =>
        {
            // Running at Non-Monobehaviour
        };

        Connection.OnMessage += PacketHandler.Dispatch;
    }

    public void Update()
    {
    }

    public async Task Connect()
    {
        Connection.ConnectAsync();

        float time = 0.0f;
        while (time <= 1f && !IsConnected)
        {
            time += Time.deltaTime;
            Managers.UI.LoadingUI.Bar.value = Mathf.Lerp(0, 0.5f, time);
            await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
        }

        if (!Connection.IsAlive)
        {
            Managers.UI.LoadingUI.SetText("연결실패..", Color.red);
            Managers.UI.LoadingUI.SetActiveExitButton(true);
        }
    }

    public void Disconnect()
    {
        Connection.Close();
    }
    
    public void Send(IMessage packet)
    {
        string s = packet.Descriptor.Name.Replace("_", "");

        PacketID id = (PacketID)Enum.Parse(typeof(PacketID), s);

        ushort size = (ushort)packet.CalculateSize();
        byte[] buffer = new byte[size + 2];
        
        Array.Copy(BitConverter.GetBytes((ushort)id), 0, buffer, 0, sizeof(ushort));
        if(buffer.Length > 2)
            Array.Copy(packet.ToByteArray(), 0, buffer, sizeof(short), buffer.Length-2);

        Connection.Send(buffer);
    }

    public void LeaveRoom()
    {
        if (EnterRoom != null)
        {
            C_LeaveRoom leave = new C_LeaveRoom();
            Send(leave);
        }
    }
}

using Google.Protobuf;
using PacketDotNet;
using SharpPcap;
using Com.Ankama.Dofus.Server.Game.Protocol;
using Com.Ankama.Dofus.Server.Connection.Protocol;
using Com.Ankama.Dofus.Server.Game.Protocol.Chat;
using System.Net.Sockets;

class Program
{
    private static Dictionary<string, List<byte>> bufferMap = new();
    private static List<string> connectionIPList = new() { "34.249.146.47", "54.74.22.247" };

    private static TcpClient client;
    private static NetworkStream stream;

    static void Main(string[] args)
    {
        bufferMap["ConnectionServerClientToServer"] = new List<byte>();
        bufferMap["ConnectionServerServerToClient"] = new List<byte>();
        bufferMap["GameServerClientToServer"] = new List<byte>();
        bufferMap["GameServerServerToClient"] = new List<byte>();

        var devices = CaptureDeviceList.Instance;
        if (devices.Count == 0)
        {
            Console.WriteLine("Aucune interface réseau trouvée.");
            return;
        }

        var device = devices[3];  // Choisir l'interface réseau
        device.OnPacketArrival += new PacketArrivalEventHandler(Device_OnPacketArrival);

        device.Open(DeviceModes.Promiscuous);
        Console.WriteLine($"Écoute sur : {device.Description}");
        device.StartCapture();

        Console.WriteLine("Appuie sur Entrée pour arrêter...");
        Console.ReadLine();
        device.StopCapture();
        device.Close();
    }

    private static void Device_OnPacketArrival(object sender, PacketCapture e)
    {
        var rawPacket = e.GetPacket();
        var packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
        var ethernetPacket = packet.Extract<EthernetPacket>();

        if (ethernetPacket != null && ethernetPacket.PayloadPacket is IPv4Packet ipPacket)
        {
            HandleIPv4Packet(ipPacket);
        }
    }

    public static void SendToServer(int port, byte[] data)
    {
        foreach (var ipList in connectionIPList)
        {
            try
            {
                using (var client = new TcpClient(ipList, port))
                using (var stream = client.GetStream())
                {
                    // Envoyer le message encodé
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("[INFO] Message sent to server.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to send message: {ex.Message}");
            }
        }
    }

    private static void HandleIPv4Packet(IPv4Packet ipPacket)
    {
        if (ipPacket.PayloadPacket is TcpPacket tcpPacket)
        {
            HandleDofusPacket(ipPacket, tcpPacket);
        }
    }

    private static void HandleDofusPacket(IPv4Packet ipPacket, TcpPacket tcpPacket)
    {
        if (tcpPacket.DestinationPort != 5555 && tcpPacket.SourcePort != 5555)
        {
            return;
        }

        if (tcpPacket.PayloadData.Length == 0)
        {
            return;
        }

        var serverType = connectionIPList.Contains(ipPacket.SourceAddress.ToString()) ? ServerType.ConnectionServer : ServerType.GameServer;
        IMessage message = serverType == ServerType.ConnectionServer ? new LoginMessage() : new GameMessage();

        var direction = tcpPacket.SourcePort == 5555 ? Direction.ServerToClient : Direction.ClientToServer;
        var bufferId = $"{serverType}{direction}";

        if (!bufferMap.ContainsKey(bufferId))
        {
            bufferMap[bufferId] = new List<byte>();
        }


        if (!bufferMap.ContainsKey(bufferId))
        {
            bufferMap[bufferId] = new List<byte>();
        }

        var partialBuffer = bufferMap[bufferId];
        partialBuffer.AddRange(tcpPacket.PayloadData);

        while (partialBuffer.Count > 0)
        {
            int sizeLength = 0;
            int size = (int)DecodeVarint(partialBuffer.ToArray(), ref sizeLength);

            // Si le message est incomplet, on attend plus de data
            if (size == 0 || size > partialBuffer.Count - sizeLength)
                break;

            var payload = partialBuffer.GetRange(sizeLength, size).ToArray();
            partialBuffer.RemoveRange(0, sizeLength + size);

            if (!Protocol.DecodeMessage(payload, message)) // Step 1 décoder le payload 
            {
                Console.WriteLine($"[ERREUR] Impossible de décoder le message Protobuf: {BitConverter.ToString(payload)}");
                continue;
            }

            ProcessMessage(direction, serverType, message);
        }

        bufferMap[bufferId] = partialBuffer;
    }

    private static void ProcessMessage(Direction direction, ServerType serverType, IMessage message)
    {
        string msgType = "UNK";
        string msgName = "Unknown";

        // ConnectionServer
        if (serverType == ServerType.ConnectionServer && message is LoginMessage cm)
        {
            switch (cm.ContentCase)
            {
                case LoginMessage.ContentOneofCase.Request:
                    msgType = "REQ";
                    msgName = cm.Request.ToString();
                    break;

                case LoginMessage.ContentOneofCase.Response:
                    msgType = "RES";
                    msgName = cm.Response.ToString();
                    break;

                case LoginMessage.ContentOneofCase.Event:
                    msgType = "EVT";
                    msgName = cm.Event.ToString();
                    break;
            }
        }
        // GameServer
        else if (serverType == ServerType.GameServer && message is GameMessage gm)
        {
            switch (gm.ContentCase)
            {
                case GameMessage.ContentOneofCase.Request:
                    msgType = "REQ";
                    msgName = gm.Request.Content.TypeUrl;
                    break;

                case GameMessage.ContentOneofCase.Response:
                    msgType = "RES";
                    msgName = gm.Response.Content.TypeUrl;
                    break;
                case GameMessage.ContentOneofCase.Event:
                    msgType = "EVT";
                    msgName = gm.Event.Content.TypeUrl;
                    break;
            }
        }

        Protocol.PrettyPrintMessage(message); // Step 2 print le message avec le resolver

        Console.ForegroundColor = direction == Direction.ClientToServer ? ConsoleColor.Cyan : ConsoleColor.Blue;
        Console.WriteLine($"[{direction}] [{msgType}] {msgName}");
        Console.ResetColor();
    }


    private static ulong DecodeVarint(byte[] buffer, ref int bytesRead)
    {
        ulong value = 0;
        int shift = 0;

        for (int i = 0; i < buffer.Length; i++)
        {
            byte b = buffer[i];
            value |= (ulong)(b & 0x7F) << shift;
            bytesRead++;

            if ((b & 0x80) == 0)
                return value;

            shift += 7;
            if (shift >= 64)
                throw new InvalidOperationException("Varint trop long.");
        }

        return 0;
    }
}
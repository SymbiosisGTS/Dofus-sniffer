using Com.Ankama.Dofus.Server.Connection.Protocol;
using Com.Ankama.Dofus.Server.Game.Protocol;
using Com.Ankama.Dofus.Server.Game.Protocol.Chat;
using Com.Ankama.Dofus.Server.Game.Protocol.Gamemap;
using Dofus_sniffer;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


public enum ServerType
{
    ConnectionServer,
    GameServer
}

public enum Direction
{
    ClientToServer,
    ServerToClient
}

public static class Protocol
{
    private static readonly CustomResolver customResolver = new();

    public static string PrettyPrintMessage(IMessage message)
    {
        // Step 3 à partir d'ici utiliser le resolver en fonction du typeUrl 
        // Method FindByURL or Name pour récupérer le descriptor qui donnera accès au parsing
        try
        {
            var jsonFormatter = new JsonFormatter(new JsonFormatter.Settings(true));

            if (message is GameMessage gm)
            {
                var anyMsg = gm.Request?.Content ?? gm.Response?.Content ?? gm.Event?.Content;

                if (anyMsg != null)
                {
                    Console.WriteLine($"[DEBUG] Found Any TypeUrl: {anyMsg.TypeUrl}");

                    // Test uniquement si c'est du chat
                    if (anyMsg.TypeUrl == "type.ankama.com/iyb")
                    {
                        var chatRequest = ChatChannelMessageRequest.Parser.ParseFrom(anyMsg.Value);
                        Console.WriteLine($"[INFO] Chat Request Unpacked: {chatRequest}");
                        return jsonFormatter.Format(chatRequest);
                    }
                    if (anyMsg.TypeUrl == "type.ankama.com/iyc")
                    {
                        Console.WriteLine("[INFO] ChatChannelMessageEvent detected!");
                        try
                        {
                            // Unpack directement en tant que ChatChannelMessageEvent
                            var chatEvent = ChatChannelMessageEvent.Parser.ParseFrom(anyMsg.Value);
                            Console.WriteLine($"[INFO] Chat Event Unpacked: {chatEvent}");
                            return jsonFormatter.Format(chatEvent);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Failed to unpack Chat Event: {ex.Message}");
                            return $"Error unpacking chat event: {ex.Message}";
                        }
                    }
                    else
                    {
                        Console.WriteLine("[INFO] Non-chat message detected. Skipping unpack.");
                    }
                }
                else
                {
                    Console.WriteLine("[INFO] No Any Content found in the message.");
                }
            }

            return jsonFormatter.Format(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Error formatting message: {ex.Message}");
            return $"Error formatting message: {ex.Message}";
        }
    }


    public static byte[] EncodeMessage(IMessage message)
    {
        // Sérialisation du message Protobuf
        byte[] data = message.ToByteArray();

        // Buffer pour stocker la taille encodée en Varint
        byte[] varIntBuffer = new byte[10];  // Max 10 octets pour un Varint 64-bit
        int sizeLength = WriteVarint(varIntBuffer, data.Length);

        // Combiner la taille Varint + le message sérialisé
        return Combine(varIntBuffer.AsSpan(0, sizeLength).ToArray(), data);
    }

    // Fonction pour encoder un entier en Varint (similaire à Go's binary.PutUvarint)
    private static int WriteVarint(byte[] buffer, int value)
    {
        int i = 0;
        while (value >= 0x80)
        {
            buffer[i++] = (byte)(value | 0x80);
            value >>= 7;
        }
        buffer[i++] = (byte)value;
        return i;  // Retourne la longueur du Varint encodé
    }

    // Combiner deux tableaux de bytes (taille + message)
    private static byte[] Combine(byte[] first, byte[] second)
    {
        byte[] result = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, result, 0, first.Length);
        Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
        return result;
    }

    public static bool DecodeMessage(byte[] data, IMessage message)
    {
        try
        {
            message.MergeFrom(data);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error decoding message: {ex.Message}");
            return false;
        }
    }

    public static IMessage DecodeSubMessage(GameMessage message)
    {
        var request = message.Request;
        var typeUrl = request.Content.TypeUrl;
        var anyMsg = request.Content;

        var descriptor = customResolver.FindMessageByURL(typeUrl);
        if (descriptor == null)
        {
            throw new Exception($"Type URL {typeUrl} non trouvé dans le resolver.");
        }

        IMessage decodedMessage = descriptor.Parser.ParseFrom(anyMsg.Value);
        return decodedMessage;
    }

    public static GameMessage EncodeGameRequest(IMessage request)
    {
        var anyMsg = Google.Protobuf.WellKnownTypes.Any.Pack(request, "type.ankama.com/");

        return new GameMessage
        {
            Request = new Com.Ankama.Dofus.Server.Game.Protocol.Request
            {
                Content = new Google.Protobuf.Any
                {
                    TypeUrl = anyMsg.TypeUrl,
                    Value = anyMsg.Value
                }
            }
        };
    }

    public static GameMessage EncodeGameResponse(IMessage response)
    {
        var anyMsg = Google.Protobuf.WellKnownTypes.Any.Pack(response, "type.ankama.com/");

        return new GameMessage
        {
            Response = new Com.Ankama.Dofus.Server.Game.Protocol.Response
            {
                Content = new Google.Protobuf.Any
                {
                    TypeUrl = anyMsg.TypeUrl,
                    Value = anyMsg.Value
                }
            }
        };
    }

    public static GameMessage EncodeGameEvent(IMessage evt)
    {
        var anyMsg = Google.Protobuf.WellKnownTypes.Any.Pack(evt, "type.ankama.com/");
        return new GameMessage
        {
            Event = new Com.Ankama.Dofus.Server.Game.Protocol.Event
            {
                Content = new Google.Protobuf.Any
                {
                    TypeUrl = anyMsg.TypeUrl,
                    Value = anyMsg.Value
                }
            }
        };
    }

    //public static IMessage JsonToProto(string json, IMessage descriptor)
    //{
    //    JsonParser parser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

    //    // Crée une nouvelle instance du message à partir du descriptor
    //    IMessage message = (IMessage)descriptor.Parser.ParseFrom(new byte[0]);


    //    // Remplit l'instance avec les données JSON
    //    return parser.Parse(json, message);
    //}
    //private static byte[] Combine(byte[] first, byte[] second)
    //{
    //    byte[] result = new byte[first.Length + second.Length];
    //    Buffer.BlockCopy(first, 0, result, 0, first.Length);
    //    Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
    //    return result;
    //}
}

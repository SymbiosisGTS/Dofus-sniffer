using Google.Protobuf;
using Google.Protobuf.Reflection;
using Com.Ankama.Dofus.Server.Game.Protocol.Chat;

namespace Dofus_sniffer
{
    public class CustomResolver
    {
        private readonly Dictionary<string, MessageDescriptor> types = new Dictionary<string, MessageDescriptor>();

        public CustomResolver()
        {
            RegisterType(ChatChannelMessageEvent.Descriptor);
        }

        private void RegisterType(MessageDescriptor descriptor)
        {
            string typeUrl = $"type.ankama.com/{descriptor.FullName}";
            types[typeUrl] = descriptor;
        }
        // Recherche par URL
        public MessageDescriptor FindMessageByURL(string url)
        {
            if (types.TryGetValue(url, out var descriptor))
            {
                return descriptor;
            }
            throw new KeyNotFoundException($"Type {url} not found in resolver.");
        }

        // Recherche par nom
        public MessageDescriptor FindMessageByName(string name)
        {
            foreach (var descriptor in types.Values)
            {
                if (descriptor.Name == name)
                {
                    return descriptor;
                }
            }
            throw new KeyNotFoundException($"Message {name} not found.");
        }

        // Crée un registre
        public static CustomResolver CreateRegistry()
        {
            return new CustomResolver();
        }
    }
}

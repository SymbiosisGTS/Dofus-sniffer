// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ui.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Com.Ankama.Dofus.Server.Game.Protocol.Ui {

  /// <summary>Holder for reflection information generated from ui.proto</summary>
  public static partial class UiReflection {

    #region Descriptor
    /// <summary>File descriptor for ui.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static UiReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cgh1aS5wcm90bxIoY29tLmFua2FtYS5kb2Z1cy5zZXJ2ZXIuZ2FtZS5wcm90",
            "b2NvbC51aSKnAgoTQ2xpZW50VUlPcGVuZWRFdmVudBJSCgR0eXBlGAEgASgO",
            "MkQuY29tLmFua2FtYS5kb2Z1cy5zZXJ2ZXIuZ2FtZS5wcm90b2NvbC51aS5D",
            "bGllbnRVSU9wZW5lZEV2ZW50LlVJVHlwZRIXCgpvYmplY3RfdWlkGAIgASgF",
            "SACIAQEikwEKBlVJVHlwZRINCglVTkRFRklORUQQABIYChRURUxFUE9SVF9H",
            "VUlMRF9IT1VTRRABEhoKFlRFTEVQT1JUX0dVSUxEX1BBRERPQ0sQAhISCg5P",
            "QkpFQ1RfTUlNSUNSWRADEhwKGExFR0VOREFSWV9UUkVBU1VSRV9RVUVTVBAE",
            "EhIKDlRFTEVQT1JUX0hPVVNFEAVCDQoLX29iamVjdF91aWRCD1oNcHJvdG9j",
            "b2wvZ2FtZWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent), global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Parser, new[]{ "Type", "ObjectUid" }, new[]{ "ObjectUid" }, new[]{ typeof(global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class ClientUIOpenedEvent : pb::IMessage<ClientUIOpenedEvent>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ClientUIOpenedEvent> _parser = new pb::MessageParser<ClientUIOpenedEvent>(() => new ClientUIOpenedEvent());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ClientUIOpenedEvent> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.UiReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ClientUIOpenedEvent() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ClientUIOpenedEvent(ClientUIOpenedEvent other) : this() {
      _hasBits0 = other._hasBits0;
      type_ = other.type_;
      objectUid_ = other.objectUid_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ClientUIOpenedEvent Clone() {
      return new ClientUIOpenedEvent(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType type_ = global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType.Undefined;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "object_uid" field.</summary>
    public const int ObjectUidFieldNumber = 2;
    private readonly static int ObjectUidDefaultValue = 0;

    private int objectUid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int ObjectUid {
      get { if ((_hasBits0 & 1) != 0) { return objectUid_; } else { return ObjectUidDefaultValue; } }
      set {
        _hasBits0 |= 1;
        objectUid_ = value;
      }
    }
    /// <summary>Gets whether the "object_uid" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasObjectUid {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "object_uid" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearObjectUid() {
      _hasBits0 &= ~1;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ClientUIOpenedEvent);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ClientUIOpenedEvent other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Type != other.Type) return false;
      if (ObjectUid != other.ObjectUid) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Type != global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType.Undefined) hash ^= Type.GetHashCode();
      if (HasObjectUid) hash ^= ObjectUid.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Type != global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType.Undefined) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (HasObjectUid) {
        output.WriteRawTag(16);
        output.WriteInt32(ObjectUid);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Type != global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType.Undefined) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (HasObjectUid) {
        output.WriteRawTag(16);
        output.WriteInt32(ObjectUid);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Type != global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType.Undefined) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (HasObjectUid) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ObjectUid);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ClientUIOpenedEvent other) {
      if (other == null) {
        return;
      }
      if (other.Type != global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType.Undefined) {
        Type = other.Type;
      }
      if (other.HasObjectUid) {
        ObjectUid = other.ObjectUid;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        // Abort on any end group tag.
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Type = (global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType) input.ReadEnum();
            break;
          }
          case 16: {
            ObjectUid = input.ReadInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        // Abort on any end group tag.
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Type = (global::Com.Ankama.Dofus.Server.Game.Protocol.Ui.ClientUIOpenedEvent.Types.UIType) input.ReadEnum();
            break;
          }
          case 16: {
            ObjectUid = input.ReadInt32();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the ClientUIOpenedEvent message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public enum UIType {
        [pbr::OriginalName("UNDEFINED")] Undefined = 0,
        [pbr::OriginalName("TELEPORT_GUILD_HOUSE")] TeleportGuildHouse = 1,
        [pbr::OriginalName("TELEPORT_GUILD_PADDOCK")] TeleportGuildPaddock = 2,
        [pbr::OriginalName("OBJECT_MIMICRY")] ObjectMimicry = 3,
        [pbr::OriginalName("LEGENDARY_TREASURE_QUEST")] LegendaryTreasureQuest = 4,
        [pbr::OriginalName("TELEPORT_HOUSE")] TeleportHouse = 5,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code

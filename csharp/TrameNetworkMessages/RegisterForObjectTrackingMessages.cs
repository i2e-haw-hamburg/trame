//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: i2enetworkmessagedefinitions/ObjectTracking/RegisterForObjectTrackingMessages.proto
namespace NetworkMessages.ObjectTracking
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RegisterForObjectTrackingMessages")]
  public partial class RegisterForObjectTrackingMessages : global::ProtoBuf.IExtensible
  {
    public RegisterForObjectTrackingMessages() {}
    
    private string _listenerip;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"listenerip", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string listenerip
    {
      get { return _listenerip; }
      set { _listenerip = value; }
    }
    private int _listenerport;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"listenerport", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int listenerport
    {
      get { return _listenerport; }
      set { _listenerport = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}
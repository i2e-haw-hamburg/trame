//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: i2enetworkmessagedefinitions/GestureInterface/UserUpdateMessage.proto
namespace NetworkMessages
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UserUpdateMessage")]
  public partial class UserUpdateMessage : global::ProtoBuf.IExtensible
  {
    public UserUpdateMessage() {}
    
    private long _userId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"userId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long userId
    {
      get { return _userId; }
      set { _userId = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _bodyPartId = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(2, Name=@"bodyPartId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<int> bodyPartId
    {
      get { return _bodyPartId; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartPositionX = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(4, Name=@"bodyPartPositionX", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartPositionX
    {
      get { return _bodyPartPositionX; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartPositionY = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(5, Name=@"bodyPartPositionY", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartPositionY
    {
      get { return _bodyPartPositionY; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartPositionZ = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(6, Name=@"bodyPartPositionZ", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartPositionZ
    {
      get { return _bodyPartPositionZ; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartRotationX = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(7, Name=@"bodyPartRotationX", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartRotationX
    {
      get { return _bodyPartRotationX; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartRotationY = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(8, Name=@"bodyPartRotationY", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartRotationY
    {
      get { return _bodyPartRotationY; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartRotationZ = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(9, Name=@"bodyPartRotationZ", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartRotationZ
    {
      get { return _bodyPartRotationZ; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartVelocityX = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(10, Name=@"bodyPartVelocityX", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartVelocityX
    {
      get { return _bodyPartVelocityX; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartVelocityY = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(11, Name=@"bodyPartVelocityY", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartVelocityY
    {
      get { return _bodyPartVelocityY; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartVelocityZ = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(12, Name=@"bodyPartVelocityZ", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartVelocityZ
    {
      get { return _bodyPartVelocityZ; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartAngularVelocityX = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(13, Name=@"bodyPartAngularVelocityX", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartAngularVelocityX
    {
      get { return _bodyPartAngularVelocityX; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartAngularVelocityY = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(14, Name=@"bodyPartAngularVelocityY", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartAngularVelocityY
    {
      get { return _bodyPartAngularVelocityY; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartAngularVelocityZ = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(15, Name=@"bodyPartAngularVelocityZ", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartAngularVelocityZ
    {
      get { return _bodyPartAngularVelocityZ; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartAngularAccelerationX = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(16, Name=@"bodyPartAngularAccelerationX", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartAngularAccelerationX
    {
      get { return _bodyPartAngularAccelerationX; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartAngularAccelerationY = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(17, Name=@"bodyPartAngularAccelerationY", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartAngularAccelerationY
    {
      get { return _bodyPartAngularAccelerationY; }
    }
  
    private readonly global::System.Collections.Generic.List<float> _bodyPartAngularAccelerationZ = new global::System.Collections.Generic.List<float>();
    [global::ProtoBuf.ProtoMember(18, Name=@"bodyPartAngularAccelerationZ", DataFormat = global::ProtoBuf.DataFormat.FixedSize, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<float> bodyPartAngularAccelerationZ
    {
      get { return _bodyPartAngularAccelerationZ; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}
using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;

class PacketManager
{
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance { get { return _instance; } }
	#endregion

	PacketManager()
	{
		Register();
	}

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();
		
	public Action<PacketSession, IMessage, ushort> CustomHandler { get; set; }

	public void Register()
	{		
		_onRecv.Add((ushort)MsgId.CEnterGame, MakePacket<C_EnterGame>);
		_handler.Add((ushort)MsgId.CEnterGame, PacketHandler.C_EnterGameHandler);		
		_onRecv.Add((ushort)MsgId.CLeaveGame, MakePacket<C_LeaveGame>);
		_handler.Add((ushort)MsgId.CLeaveGame, PacketHandler.C_LeaveGameHandler);		
		_onRecv.Add((ushort)MsgId.CSpawn, MakePacket<C_Spawn>);
		_handler.Add((ushort)MsgId.CSpawn, PacketHandler.C_SpawnHandler);		
		_onRecv.Add((ushort)MsgId.CDespawn, MakePacket<C_Despawn>);
		_handler.Add((ushort)MsgId.CDespawn, PacketHandler.C_DespawnHandler);		
		_onRecv.Add((ushort)MsgId.CMove, MakePacket<C_Move>);
		_handler.Add((ushort)MsgId.CMove, PacketHandler.C_MoveHandler);		
		_onRecv.Add((ushort)MsgId.CChat, MakePacket<C_Chat>);
		_handler.Add((ushort)MsgId.CChat, PacketHandler.C_ChatHandler);		
		_onRecv.Add((ushort)MsgId.CLeaveScene, MakePacket<C_LeaveScene>);
		_handler.Add((ushort)MsgId.CLeaveScene, PacketHandler.C_LeaveSceneHandler);		
		_onRecv.Add((ushort)MsgId.CEnterScene, MakePacket<C_EnterScene>);
		_handler.Add((ushort)MsgId.CEnterScene, PacketHandler.C_EnterSceneHandler);		
		_onRecv.Add((ushort)MsgId.CLogin, MakePacket<C_Login>);
		_handler.Add((ushort)MsgId.CLogin, PacketHandler.C_LoginHandler);		
		_onRecv.Add((ushort)MsgId.CSignUp, MakePacket<C_SignUp>);
		_handler.Add((ushort)MsgId.CSignUp, PacketHandler.C_SignUpHandler);		
		_onRecv.Add((ushort)MsgId.CLoginCheck, MakePacket<C_LoginCheck>);
		_handler.Add((ushort)MsgId.CLoginCheck, PacketHandler.C_LoginCheckHandler);		
		_onRecv.Add((ushort)MsgId.CFriendCheck, MakePacket<C_FriendCheck>);
		_handler.Add((ushort)MsgId.CFriendCheck, PacketHandler.C_FriendCheckHandler);		
		_onRecv.Add((ushort)MsgId.CDirectChat, MakePacket<C_DirectChat>);
		_handler.Add((ushort)MsgId.CDirectChat, PacketHandler.C_DirectChatHandler);		
		_onRecv.Add((ushort)MsgId.CAddFriend, MakePacket<C_AddFriend>);
		_handler.Add((ushort)MsgId.CAddFriend, PacketHandler.C_AddFriendHandler);		
		_onRecv.Add((ushort)MsgId.CUserCheck, MakePacket<C_UserCheck>);
		_handler.Add((ushort)MsgId.CUserCheck, PacketHandler.C_UserCheckHandler);		
		_onRecv.Add((ushort)MsgId.CStartminigame, MakePacket<C_Startminigame>);
		_handler.Add((ushort)MsgId.CStartminigame, PacketHandler.C_StartminigameHandler);		
		_onRecv.Add((ushort)MsgId.CFinishminigame, MakePacket<C_Finishminigame>);
		_handler.Add((ushort)MsgId.CFinishminigame, PacketHandler.C_FinishminigameHandler);
	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Action<PacketSession, ArraySegment<byte>, ushort> action = null;
		if (_onRecv.TryGetValue(id, out action))
			action.Invoke(session, buffer, id);
	}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
	{
		T pkt = new T();
		pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

		if (CustomHandler != null)
		{
			CustomHandler.Invoke(session, pkt, id);
		}
		else
		{
			Action<PacketSession, IMessage> action = null;
			if (_handler.TryGetValue(id, out action))
				action.Invoke(session, pkt);
		}
	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		Action<PacketSession, IMessage> action = null;
		if (_handler.TryGetValue(id, out action))
			return action;
		return null;
	}
}
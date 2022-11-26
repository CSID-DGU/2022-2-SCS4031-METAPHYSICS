using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using Server.Game;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
	public static void C_EnterGameHandler(PacketSession session, IMessage packet)
	{
		C_EnterGame enterPacket = packet as C_EnterGame;
		ClientSession clientSession = session as ClientSession;

		clientSession.MyPlayer = PlayerManager.Instance.Add();
		{
			//내용 집어넣기
			clientSession.MyPlayer.Info.ColorIndex = enterPacket.Player.ColorIndex;
			clientSession.MyPlayer.Info.UserName = enterPacket.Player.UserName;
			clientSession.MyPlayer.Info.UserPrivilege = enterPacket.Player.UserPrivilege;
			clientSession.MyPlayer.Info.Scene = enterPacket.Player.Scene;
			clientSession.MyPlayer.Session = clientSession;
		}

		GameRoom room = RoomManager.Instance.Find(1);
		room.Push(room.EnterGame, clientSession.MyPlayer);
	}

	public static void C_LeaveGameHandler(PacketSession session, IMessage packet)
	{

	}

	public static void C_SpawnHandler(PacketSession session, IMessage packet)
	{

	}

	public static void C_DespawnHandler(PacketSession session, IMessage packet)
	{

	}

	public static void C_MoveHandler(PacketSession session, IMessage packet)
	{
		C_Move movePacket = packet as C_Move;
		ClientSession clientSession = session as ClientSession;

		//애니메이션 번호 출력
		//Console.WriteLine($"C_Move({movePacket.PosInfo.Movedir})");

		Player player = clientSession.MyPlayer;
		if (player == null)
			return;

		GameRoom room = player.Room;
		if (room == null)
			return;

		room.Push(room.HandleMove, player, movePacket);
	}

	public static void C_ChatHandler(PacketSession session, IMessage packet)
	{
		C_Chat chatPacket = packet as C_Chat;
		ClientSession clientSession = session as ClientSession;

		Player player = clientSession.MyPlayer;
		if (player == null)
			return;

		//Console.WriteLine($"C_Chat({chatPacket.ChatInfo.ChattingText})");

		GameRoom room = player.Room;
		if (room == null)
			return;

		room.Push(room.HandleChat, player, chatPacket);
	}

	public static void C_EnterSceneHandler(PacketSession session, IMessage packet)
	{
		C_EnterScene enterPacket = packet as C_EnterScene;
		ClientSession clientSession = session as ClientSession;

		clientSession.MyPlayer = PlayerManager.Instance.Find(enterPacket.Player.PlayerId);
		{
			//내용 집어넣기
			clientSession.MyPlayer.Info.ColorIndex = enterPacket.Player.ColorIndex;
			clientSession.MyPlayer.Info.UserName = enterPacket.Player.UserName;
			clientSession.MyPlayer.Info.UserPrivilege = enterPacket.Player.UserPrivilege;
			clientSession.MyPlayer.Info.Scene = enterPacket.Player.Scene;
			clientSession.MyPlayer.Session = clientSession;
		}
		Player player = clientSession.MyPlayer;
		if (player == null)
			return;

		GameRoom room = player.Room;
		if (room == null)
			return;

		room.Push(room.EnterScene, clientSession.MyPlayer);
	}

	public static void C_LeaveSceneHandler(PacketSession session, IMessage packet)
	{
		C_LeaveScene leavePacket = packet as C_LeaveScene;
		ClientSession clientSession = session as ClientSession;

		Player player = clientSession.MyPlayer;
		if (player == null)
			return;

		GameRoom room = player.Room;
		if (room == null)
			return;

		room.Push(room.LeaveScene, leavePacket);
		Console.WriteLine($"C_LeaveSceneHandler : {leavePacket.Player.Scene}");
	}

}
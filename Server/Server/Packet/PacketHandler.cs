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
	public static void C_MoveHandler(PacketSession session, IMessage packet)
	{
		C_Move movePacket = packet as C_Move;
		ClientSession clientSession = session as ClientSession;

		//애니메이션 번호 출력
		Console.WriteLine($"C_Move({movePacket.PosInfo.Movedir})");

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

	public static void C_EnterGameHandler(PacketSession session, IMessage packet)
	{
		C_EnterGame enterPacket = packet as C_EnterGame;
		ClientSession clientSession = session as ClientSession;

		//Console.WriteLine($"C_EnterGame({enterPacket.Player.UserName})");
		//Console.WriteLine($"C_EnterGame({enterPacket.Player.ColorIndex})");

		clientSession.MyPlayer = PlayerManager.Instance.Add();
		{
			//내용 집어넣기
			clientSession.MyPlayer.Info.PosInfo.PosX = 0.0f;
			clientSession.MyPlayer.Info.PosInfo.PosY = 0.0f;
			clientSession.MyPlayer.Info.PosInfo.MovedirX = 0.0f;
			clientSession.MyPlayer.Info.PosInfo.MovedirY = 0.0f;
			clientSession.MyPlayer.Info.ColorIndex = enterPacket.Player.ColorIndex;
			clientSession.MyPlayer.Info.UserName = enterPacket.Player.UserName;
			clientSession.MyPlayer.Session = clientSession;
		}

		//Console.WriteLine($"C_EnterGame({clientSession.MyPlayer.Info.PosInfo.PosX})");
		//Console.WriteLine($"C_EnterGame({clientSession.MyPlayer.Info.PosInfo.PosY})");
		//Console.WriteLine($"C_EnterGame({clientSession.MyPlayer.Info.PosInfo.MovedirX})");
		//Console.WriteLine($"C_EnterGame({clientSession.MyPlayer.Info.PosInfo.MovedirY})");
		//Console.WriteLine($"C_EnterGame({clientSession.MyPlayer.Info.UserName})");
		//Console.WriteLine($"C_EnterGame({clientSession.MyPlayer.Info.ColorIndex})");
		//Console.WriteLine($"C_EnterGame({clientSession.MyPlayer.Info.PlayerId})");

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
}
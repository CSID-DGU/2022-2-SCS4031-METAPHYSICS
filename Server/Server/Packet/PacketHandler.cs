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

        //Console.WriteLine($"C_Move({movePacket.PosInfo.PosX}, {movePacket.PosInfo.PosY})");

		Player player = clientSession.MyPlayer;
		if (player == null)
			return;

		GameRoom room = player.Room;
		if (room == null)
			return;

		room.HandleMove(player, movePacket);
	}

	public static void C_ChatHandler(PacketSession session, IMessage packet)
    {
		C_Chat chatPacket = packet as C_Chat;
		ClientSession clientSession = session as ClientSession;

		Player player = clientSession.MyPlayer;
		if (player == null)
			return;

		Console.WriteLine($"C_Chat({chatPacket.ChatInfo.ChattingText})");

		GameRoom room = player.Room;
		if (room == null)
			return;

		room.HandleChat(player, chatPacket);
	}
}
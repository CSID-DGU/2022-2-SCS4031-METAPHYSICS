﻿using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PacketHandler
{
	public static void S_EnterGameHandler(PacketSession session, IMessage packet)
	{
		S_EnterGame enterGamePacket = packet as S_EnterGame;
		Managers.Object.Add(enterGamePacket.Player, myPlayer: true);
		Debug.Log("S_EnterGameHandler");
	}

	public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
	{
		S_LeaveGame leaveGameHandler = packet as S_LeaveGame;
		Managers.Object.RemoveMyPlayer();
	}


	public static void S_SpawnHandler(PacketSession session, IMessage packet)
	{
        S_Spawn spawnPacket = packet as S_Spawn;
        foreach (PlayerInfo player in spawnPacket.Players)
        {
            Managers.Object.Add(player, myPlayer: false);
        }
        //Debug.Log("S_SpawnHandler");
    }

	public static void S_DespawnHandler(PacketSession session, IMessage packet)
	{
		S_Despawn despawnPacket = packet as S_Despawn;
		foreach (int id in despawnPacket.PlayerIds)
		{
			Managers.Object.Remove(id);
		}
		//Debug.Log("S_DespawnHandler");
	}

	public static void S_MoveHandler(PacketSession session, IMessage packet)
	{
		S_Move movePacket = packet as S_Move;
		ServerSession serverSession = session as ServerSession;

		//있는지 없는지 찾아줌
		GameObject go = Managers.Object.FindById(movePacket.PlayerId);
		if (go == null)
			return;

		UserControllerScript cc = go.GetComponent<UserControllerScript>();
		if (cc == null)
			return;

		//서버거를 클라에 붙여줌
		cc.PosInfo = movePacket.PosInfo;
	}

	public static void S_ChatHandler(PacketSession session, IMessage packet)
	{
		S_Chat chatPacket = packet as S_Chat;
		ServerSession serverSession = session as ServerSession;

		//있는지 없는지 찾아줌
		GameObject go = Managers.Object.FindById(chatPacket.PlayerId);
		if (go == null)
			return;

		ChatManager cm = go.GetComponent<ChatManager>();
		if (cm == null)
			return;

		//cm.Chat_Info = chatPacket.ChatInfo;
		cm.PushGlobalChat(chatPacket.ChatInfo.UserName, chatPacket.ChatInfo.ChattingText);
	}
}



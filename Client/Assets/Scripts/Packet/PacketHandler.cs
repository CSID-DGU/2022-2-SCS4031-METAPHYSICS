using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Struct;

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
		//보낸 사람의 id, 채팅정보(username, chatting text)
		S_Chat chatPacket = packet as S_Chat;
		ServerSession serverSession = session as ServerSession;

		//내 클라의 매니저 받아오기
		ChatManager cm = Managers.Chat;
		if (cm == null)
			return;

		//보낸 캐릭터의 이름이 나와 다르면 패킷 받기
		if(cm.UserName!= chatPacket.ChatInfo.UserName)
        {
			cm.PushGlobalChat(chatPacket.ChatInfo.UserName, chatPacket.ChatInfo.ChattingText);
        }
	}
}



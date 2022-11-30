﻿using Google.Protobuf;
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
		//Debug.Log("S_EnterGameHandler");
	}

	public static void S_EnterSceneHandler(PacketSession session, IMessage packet)
	{
		S_EnterScene enterScenePacket = packet as S_EnterScene;
		// 전의 id 오브젝트를 삭제
		//Managers.Object.Remove(enterScenePacket.Player.PlayerId);
		// 같은 id 오브젝트를 추가
		Managers.Object.Add(enterScenePacket.Player, myPlayer: true);
		//Debug.Log("S_EnterGameHandler");
	}

	public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
	{
		S_LeaveGame leaveGameHandler = packet as S_LeaveGame;
		Managers.Object.Clear();
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

	public static void S_DirectChatHandler(PacketSession session, IMessage packet)
	{
		S_DirectChat directChatPacket = packet as S_DirectChat;
		DirectMessageStruct MessageData = new DirectMessageStruct();
		MessageData.ChattingText = directChatPacket.ChattingText;
		MessageData.ReceiverUser = directChatPacket.Receiver;
		MessageData.SenderUser = directChatPacket.Sender;
		Managers.Data.m_FriendManager.ReceiveDirectMessage(MessageData);
	}

	public static void S_ConnectedHandler(PacketSession session, IMessage packet)
	{
		Debug.Log("S_ConnectedHandler");
		C_Login loginPacket = new C_Login();
		loginPacket.UniqueId = SystemInfo.deviceUniqueIdentifier; // 랜덤 아이디 생성기
		Managers.Network.Send(loginPacket);
	}

	public static void S_LoginHandler(PacketSession session, IMessage packet)
	{
		S_Login loginPacket = packet as S_Login;
		//Debug.Log($"LoginOK({loginPacket.LoginOk}");
		if(loginPacket.LoginOk == 0)
        {
			Managers.Data.prevUser = true;
			Managers.Data.userId = loginPacket.AccountId;
			Managers.Data.userPassword = loginPacket.AccountPassword;
			Managers.Data.userName = loginPacket.AccountName;
		}
        else
        {
			Managers.Data.prevUser = false;
		}
	}

	public static void S_FriendCheckHandler(PacketSession session, IMessage packet)
	{
		S_FriendCheck friendPacket = packet as S_FriendCheck;
		Managers.Data.m_FriendManager.SetFriendList(friendPacket.FriendList);
	}

	public static void S_AddFriend(PacketSession session, IMessage packet)
	{
		S_AddFriend friendPacket = packet as S_AddFriend;
		Managers.Data.m_FriendManager.SetFriendList(friendPacket.FriendList);
	}
}



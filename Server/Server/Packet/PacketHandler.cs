using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using Server.DB;
using Server.Game;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

	public static void C_DirectChatHandler(PacketSession session, IMessage packet)
	{
		C_DirectChat checkPacket = packet as C_DirectChat;
		ClientSession clientSession = session as ClientSession;

		Console.WriteLine($"C_DirectChatHandler : {checkPacket.Receiver}");
		S_DirectChat directChatPacket = new S_DirectChat()
		{
			Sender = checkPacket.Sender,
			Receiver = checkPacket.Receiver,
			ChattingText = checkPacket.ChattingText
		};
		clientSession.Send(directChatPacket);
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

	public static void C_LoginHandler(PacketSession session, IMessage packet)
	{
		C_Login loginpacket = packet as C_Login;
		ClientSession clientSession = session as ClientSession;

		Console.WriteLine($"UniqueId : {loginpacket.UniqueId}");

		using (AppDbContext db = new AppDbContext())
        {
			// 만들어져 있는지 확인
			AccountDb findAccount = db.Accounts
				.Where(a => a.AccountName == loginpacket.UniqueId).FirstOrDefault();

			if(findAccount != null)
            {
				S_Login loginOk = new S_Login() { LoginOk = 1 };
				clientSession.Send(loginOk);
            }
            else
            {
				AccountDb newAccount = new AccountDb() { AccountName = loginpacket.UniqueId };
				db.Accounts.Add(newAccount);
				db.SaveChanges();

				S_Login loginOk = new S_Login() { LoginOk = 1 };
				clientSession.Send(loginOk);
            }
        }
	}

	public static void C_SignUpHandler(PacketSession session, IMessage packet)
	{
		C_SignUp signUpPacket = packet as C_SignUp;
		ClientSession clientSession = session as ClientSession;

		Console.WriteLine($"C_SignUpHandler : {signUpPacket.AccountName}");

		using (AppDbContext db = new AppDbContext())
		{
			AccountDb newAccount = new AccountDb() { AccountId = signUpPacket.AccountId,
			AccountName = signUpPacket.AccountName, AccountPassword = signUpPacket.AccountPassword };
			db.Accounts.Add(newAccount);
			db.SaveChanges();
			Console.WriteLine($"AccountId : {signUpPacket.AccountId}");
			Console.WriteLine($"AccountName : {signUpPacket.AccountName}");
			Console.WriteLine($"AccountPassword : {signUpPacket.AccountPassword}");
		}
	}

	public static void C_LoginCheckHandler(PacketSession session, IMessage packet)
	{
		C_LoginCheck loginCheckpacket = packet as C_LoginCheck;
		ClientSession clientSession = session as ClientSession;

		using (AppDbContext db = new AppDbContext())
		{
			// 만들어져 있는지 확인
			AccountDb findAccount = db.Accounts
				.Where(a => a.AccountId == loginCheckpacket.AccountId).FirstOrDefault();

			Console.WriteLine($"C_LoginCheckHandler : {loginCheckpacket.AccountId}");
			// 있으면
			if (findAccount != null)
			{
				S_Login loginOk = new S_Login() { LoginOk = 0, AccountId = findAccount.AccountId,
				AccountName = findAccount.AccountName, AccountPassword = findAccount.AccountPassword};
				clientSession.Send(loginOk);
				Console.WriteLine($"AccountId : {findAccount.AccountId}");
				Console.WriteLine($"AccountName : {findAccount.AccountName}");
				Console.WriteLine($"AccountPassword : {findAccount.AccountPassword}");
			}
            else
            {
				S_Login loginOk = new S_Login() { LoginOk = 1 };
				clientSession.Send(loginOk);
			}
		}
	}

	public static void C_FriendCheckHandler(PacketSession session, IMessage packet)
	{
		C_FriendCheck checkPacket = packet as C_FriendCheck;
		ClientSession clientSession = session as ClientSession;

		using (AppDbContext db = new AppDbContext())
		{
			// 만들어져 있는지 확인
			AccountDb findAccount = db.Accounts
				.Where(a => a.AccountName == checkPacket.AccountName).FirstOrDefault();

			Console.WriteLine($"C_FriendCheckHandler : {checkPacket.AccountName}");
			S_FriendCheck friendPacket = new S_FriendCheck()
			{
				FriendList = findAccount.FriendList
			};
			clientSession.Send(friendPacket);
		}
	}
}
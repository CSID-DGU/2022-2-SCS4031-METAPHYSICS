using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game
{
	public class GameRoom : JobSerializer
	{
		//object _lock = new object();
		public int RoomId { get; set; }

		List<Player> _players = new List<Player>();

		public int highScore = 0;
		public string highScoreName = "";
		int minigamePlayerCount = 0;

		public void EnterGame(Player newPlayer)
		{
			if (newPlayer == null)
				return;

			_players.Add(newPlayer);
			newPlayer.Room = this;


			// 본인한테 정보 전송
			{
				S_EnterGame enterPacket = new S_EnterGame();
				enterPacket.Player = newPlayer.Info;
				//Console.WriteLine($"S_EnterGame({enterPacket.Player.UserPrivilege})");
				newPlayer.Session.Send(enterPacket);

				//다른사람들 불러오기
				S_Spawn spawnPacket = new S_Spawn();
				foreach (Player p in _players)
				{
					if (newPlayer != p)
                    {
						if(p.Info.Scene == newPlayer.Info.Scene)
							spawnPacket.Players.Add(p.Info);
                    }
				}
				newPlayer.Session.Send(spawnPacket);
			}

			// 타인한테 정보 전송
			{
				S_Spawn spawnPacket = new S_Spawn();
				spawnPacket.Players.Add(newPlayer.Info);
				foreach (Player p in _players)
				{
					if (newPlayer != p)
                    {
						if(p.Info.Scene == newPlayer.Info.Scene)
                        {
							p.Session.Send(spawnPacket);
							Console.WriteLine($"spawnPacket : {p.Info.PlayerId}");
						}
                    }
				}
			}
		}

		// 다른 씬으로 넘어가서, 내 플레이어의 id를 상대 클라에서 spawn시킴
		// 다른 씬으로 넘어가서, 다른 플레이어의 id를 내 클라에 spawn 시킴
		// 
		public void EnterScene(Player Myplayer)
        {
			if (Myplayer == null)
				return;

			_players.Add(Myplayer);
			Myplayer.Room = this;

			// 본인한테 정보 전송
			{
				S_EnterScene SenterScenePacket = new S_EnterScene();
				SenterScenePacket.Player = Myplayer.Info;
				//Console.WriteLine($"S_EnterScene({SenterScenePacket.Player.PlayerId})");
				//Console.WriteLine($"S_EnterScene({SenterScenePacket.Player.UserName})");
				//Console.WriteLine($"S_EnterScene({enterSPacket.Player.ColorIndex})");
				Myplayer.Session.Send(SenterScenePacket);

				//다른사람들 불러오기
				S_Spawn spawnPacket = new S_Spawn();
				foreach (Player p in _players)
				{
					if (Myplayer != p)
					{
						if (p.Info.Scene == Myplayer.Info.Scene)
							spawnPacket.Players.Add(p.Info);
					}
				}
				Myplayer.Session.Send(spawnPacket);
			}

			// 타인한테 정보 전송
			{
				S_Spawn spawnPacket = new S_Spawn();
				spawnPacket.Players.Add(Myplayer.Info);
				foreach (Player p in _players)
				{
					if (Myplayer != p)
					{
						if (p.Info.Scene == Myplayer.Info.Scene)
                        {
							p.Session.Send(spawnPacket);
							Console.WriteLine($"spawnPacket : {p.Info.PlayerId}");
						}
					}
				}
			}
		}

		public void StartMinigame(string Username)
		{
			S_Startminigame startminigamePacket = new S_Startminigame();

			// 만해광장 씬에 있는 나를 제외한 플레이어들에게 패킷 발송
			foreach (Player p in _players)
			{
				if (Username != p.Info.UserName)
				{
					if (p.Info.Scene == "ManhaeGwangjang")
					{
						p.Session.Send(startminigamePacket);
						minigamePlayerCount++;
						Console.WriteLine($"startminigamePacket : {p.Info.UserName}");
					}
				}
			}
		}

		// 최고 기록자 이름, 점수 저장
		public void FinishMinigame(string Username, int score)
		{
			minigamePlayerCount--;
			if (highScore < score)
            {
				highScore = score;
				highScoreName = Username;
            }
			//마지막 사람의 finishminigame 호출시에 다른 모든 사람들에게 발송
			if(minigamePlayerCount==0)
            {
				S_Finishminigame finishminigamePacket = new S_Finishminigame();
				finishminigamePacket.UserName = highScoreName;
				finishminigamePacket.Score = highScore;
				foreach (Player p in _players)
				{
					p.Session.Send(finishminigamePacket);
				}
			}
		}

		// 다른 씬으로 넘어갈때, 내 플레이어의 id를 상대 클라에서 despawn시킴
		public void LeaveScene(C_LeaveScene leaveScenePacket)
		{
			_players.Find(p => p.Info.PlayerId == leaveScenePacket.Player.PlayerId).Info.Scene = leaveScenePacket.Player.Scene;
			Player player = _players.Find(p => p.Info.PlayerId == leaveScenePacket.Player.PlayerId);
			if (player == null)
				return;

			_players.Remove(player);

			//player.Room = null;
			// 타인한테 정보 전송
			{
				//Console.WriteLine($"OnDisconnected : {player.Info.PlayerId}");
				S_Despawn despawnPacket = new S_Despawn();
				despawnPacket.PlayerIds.Add(player.Info.PlayerId);
				foreach (Player p in _players)
				{
					if (player != p)
                    {
						if(p.Info.Scene != player.Info.Scene)
                        {
							p.Session.Send(despawnPacket);
							Console.WriteLine($"despawnPacket : {p.Info.PlayerId}");
						}
                    }
				}
			}
		}

		public void LeaveGame(int playerId)
		{
			Player player = _players.Find(p => p.Info.PlayerId == playerId);
			if (player == null)
				return;

			_players.Remove(player);
			player.Room = null;

			// 본인한테 정보 전송
			{
				//Console.WriteLine($"OnDisconnected : {player.Info.PlayerId}");
				S_LeaveGame leavePacket = new S_LeaveGame();
				player.Session.Send(leavePacket);
			}

			// 타인한테 정보 전송
			{
				//Console.WriteLine($"OnDisconnected : {player.Info.PlayerId}");
				S_Despawn despawnPacket = new S_Despawn();
				despawnPacket.PlayerIds.Add(player.Info.PlayerId);
				foreach (Player p in _players)
				{
					if (player != p)
						p.Session.Send(despawnPacket);
				}
			}
		}

		public void HandleLeave(int playerId)
		{
			Player player = _players.Find(p => p.Info.PlayerId == playerId);
			if (player == null)
				return;

			//_players.Remove(player);
			//player.Room = null;

			// 본인한테 정보 전송
			{
				//Console.WriteLine($"OnDisconnected : {player.Info.PlayerId}");
				S_LeaveGame leavePacket = new S_LeaveGame();
				player.Session.Send(leavePacket);
			}

			// 타인한테 정보 전송
			{
				//Console.WriteLine($"OnDisconnected : {player.Info.PlayerId}");
				S_Despawn despawnPacket = new S_Despawn();
				despawnPacket.PlayerIds.Add(player.Info.PlayerId);
				foreach (Player p in _players)
				{
					if (player != p)
						p.Session.Send(despawnPacket);
				}
			}
		}

		public void HandleMove(Player player, C_Move movePacket)
		{
			if (player == null)
				return;

			// 일단 서버에서 좌표 이동
			PlayerInfo info = player.Info;
			info.PosInfo = movePacket.PosInfo;

			// 다른 플레이어한테도 알려준다
			S_Move resMovePacket = new S_Move();
			resMovePacket.PlayerId = player.Info.PlayerId;
			resMovePacket.PosInfo = movePacket.PosInfo;

			Broadcast(resMovePacket);
		}

		public void HandleChat(Player player, C_Chat chatPacket)
		{
			if (player == null)
				return;

			// 다른 플레이어한테 전해주기
			S_Chat resChatPacket = new S_Chat();
			resChatPacket.PlayerId = player.Info.PlayerId;
			resChatPacket.ChatInfo = chatPacket.ChatInfo;

			Broadcast(resChatPacket);
		}

		//public void HandleEnter(C_EnterGame enterPacket)
		//{
		//	S_EnterGame resEnterPacket = new S_EnterGame();
		//	//resEnterPacket.PlayerId = player.Info.PlayerId;
		//	resEnterPacket.Player.UserName = enterPacket.Player.UserName;
		//	resEnterPacket.Player.ColorIndex = enterPacket.Player.ColorIndex;
		//	resEnterPacket.Player.PosInfo.PosX = enterPacket.Player.PosInfo.PosX;
		//	resEnterPacket.Player.PosInfo.PosY = enterPacket.Player.PosInfo.PosY;
		//	resEnterPacket.Player.PosInfo.MovedirX = enterPacket.Player.PosInfo.MovedirX;
		//	resEnterPacket.Player.PosInfo.MovedirY = enterPacket.Player.PosInfo.MovedirY;

		//	Broadcast(resEnterPacket);
		//}

		//방의 모든 플레이어들에게 send함
		public void Broadcast(IMessage packet)
		{
			foreach (Player p in _players)
			{
				p.Session.Send(packet);
			}
		}
	}
}

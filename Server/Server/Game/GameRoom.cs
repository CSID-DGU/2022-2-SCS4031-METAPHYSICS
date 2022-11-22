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
				//Console.WriteLine($"S_EnterGame({enterPacket.Player.ColorIndex})");
				newPlayer.Session.Send(enterPacket);

				//다른사람들 불러오기
				S_Spawn spawnPacket = new S_Spawn();
				foreach (Player p in _players)
				{
					if (newPlayer != p)
						spawnPacket.Players.Add(p.Info);
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
						p.Session.Send(spawnPacket);
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

		public void HandleEnter(C_EnterGame enterPacket)
		{
			S_EnterGame resEnterPacket = new S_EnterGame();
			//resEnterPacket.PlayerId = player.Info.PlayerId;
			resEnterPacket.Player.UserName = enterPacket.Player.UserName;
			resEnterPacket.Player.ColorIndex = enterPacket.Player.ColorIndex;
			resEnterPacket.Player.PosInfo.PosX = enterPacket.Player.PosInfo.PosX;
			resEnterPacket.Player.PosInfo.PosY = enterPacket.Player.PosInfo.PosY;
			resEnterPacket.Player.PosInfo.MovedirX = enterPacket.Player.PosInfo.MovedirX;
			resEnterPacket.Player.PosInfo.MovedirY = enterPacket.Player.PosInfo.MovedirY;

			Broadcast(resEnterPacket);
		}

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

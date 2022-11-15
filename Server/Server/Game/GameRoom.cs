using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game
{
	public class GameRoom
	{
		object _lock = new object();
		public int RoomId { get; set; }

		List<Player> _players = new List<Player>();

		public void EnterGame(Player newPlayer)
		{
			if (newPlayer == null)
				return;

			lock (_lock)
			{
				_players.Add(newPlayer);
				newPlayer.Room = this;

				// 본인한테 정보 전송
				{
					S_EnterGame enterPacket = new S_EnterGame();
					enterPacket.Player = newPlayer.Info;
					newPlayer.Session.Send(enterPacket);

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
		}

		public void LeaveGame(int playerId)
		{
			lock (_lock)
			{
				Player player = _players.Find(p => p.Info.PlayerId == playerId);
				if (player == null)
					return;

				_players.Remove(player);
				player.Room = null;

				// 본인한테 정보 전송
				{
					S_LeaveGame leavePacket = new S_LeaveGame();
					player.Session.Send(leavePacket);
				}

				// 타인한테 정보 전송
				{
					S_Despawn despawnPacket = new S_Despawn();
					despawnPacket.PlayerIds.Add(player.Info.PlayerId);
					foreach (Player p in _players)
					{
						if (player != p)
							p.Session.Send(despawnPacket);
					}
				}
			}
		}

		public void HandleMove(Player player, C_Move movePacket)
        {
			if (player == null)
				return;

			lock(_lock)
            {
				// 일단 서버에서 좌표 이동
				PlayerInfo info = player.Info;
				info.PosInfo = movePacket.PosInfo;

				// 다른 플레이어한테도 알려준다
				S_Move resMovePacket = new S_Move();
				resMovePacket.PlayerId = player.Info.PlayerId;
				resMovePacket.PosInfo = movePacket.PosInfo;

				Broadcast(resMovePacket);
			}
		}

		public void HandleChat(Player player, C_Chat chatPacket)
		{
			if (player == null)
				return;

			lock (_lock)
			{
				// 일단 서버에 받기
				ChatInfo info = chatPacket.ChatInfo;

				// 다른 플레이어한테 전해주기
				S_Chat resChatPacket = new S_Chat();
				resChatPacket.ChatInfo.UserName = info.UserName;
				resChatPacket.ChatInfo.ChattingText = info.ChattingText;

				Broadcast(resChatPacket);
			}
		}

		//방의 모든 플레이어들에게 send함
		public void Broadcast(IMessage packet)
		{
			lock (_lock)
			{
				foreach (Player p in _players)
				{
					p.Session.Send(packet);
				}
			}
		}
	}
}

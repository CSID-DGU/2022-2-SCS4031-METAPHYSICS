using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using Google.Protobuf.WellKnownTypes;
using Server.DB;
using Server.Game;
using ServerCore;

namespace Server
{
	class Program
	{
		static Listener _listener = new Listener();

		static void FlushRoom()
		{
			JobTimer.Instance.Push(FlushRoom, 250);
		}

		static void Main(string[] args)
		{
			RoomManager.Instance.Add();

			// DB Test
			//using(AppDbContext db = new AppDbContext())
   //         {
			//	db.Accounts.Add(new AccountDb() { AccountName = "TestAccount" });
			//	db.SaveChanges();
   //         }

			// DNS (Domain Name System)
			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			
			// 로컬 버전
			IPAddress ipAddr = IPAddress.Parse("192.168.35.217");

			// AWS 버전
			//IPAddress ipAddr = IPAddress.Parse("172.31.46.213");

			// 최종 아이피주소, 포트번호
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			//패킷받기
			_listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
			Console.WriteLine("Listening...");

			//FlushRoom();
			JobTimer.Instance.Push(FlushRoom);

			while (true)
			{
				JobTimer.Instance.Flush();
			}
		}
	}
}

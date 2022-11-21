using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ObjectManager
{
	public MyPlayerController MyPlayer { get; set; }
	public UserControllerScript OtherPlayer { get; set; }
	Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

	public void Add(PlayerInfo info, bool myPlayer = false)
	{
		if (myPlayer)
		{
			GameObject go = Managers.Resource.Instantiate("User/MyPlayer");

			MyPlayer = go.GetComponent<MyPlayerController>();
			MyPlayer.Id = info.PlayerId;
			MyPlayer.PosInfo = info.PosInfo;
			Managers.Data.SetCurrentUserColor(info.ColorIndex);
			MyPlayer.SetCustomPrivilege(info.UserPrivilege);
			MyPlayer.SyncPos();

			_objects.Add(info.PlayerId, go);
		}
		else
		{
			GameObject go = Managers.Resource.Instantiate("User/ACO");
			//go.name = info.Name;

			OtherPlayer = go.GetComponent<UserControllerScript>();
			OtherPlayer.Id = info.PlayerId;
			OtherPlayer.PosInfo = info.PosInfo;
			OtherPlayer.SetCustomColor(info.ColorIndex);
			OtherPlayer.SetCustomPrivilege(info.UserPrivilege);
			OtherPlayer.SyncPos();

			_objects.Add(info.PlayerId, go);
		}
	}

	public void Add(int id, GameObject go)
	{
		_objects.Add(id, go);
	}

	public void Remove(int id)
	{
		_objects.Remove(id);
	}

	public void RemoveMyPlayer()
	{
		if (MyPlayer == null)
			return;

		Remove(MyPlayer.Id);
		MyPlayer = null;
	}

	public GameObject FindById(int id)
	{
		GameObject go = null;
		_objects.TryGetValue(id, out go);
		return go;
	}

	public GameObject Find(Vector2 cellPos)
	{
		foreach (GameObject obj in _objects.Values)
		{
			UserControllerScript cc = obj.GetComponent<UserControllerScript>();
			if (cc == null)
				continue;

			if (cc.CellPos == cellPos)
				return obj;
		}

		return null;
	}

	public GameObject Find(Func<GameObject, bool> condition)
	{
		foreach (GameObject obj in _objects.Values)
		{
			if (condition.Invoke(obj))
				return obj;
		}

		return null;
	}

	public void Clear()
	{
		_objects.Clear();
	}
}

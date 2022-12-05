using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public static class Struct
{
    public struct UserData
    {
        public string UserNum;
        public string Password;
        public string UserName;
        public UserCustomize UserColor;
    }

    public struct GlobalChatData
    {
        public string UserName;
        public string ChattingText;
    }

    public struct NavData
    {
        GameObject MoveObj;
        List<Vector3Int> PathList;
    }

    public struct PortalData
    {
        public GameScene CurrentScene;
        public GameScene NextSceneType;
    }
    public struct DirectMessageStruct
    {
        public string ChattingText;
        public string SenderUser;
        public string ReceiverUser;
    }

    public struct FriendData
    {
        public string FriendName;
        public List<DirectMessageStruct> DirectMessageList;
    }

    public struct QuizRankData
    {
        public string   UserName;
        public int      CorrectCount;
    }

    public struct QuizData
    {
        public string strQuestion;
        public string strAnswer;
        public OXMark Answer;
    }
}

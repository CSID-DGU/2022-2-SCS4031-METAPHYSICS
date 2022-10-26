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
}

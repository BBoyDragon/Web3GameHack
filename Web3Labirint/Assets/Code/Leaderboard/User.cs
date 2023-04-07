using System;

namespace Code.Leaderboard
{
    [Serializable]
    public class Users
    {
        public User[] scores;
    }

    [Serializable]
    public class User
    {
        public string sub;
        public string username;
        public long score;
    }
}
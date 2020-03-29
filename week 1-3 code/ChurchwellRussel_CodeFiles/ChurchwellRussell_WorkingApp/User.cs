using System;
namespace ChurchwellRussell_WorkingApp
{
    public class User
    {
        private int id;
        private string userName;

        public User(int _id, string _userName)
        {
            id = _id;
            userName = _userName;
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
        }
    }
}

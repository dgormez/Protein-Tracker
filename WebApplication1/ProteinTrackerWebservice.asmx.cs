using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace ProteinTracker
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class ProteinTrackerWebservice : WebService
    {
        public class AuthenticationHeader : SoapHeader
        {
            public string UserName;
            public string Password;
        }

        public AuthenticationHeader Authentication;

        private UserRepository repository = new UserRepository();

        [WebMethod(Description ="Add an amount to the total",EnableSession = true)]
        [SoapHeader("Authentication")]
        public int AddProtein(int amount, int userId)
        {
            if (Authentication == null || Authentication.UserName != "Dav" || Authentication.Password != "666")
                throw new Exception("Bad credentials");

            var user = repository.GetById(userId);
            if (user == null)
                return -1;

            user.Total += amount;
            repository.Save(user);
            return user.Total;
        }

        [WebMethod(EnableSession = true)]
        public int AddUser(string name, int goal)
        {
            var user = new User { Goal = goal, Name = name, Total = 0 };
            repository.Add(user);
            return user.UserId;
        }

        [WebMethod(EnableSession =true)]
        public List<User> ListUser()
        {
            return new List<User>(repository.GetAll());
        }


    }
}

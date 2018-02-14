using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProteinTrackerWebServices
{
    public partial class ProteinTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static int AddProtein(int amount, int userId)
        {
            UserRepository repository = new UserRepository();
            var user = repository.GetById(userId);
            if (user == null)
                return -1;
            user.Total += amount;
            repository.Save(user);
            return user.Total;
        }

        [WebMethod]
        public static int AddUser(string name, int goal)
        {
            UserRepository repository = new UserRepository();
            var user = new User { Goal = goal, Name = name, Total = 0 };
            repository.Add(user);

            return user.UserId;
        }

        [WebMethod]
        public static List<User> ListUsers()
        {
            UserRepository repository = new UserRepository();
            return new List<User>(repository.GetAll());
        }
    }
}
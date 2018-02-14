using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace ProteinTrackerWebServices
{
    [WebService(Namespace = "http://simpleprogrammer.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [ServiceContract(Namespace = "http://simpleprogrammer.com")]
    public interface IProteinTrackerService
    {
        [WebMethod]
        [OperationContract]
        int AddProtein(int amount, int userId);

        [WebMethod]
        [OperationContract]
        int AddUser(string name, int goal);

        [WebMethod]
        [OperationContract]
        List<User> ListUsers();
    }
    
    public class ProteinTrackingService : WebService , IProteinTrackerService
    {
        private UserRepository repository = new UserRepository();

        [WebMethod]
        public int AddProtein(int amount, int userId)
        {
            var user = repository.GetById(userId);
            if (user == null)
                return -1;
            user.Total += amount;
            repository.Save(user);
            return user.Total;
        }

        [WebMethod]
        public int AddUser(string name, int goal)
        {
            var user = new User { Goal = goal, Name = name, Total = 0 };
            repository.Add(user);

            return user.UserId;
        }

        [WebMethod]
        public List<User> ListUsers()
        {
            return new List<User>(repository.GetAll());
        }

    }
}

using DataBlendLibrary.Client;
using DataBlendLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITester
{
    class Program
    {
        static void Main(string[] args)
        {
            //the following fields could be passed in by config file 
            var client = new RestfulAPIClient("412d7892-e7d0-4551-a99a-ace5b6948ce5.mock.pstmn.io", 443,"admin","password",true);
            var content = client.Get(TargetObject.TargetType.users.ToString()).Result;
            var users = JsonConvert.DeserializeObject<List<User>>(content);
            content = client.Get(TargetObject.TargetType.users.ToString(), users[0].id).Result;
            var user = JsonConvert.DeserializeObject<User>(content);
            client.Delete(TargetObject.TargetType.users.ToString(), users[0].id);
            var postResult = client.Post(TargetObject.TargetType.users.ToString(), users[0]).Result;
            var updateResult = client.Put(TargetObject.TargetType.users.ToString(), users[0], users[0].id).Result;
            Console.WriteLine(user);
        }
    }
}

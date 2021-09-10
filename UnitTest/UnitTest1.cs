using System;
using System.Collections.Generic;
using DataBlendLibrary.Client;
using DataBlendLibrary.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        string host = "412d7892-e7d0-4551-a99a-ace5b6948ce5.mock.pstmn.io";
        string password = "password";
        string userName = "admin";
        bool secure = true;
        int port = 443;

        [TestMethod]
        public void TestFullyMethod()
        {

            var client = new RestfulAPIClient(host, port, userName, password,secure);
            var content = client.Get(TargetObject.TargetType.users.ToString()).Result;
            var users = JsonConvert.DeserializeObject<List<User>>(content);
            Assert.IsTrue(users.Count > 0);
            content = client.Get(TargetObject.TargetType.users.ToString(), users[0].id).Result;
            var user = JsonConvert.DeserializeObject<User>(content);
            Assert.IsNotNull(user);
            client.Delete(TargetObject.TargetType.users.ToString(), users[0].id);
            var postResult = client.Post(TargetObject.TargetType.users.ToString(), users[0]).Result;
            Assert.IsTrue(postResult);
            var updateResult = client.Put(TargetObject.TargetType.users.ToString(), users[0], users[0].id).Result;
            Assert.IsTrue(updateResult);
        }
    }
}

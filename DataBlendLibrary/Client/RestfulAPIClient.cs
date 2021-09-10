using Newtonsoft.Json;
using DataBlendLibrary.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataBlendLibrary.Client
{
    public class RestfulAPIClient : ClientBase
    {
        //protected readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public RestfulAPIClient(string host, int port, string username, string password, bool secured)
       : base(host, port, username, password, secured)
        {

        }

        public HttpClient SetBasicAuth()
        {
            HttpClient httpClient = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes($"{Username}:{Password}");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return httpClient;
        }

        public string SetUrl(string path)
        {
            //checking if the site is secure or not
            string protocol = IsSecure ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
            UriBuilder uri = new UriBuilder(protocol, Host, Port, path);
            var query = HttpUtility.ParseQueryString(uri.Query);
            query["Application"] = "json";
            uri.Query = query.ToString();
            return uri.ToString();
        }

        public StringContent ConvertObjecttoJson(object content)
        {
            var json = JsonConvert.SerializeObject(content);
            return  new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<string> Get(string TargetObject, string id = null)
        {
            var doc = "";
            try
            {
                //check for parameter
                var path = id == null ? $"Get/{TargetObject}" : $"Get/{TargetObject}/{id}";

                //_logger.Debug("get data from : " + url);

                var url = SetUrl(path);
                HttpClient httpClient = SetBasicAuth();

                var response = await httpClient.GetAsync(url);
                doc = await response.Content.ReadAsStringAsync();
                //doc = JsonConvert.DeserializeObject<User>(responseBody);
            }
            catch (Exception e)
            {
                doc = null;
                //_logger.Error("Exception when calling get", e);
                throw new Exception("Exception when calling get", e);
            }
            return doc;
        }

        public async Task<bool> Post(string TargetObject, object content)
        {
            try
            {
                var data = ConvertObjecttoJson(content);

                var url = SetUrl($"Post/{TargetObject}");

                //_logger.Debug("Post to : " + url);
                //_logger.Debug("data: " + json);

                HttpClient httpClient = SetBasicAuth();


                var response = await httpClient.PostAsync(url, data);
                return response.EnsureSuccessStatusCode().IsSuccessStatusCode;

            }
            catch (Exception e)
            {
                throw new Exception("Exception when calling post", e);
                //_logger.Error("Exception when calling post", e);
            }
        }

        public async Task<bool> Put(string TargetObject, object content, string id)
        {
            try
            {
                var data = ConvertObjecttoJson(content);

                var url = SetUrl($"Put/{TargetObject}/{id}");
                //_logger.Debug("put data to: " + url);
                //_logger.Debug("Message: " + json);

                HttpClient httpClient = SetBasicAuth();

                var response = await httpClient.PutAsync(url, data);
                return response.EnsureSuccessStatusCode().IsSuccessStatusCode;

            }
            catch (Exception e)
            {
                throw new Exception("Exception when calling put", e);
                //_logger.Error("Exception when calling put ", e);
            }
        }

        public async void Delete(string TargetObject, string id)
        {
            try
            {
                //_logger.Debug("delete data from : " + url);
                var url = SetUrl($"Delete/{TargetObject}/{id}");

                HttpClient httpClient = SetBasicAuth();

                var response = await httpClient.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new Exception("Exception when calling delete", e);
                //_logger.Error("Exception when calling delete", e);
            }
        }
    }
}

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //var client = new RestClient("https://postman-echo.com/get");
            var client = new RestClient("https://datasensedevapimgmt.azure-api.net/auth/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddParameter("client_secret", "8tdawWHanErM8SOArCRBAmyb1zuYFDQnu6lUV+/UvuQ=");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", "b5d901d5-09ed-4b0c-937f-973cd6f13edc");
            
            IRestResponse response = client.Execute(request);
            tokenData tempTokenData = new tokenData();
            tempTokenData  = JsonConvert.DeserializeObject<tokenData>(response.Content);

            client = new RestClient("https://datasensedevedsapi.azurewebsites.net/api/School/All?pageNumber=2&limit=5");
            request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", "4346d75edd1e47799abd1cd723c05517");
            request.AddHeader("Authorization", "Bearer "+tempTokenData.access_token);
            
            response = client.Execute(request);
            Console.Write(response.Content);
            Console.ReadKey();
        }
    }


    class tokenData
    {
        public string token_type;
        public string expires_in;
        public string ext_expires_in;
        public string not_before;
        public string resource;
        public string access_token;
    }
}

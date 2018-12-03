using Inventory.DataLayer.Repository;
using Inventory.Models;
using Inventory.Utils;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Inventory.Server.LUIS
{
    public class LUISAdapter
    {
        public static SearchQueryResponse GetSearchQuery(string searchString)
        {
            string rawJSON = GetRawResponse(searchString);

            if (!String.IsNullOrEmpty(rawJSON))
            {
                SearchQueryResponse resp = new SearchQueryResponse();
                resp.RawJSON = rawJSON;
                LUISResponse luisResp = JsonConvert.DeserializeObject<LUISResponse>(rawJSON);

                return resp;         
            }

            return new SearchQueryResponse();
        }

        private static string GetRawResponse(string searchString)
        {
            string getRequest = "https://eastus.api.cognitive.microsoft.com/luis/v2.0/apps/8fa8db33-de03-4b62-9107-a96a89c567fe?subscription-key=5f7ef840d38e48aa8fb1c3f3f9428ba8&timezoneOffset=-360&q=" + searchString;
            
            WebRequest request = WebRequest.Create(getRequest);
            // Get the response.  
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
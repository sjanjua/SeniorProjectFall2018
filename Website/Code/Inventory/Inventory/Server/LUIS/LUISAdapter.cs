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
                LUISResponse luisResp = JsonConvert.DeserializeObject<LUISResponse>(rawJSON);

                SearchQueryResponse resp = new SearchQueryResponse();
                resp.RawJSON = rawJSON;
                resp.SearchQuery = getSearchQuery(luisResp);

                return resp;
            }

            return new SearchQueryResponse();
        }

        private static string getSearchQuery(LUISResponse luisResp)
        {
            string sql = "";

            if (luisResp != null)
            {
                foreach (var item in luisResp.entities)
                {
                    
                    switch (item.type)
                    {
                        case "builtin.datetimeV2.daterange":
                            if (item.resolution.values.Length > 0)
                            {
                                string endDate = String.IsNullOrEmpty(item.resolution.values[0].end) ? DateTime.Now.ToString("yyyy-MM-dd") : item.resolution.values[0].end;
                                sql = appendString(sql, string.Format("orderdate between '{0}' and '{1}'", item.resolution.values[0].start, endDate));
                            }
                            break;
                        case "company_name":
                            if (!string.IsNullOrEmpty(item.entity))
                                sql = appendString(sql, string.Format("lower(shipname) like '%{0}%'", item.entity));
                            break;
                        case "user":
                            if (!string.IsNullOrEmpty(item.entity))
                                sql = appendString(sql, string.Format("lower(u.firstname) like '%{0}%' or lower(u.lastname) like '%{0}%'", item.entity));
                            break;
                    }
                }
            }

            return sql;
        }

        private static string appendString(string sql, string v)
        {
            if (!String.IsNullOrEmpty(sql))
                sql += " and " + v;
            else
                sql += v;

            return sql;
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
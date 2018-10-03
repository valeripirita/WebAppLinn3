using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using System.Data;
using Newtonsoft.Json.Linq;

namespace WebAppLinn3.Models
{
    public class ApiStatus
    {
        public string ApiToken { get; set; } = "please-provide-api-token";
        public string ApiServer { get; set; } = "https://eu.linnworks.net/";
        public string StatusMsg { get; set; } = "loading, please wait ...";
    }

    public partial class myLinnWebApiContext
    {
        public ApiStatus apiStatus { get; set; } = new ApiStatus();
        private static HttpClient httpClient = new HttpClient();

        public ApiStatus SetApiToken(string token)
        {
            apiStatus.ApiToken = token;
            apiStatus.StatusMsg = string.Format("SetApiToken(\"{0}\") => Done", token);
            return apiStatus;
        }

        //Helper function, related to target site API
        private async Task<HttpResponseMessage> SendRequestToApiAsync(string route, string query = "")
        {
            string requestUri = apiStatus.ApiServer + route + "?" + query;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("Authorization", apiStatus.ApiToken);

            return await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            //string route = "/api/Inventory/GetCategories";
            string route = "/api/Dashboards/ExecuteCustomScriptQuery";
            string query = @"script=
                SELECT p.CategoryId, p.CategoryName, Count(S.CategoryId) AS CategoryStock
                FROM ProductCategories AS P
                LEFT JOIN StockItem AS S ON P.CategoryId = S.CategoryId
                GROUP BY P.CategoryId, P.CategoryName";

            apiStatus.StatusMsg = string.Format("GetAllCategories() => ");
            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            string respString = response.Content.ReadAsStringAsync().Result;
            var parsedObject = JObject.Parse(respString);
            if (response.IsSuccessStatusCode)
            {
                var list = parsedObject["Results"].ToObject<List<Category>>();
                apiStatus.StatusMsg += string.Format("OK, {0} item(s)", list.Count);
                return list;
            }
            apiStatus.StatusMsg += parsedObject["Message"].ToString();
            return new List<Category>();
        }

        public ApiStatus AddCategory(Category category)
        {
            string route = "/api/Inventory/CreateCategory";
            string query = "CategoryName=" + category.CategoryName;

            apiStatus.StatusMsg = string.Format("AddCategory(\"{0}\") => ", category.CategoryName);
            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            string respString = response.Content.ReadAsStringAsync().Result;
            var parsedObject = JObject.Parse(respString);
            if (response.IsSuccessStatusCode)
            {
                category.CategoryId = parsedObject["CategoryId"].ToString();
                apiStatus.StatusMsg += string.Format("OK, (id:\"{0}\")", category.CategoryId);
            }
            else
                apiStatus.StatusMsg += parsedObject["Message"].ToString();
            return apiStatus;
        }

        public ApiStatus UpdateCategory(Category category)
        {
            string route = "/api/Inventory/UpdateCategory";
            string query = "category={" +
                "\"CategoryId\": \"" + category.CategoryId + "\"," +
                "\"CategoryName\": \"" + category.CategoryName + "\"}";

            apiStatus.StatusMsg = string.Format("UpdateCategory(\"{0}\") => ", category.CategoryName);
            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            string respString = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                apiStatus.StatusMsg += "OK";
            else
            {
                var parsedObject = JObject.Parse(respString);
                apiStatus.StatusMsg += parsedObject["Message"].ToString();
            }
            return apiStatus;
        }

        public Category GetCategoryData(string id)
        {
            //string route = "/api/Inventory/GetCategories";
            string route = "/api/Dashboards/ExecuteCustomScriptQuery";
            string query = @"script=
                SELECT p.CategoryId, p.CategoryName, Count(S.CategoryId) AS CategoryStock
                FROM ProductCategories AS P
                LEFT JOIN StockItem AS S ON P.CategoryId = S.CategoryId
                WHERE p.CategoryId LIKE '" + id + @"'
                GROUP BY P.CategoryId, P.CategoryName";

            apiStatus.StatusMsg = string.Format("GetCategoryData({0}) => ", id);
            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            string respString = response.Content.ReadAsStringAsync().Result;
            var parsedObject = JObject.Parse(respString);
            if (response.IsSuccessStatusCode)
            {
                var list = parsedObject["Results"].ToObject<List<Category>>();
                apiStatus.StatusMsg +="OK";
                return list[0];
            }
            apiStatus.StatusMsg += parsedObject["Message"].ToString();
            return new Category();
        }

        public ApiStatus DeleteCategory(string CategoryId)
        {
            string route = "/api/Inventory/DeleteCategoryById";
            string query = "categoryId=" + CategoryId;

            apiStatus.StatusMsg = string.Format("DeleteCategory(\"{0}\") => ", CategoryId);
            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            string respString = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                apiStatus.StatusMsg += "OK";
            else
            {
                var parsedObject = JObject.Parse(respString);
                apiStatus.StatusMsg += parsedObject["Message"].ToString();
            }
            return apiStatus;
        }
    }
}

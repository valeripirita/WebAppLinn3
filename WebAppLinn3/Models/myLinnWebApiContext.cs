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
        public string ApiToken { get; set; } = "your-api-token";
        public string ApiServer { get; set; } = "https://eu.linnworks.net/";
        public string StatusMsg { get; set; } = "loading, please wait ...";
    }

    public partial class MyLinnWebApiContext
    {
        public ApiStatus apiStatus = new ApiStatus();
        private static HttpClient httpClient = new HttpClient();

        public ApiStatus SetApiToken(string token)
        {
            apiStatus.StatusMsg = string.Format("SetApiToken(\"{0}\") => ", token);
            if (token == null || token.Length == 0)
                apiStatus.StatusMsg += "Error: Token can't be empty!";
            else
            {
                apiStatus.StatusMsg += "OK";
                apiStatus.ApiToken = token;
            }
            return apiStatus;
        }

        //Helper function, related to target site API
        private JObject ExecuteApiRequest(string route, string query = "")
        {
            string requestUri = apiStatus.ApiServer + route + "?" + query;
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("Authorization", apiStatus.ApiToken);
            var response = httpClient.SendAsync(request).Result;

            string respString = response.Content.ReadAsStringAsync().Result;
            JObject obj = JObject.Parse(respString.Length > 0 ? respString : "{}");
            if (response.IsSuccessStatusCode)
            {
                obj["msg"] = "OK";
                obj["ok"] = true;
            }
            else
            {
                obj["msg"] = obj["Message"];
                obj["ok"] = false;
            }
            return obj;
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

            var list = new List<Category>();
            var res = ExecuteApiRequest(route, query);
            string msg = string.Format("GetAllCategories() => {0}", res["msg"]);
            if (res["ok"].ToObject<bool>() == true)
            {
                list = res["Results"].ToObject<List<Category>>();
                msg += string.Format(", {0} item(s)", list.Count);
            }
            apiStatus.StatusMsg = msg;
            return list;
        }

        public ApiStatus AddCategory(Category category)
        {
            string route = "/api/Inventory/CreateCategory";
            string query = "CategoryName=" + category.CategoryName;

            var res = ExecuteApiRequest(route, query);
            var msg = string.Format("AddCategory(\"{0}\") => {1}", category.CategoryName, res["msg"]);
            if (res["ok"].ToObject<bool>() == true)
            {
                category.CategoryId = res["CategoryId"].ToString();
                msg += string.Format(", (id:\"{0}\")", category.CategoryId);
            }
            apiStatus.StatusMsg = msg;
            return apiStatus;
        }

        public ApiStatus UpdateCategory(Category category)
        {
            string route = "/api/Inventory/UpdateCategory";
            string query = "category={" +
                "\"CategoryId\": \"" + category.CategoryId + "\"," +
                "\"CategoryName\": \"" + category.CategoryName + "\"}";

            var res = ExecuteApiRequest(route, query);
            var msg = string.Format("UpdateCategory(\"{0}\") => {1}", category.CategoryName, res["msg"]);
            apiStatus.StatusMsg = msg;
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

            var cat = new Category();
            var res = ExecuteApiRequest(route, query);
            var msg = string.Format("GetCategoryData({0}) => {1}", id, res["msg"]);
            if (res["ok"].ToObject<bool>() == true)
            {
                cat = res["Results"].ToObject<List<Category>>()[0];
            }
            apiStatus.StatusMsg = msg;
            return cat;
        }

        public ApiStatus DeleteCategory(string CategoryId)
        {
            string route = "/api/Inventory/DeleteCategoryById";
            string query = "categoryId=" + CategoryId;

            var res = ExecuteApiRequest(route, query);
            var msg = string.Format("DeleteCategory(\"{0}\") => {1}", CategoryId, res["msg"]);
            apiStatus.StatusMsg = msg;
            return apiStatus;
        }
    }
}

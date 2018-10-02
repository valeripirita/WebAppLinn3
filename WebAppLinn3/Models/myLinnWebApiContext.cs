using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using System.Data;


namespace WebAppLinn3.Models
{
    public partial class myLinnWebApiContext
    {
        //public string ApiToken { get; set; }

        private string _apiServer = "https://eu.linnworks.net/";
        private string _apiToken = "097b0f85-7a6d-44ef-9aac-abbdd994bcc4";
        private static HttpClient httpClient = new HttpClient();

        public class helper
        {
            public class col { int Index { get; set; } string Name { get; set; } string Type { get; set; } }
            public bool IsError { get; set; }
            string ErrorMessage { get; set; }
            int TotalResults { get; set; }
            public List<col> Columns { get; set; }
            public List<Category> Results { get; set; }
        }
        
        //Helper function, related to target site API
        private async Task<HttpResponseMessage> SendRequestToApiAsync(string route, string query = "")
        {
            string requestUri = _apiServer + route + "?" + query;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("Authorization", _apiToken);

            return await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            string route = "/api/Dashboards/ExecuteCustomScriptQuery";
            string query = @"script=
                SELECT p.CategoryId, p.CategoryName, Count(S.CategoryId) AS CategoryStock
                FROM ProductCategories AS P
                LEFT JOIN StockItem AS S ON P.CategoryId = S.CategoryId
                GROUP BY P.CategoryId, P.CategoryName";
            //string route = "/api/Inventory/GetCategories", query = "";

            //System.Net.Http.HttpContentExtensions

            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            if (response.IsSuccessStatusCode)
            {
                //return response.Content.ReadAsAsync<helper>().Result.Results;
                string respString = response.Content.ReadAsStringAsync().Result;
                var respFull = JsonConvert.DeserializeObject<helper>(respString);
                return respFull.Results;
            }
            return new List<Category>();
        }

        public bool AddCategory(Category category)
        {
            string route = "/api/Inventory/CreateCategory";
            string query = "CategoryName=" + category.CategoryName;

            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            return response.IsSuccessStatusCode;
            //{
            //return response.Content.ReadAsAsync<IEnumerable<Category>>().Result;
            //return response.Content.ReadAsStringAsync().Result;
            //   return respContent;
            //}
            //return "Error: " + response.ReasonPhrase + "\n" + respContent;
        }

        public bool UpdateCategory(Category category)
        {
            string route = "/api/Inventory/UpdateCategory";
            string query = "category={" +
                "\"CategoryId\": \"" + category.CategoryId + "\"," +
                "\"CategoryName\": \"" + category.CategoryName + "\"}";

            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            return response.IsSuccessStatusCode;
        }

        public Category GetCategoryData(string id)
        {
            string route = "/api/Dashboards/ExecuteCustomScriptQuery";
            string query = @"script=
                SELECT p.CategoryId, p.CategoryName, Count(S.CategoryId) AS CategoryStock
                FROM ProductCategories AS P
                LEFT JOIN StockItem AS S ON P.CategoryId = S.CategoryId
                WHERE p.CategoryId LIKE '" + id + @"'
                GROUP BY P.CategoryId, P.CategoryName";
            //string route = "/api/Inventory/GetCategories", query = "";

            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            if (response.IsSuccessStatusCode)
            {
                //var Categories = response.Content.ReadAsAsync<helper>().Result.Results;
                string respString = response.Content.ReadAsStringAsync().Result;
                var respFull = JsonConvert.DeserializeObject<helper>(respString);
                return respFull.Results[0];
            }
            return new Category();
        }

        public bool DeleteCategory(string CategoryId)
        {
            string route = "/api/Inventory/DeleteCategoryById";
            string query = "categoryId=" + CategoryId;

            HttpResponseMessage response = SendRequestToApiAsync(route, query).Result;
            return response.IsSuccessStatusCode;
        }
    }
}

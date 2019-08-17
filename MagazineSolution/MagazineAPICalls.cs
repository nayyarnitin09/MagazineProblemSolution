using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MagazineSolution
{
    public  class MagazineAPICalls
    {

       
        public  const string baseUrl= "http://magazinestore.azurewebsites.net";
        
        //get token first
        public static string GetToken()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl + "/api/token").Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GetTokenResponse>(response.Content.ReadAsStringAsync().Result).token;
            else
                return "";
        }


        // get  subscribers
        public static GetSubscribersResponse GetSubscribers()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl + "/api/subscribers/" + GetToken()).Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GetSubscribersResponse>(response.Content.ReadAsStringAsync().Result);
            else
                return new GetSubscribersResponse();
        }


        //get categories and magazines

        public static Dictionary<string, List<int>> GetCategoriesAndMagazines()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl + "/api/categories/" + GetToken()).Result;

            if (response.IsSuccessStatusCode)
            {
                return ReturnCategoryMagazineDictonaryObject(JsonConvert.DeserializeObject<GetCategoriesResponse>(response.Content.ReadAsStringAsync().Result).data);
            }
            else
            {
               return new Dictionary<string, List<int>>();
            }

        }

        public static Dictionary<string, List<int>> ReturnCategoryMagazineDictonaryObject(List<string> categories)
        {
            HttpClient client = new HttpClient();
            Dictionary<string, List<int>> dictCategoriesMagazines = new Dictionary<string, List<int>>();
            foreach (var category in categories)
            {
                dictCategoriesMagazines.Add(category, new List<int>());
                HttpResponseMessage response = client.GetAsync(baseUrl + "/api/magazines/" + GetToken() + "/" + category).Result;
                if (response.IsSuccessStatusCode)
                {
                    var magazines = JsonConvert.DeserializeObject<GetMagazineResponse>(response.Content.ReadAsStringAsync().Result).data;
                    if(magazines != null)
                    foreach(var magazine in magazines)
                    {
                        dictCategoriesMagazines[category].Add(magazine.id);
                    }
                }
            }

            return dictCategoriesMagazines;
        }

        public static bool IsAnswerCorrect(ListOfSubscribers lstSubscriberIds)
        {
            HttpClient client = new HttpClient();
 
            var content = new StringContent(JsonConvert.SerializeObject(lstSubscriberIds), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(baseUrl + "/api/answer/" + GetToken(),content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<GetAnswerResponse>(response.Content.ReadAsStringAsync().Result);
                return Convert.ToBoolean(result.success);
            }

            return false;
       }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Data
{
    public interface IAPIService
    {
        Task<List<Post>> GetPosts();
    }
    public class APIService
    {
        #region Variables

        private HttpClient client;

        #endregion

        #region Constructor

        public APIService() { }

        #endregion

        #region Methods

        public async Task<List<Post>> GetPosts()
        {
            /*
            Debug.WriteLine("GET for people.");
            var list = new List<float>(2);
            list.Add(-81f);
            list.Add(26f);
            Debug.WriteLine(list);
            return new List<Post>()
             {
             new Post() { content="Funny Boi", _id="123456789", lazy_load="123456789", date = new DateTime(), dis= (float)25876.66, geometry = new Geometry { coordinates =  list } },
             new Post() { content="Funnier Boi", _id="923456789", lazy_load="923456789", date = new DateTime(),dis= (float)25876.66 , geometry = new Geometry { coordinates =  list } },
             new Post() { content="Stop, shite talking will ye", _id="193456789", lazy_load="193456789", date = new DateTime(),dis= (float)25876.66 , geometry = new Geometry { coordinates =  list } },
             };*/

            this.client = new HttpClient();

            this.client.DefaultRequestHeaders.Accept.Clear();

            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build URL.
            string url = "http://localhost:4000/?lng=-81&lat=26";

            client.BaseAddress = new Uri(url);

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Post>>();
            }
            else
            {
                return null;
            }
        }
        public static void Write(Post post)
        {
            Debug.WriteLine("Add post");
            var list = new List<float>(2);
            // Add latitude to list.
            list.Add(-81f);
            // Add longtitude to list.
            list.Add(26f);
            // Then set list to equal posts coords
            // Send post in post req
        }
        public static void Delete(Post post)
        {
            Debug.WriteLine("DELETE post");
        }

        #endregion
    }
}

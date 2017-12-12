using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Data
{
    public interface IAPIService
    {
        Task<List<Post>> GetPosts();
        Task<List<Post>> GetMorePosts(string urlData);
        Task<Post> AddPost(Post post);
    }
    public class APIService: IAPIService
    {
        #region Variables

        private HttpClient client;
        private static readonly string baseUrl = "https://salty-ridge-39119.herokuapp.com/";
        string latitude;
        string longitude;
        bool accessAllowed;

        #endregion

        #region Constructor

        public APIService() { }

        #endregion

        #region API Methods

        public async Task<List<Post>> GetPosts()
        {
            accessAllowed = await GetLocation();

            if (!accessAllowed) return null;

            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude)) return null;

            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build URL.
            string url = baseUrl + "?lng=" + longitude +"&lat=" + latitude;
            
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

        public async Task<List<Post>> GetMorePosts(string urlData)
        {
            if (!accessAllowed) return null;

            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(urlData)) return null;

            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build URL.
            string url = baseUrl + "?lng=" + longitude + "&lat=" + latitude + urlData;
           
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

        public async Task<Post> AddPost(Post post)
        {
            accessAllowed = await GetLocation();

            if (!accessAllowed) return null;

            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude)) return null;

            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var list = new List<float>(2);
            list.Add(float.Parse(longitude));
            list.Add(float.Parse(latitude));
            post.geometry = new Geometry { coordinates = list };

            client.BaseAddress = new Uri(baseUrl);

            HttpContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("",content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Post>();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Helper Methods

        public async Task<bool> GetLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            var accessAllowed = false;

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    accessAllowed = true;

                    var geoLocator = new Geolocator();
                    geoLocator.DesiredAccuracy = PositionAccuracy.Default;
                    Geoposition pos = await geoLocator.GetGeopositionAsync();
                    latitude = pos.Coordinate.Point.Position.Latitude.ToString();
                    longitude = pos.Coordinate.Point.Position.Longitude.ToString();

                    break;

                case GeolocationAccessStatus.Denied:
                    break;

                case GeolocationAccessStatus.Unspecified:
                    break;
            }
            return accessAllowed;
        }

        #endregion
    }
}

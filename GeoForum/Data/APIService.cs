using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Diagnostics;
using Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Windows.System;

namespace Data
{
    public interface IAPIService
    {
        Task<List<Post>> GetPosts();
        Task<List<Post>> GetMorePosts(string urlData);
    }
    public class APIService: IAPIService
    {
        #region Variables

        private HttpClient client;
        private string baseUrl = "http://localhost:4000/";
        string latitude;
        string longitude;
        bool accessAllowed;

        #endregion

        #region Constructor

        public APIService() { }

        #endregion

        #region Methods

        public async Task<List<Post>> GetPosts()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            accessAllowed = false;

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

            if (!accessAllowed) return null;

            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude)) return null;

            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build URL.
            string url = baseUrl + "?lng=-81&lat=26"; //"http://localhost:4000/?lng=-81&lat=26";
            // Add lng and lat.
            // string url = baseUrl + "?lng=" + longitude + "&lat=" + latitude;
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
        public static void AddPost(Post post)
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
        public async Task<List<Post>> GetMorePosts(string urlData)
        {
            if (!accessAllowed) return null;

            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(urlData)) return null;

            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build URL.
            string url = baseUrl + "?lng=-81&lat=26" + urlData;
            // Add lng and lat.
            // Add urlData.
            // string url = baseUrl + "?lng=" + longitude + "&lat=" + latitude + urlData;
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

        #endregion
    }
}

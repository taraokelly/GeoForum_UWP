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
        
    }
    public class APIService
    {
        #region Variables

        private HttpClient client;
        private string baseUrl = "http://localhost:4000/";
        string latitude;
        string longitude;

        #endregion

        #region Constructor

        public APIService() { }

        #endregion

        #region Methods

        public async Task<List<Post>> GetPosts()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            bool accessAllowed = false;

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    accessAllowed = true;

                    var geoLocator = new Geolocator();
                    geoLocator.DesiredAccuracy = PositionAccuracy.Default;
                    Geoposition pos = await geoLocator.GetGeopositionAsync();
                    string latitude = pos.Coordinate.Point.Position.Latitude.ToString();
                    string longitude = pos.Coordinate.Point.Position.Longitude.ToString();

                    Debug.WriteLine("LAT: " + latitude);
                    Debug.WriteLine("LAT: " + longitude);

                    break;

                case GeolocationAccessStatus.Denied:
                    break;

                case GeolocationAccessStatus.Unspecified:
                    break;
            }

            if (!accessAllowed) return null;

            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build URL.
            string url = baseUrl + "?lng=-81&lat=26"; //"http://localhost:4000/?lng=-81&lat=26";
            // Add lng and lat.
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

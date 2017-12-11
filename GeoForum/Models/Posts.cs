using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Diagnostics;

namespace Models
{
    public class Posts
    {
        #region Variables

        public List<Post> posts { get; set; }
        APIService APIService;

        #endregion

        #region Constructor

        public Posts()
        {
            posts = new List<Post>();
            APIService = new APIService();
        }

        #endregion

        #region Methods

        public async Task<List<Post>> GetPosts()
        {
            var response = await APIService.GetPosts();

            if (response != null)
            {
                foreach (var post in response)
                {
                    Debug.WriteLine(post.date);
                    posts.Add(post);
                }
            }
            return response;
        }
        public async Task<List<Post>> RefreshPosts()
        {
            posts.Clear();

            var response = await APIService.GetPosts();

            if (response != null)
            {
                foreach (var post in response)
                {
                    Debug.WriteLine(post.content);
                    posts.Add(post);
                }
            }
            return response;
        }
        public async Task<List<Post>> GetMorePosts()
        {
            string urlData = "";
            DateTime lastDate = new DateTime();
            int count = (posts.Count() - 1);
            
            for (int i = count; i >= 0; i--)
            {
                if(i == count)
                {
                    lastDate = posts[i].date;
                    urlData = "&yr=" + lastDate.Year + "&m=" + (lastDate.Month - 1) + "&d=" + lastDate.Day + "&hr=" + lastDate.Hour + "&mins=" + lastDate.Minute + "&s=" + (lastDate.Second - 1) + "&id=" + posts[i]._id;
                }
                else
                {
                    int result = DateTime.Compare(lastDate, posts[i].date);

                    if (result == 0)
                        urlData += "&id=" + posts[i]._id;
                    else
                        break;
                }
            }
            var response = await APIService.GetMorePosts(urlData);

            if (response != null)
            {
                foreach (var post in response)
                {
                    posts.Add(post);
                }
            }
            return response;
        }
        public async Task<Post> Add(Post post)
        {
            if (!posts.Contains(post))
            {
                var response = await APIService.AddPost(post);
                // Insert at top of list.
                if (response != null)
                {
                    posts.Insert(0, post);
                    return response;
                }
            }
            return null;
        }
        #endregion
    }
}
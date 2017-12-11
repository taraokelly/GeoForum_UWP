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
                    //posts.Insert(0, post);
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
                    //posts.Insert(0, post);
                }
            }
            return response;
        }
        public void Add(Post person)
        {
            if (!posts.Contains(person))
            {
                // Insert at top of list.
                posts.Insert(0, person);
                APIService.Write(person);
            }
        }
        public void Delete(Post person)
        {
            if (posts.Contains(person))
            {
                posts.Remove(person);
                APIService.Delete(person);
            }
        }
        public void Update(Post person)
        {
            APIService.Write(person);
        }

        #endregion
    }
}
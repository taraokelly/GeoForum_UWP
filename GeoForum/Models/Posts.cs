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
        public List<Post> posts { get; set; }
        APIService APIService = new APIService();
        public Posts()
        {
            posts = new List<Post>();
        }
        public async Task<List<Post>> GetPeople()
        {
            var response = await APIService.GetPeople();
            if (response == null)
            {
                Debug.WriteLine("");
            }
            else
            {
                foreach (var post in response)
                {
                    Debug.WriteLine(post.content);
                    posts.Add(post);
                }
            }
            return response;
        }
        public void Add(Post person)
        {
            if (!posts.Contains(person))
            {
                // Posts.Add(person);
                // Insert to top of list.
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Models
{
    public class Posts
    {
        public List<Post> posts { get; set; }
        //public String Name { get; set; }
        public Posts(/*String databaseName*/)
        {
            //Name = databaseName;
            posts = APIService.GetPeople();
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
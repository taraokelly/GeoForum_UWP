using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Data
{
    public class Post
    {
        /*public String Name { get; set; }
        public int Age { get; set; }*/
        public String content { get; set; }
        public String _id { get; set; }
        public String lazy_load { get; set; }
        // public String date { get; set; }
        public Double distance { get; set; }
        public Double lng { get; set; }
        public Double lat { get; set; }
    }
    public class APIService
    {
        public static String Name = "Fake Data Service.";
        public static List<Post> GetPeople()
        {
            Debug.WriteLine("GET for people.");
            return new List<Post>()
 {
 new Post() { content="Funny Boi", _id="123456789", lazy_load="123456789", distance= 25876.66, lng=-80, lat=26 },
 new Post() { content="Funnier Boi", _id="923456789", lazy_load="923456789", distance= 25876.66, lng=-80, lat=26 },
 new Post() { content="Stop, shite talking will ye", _id="193456789", lazy_load="193456789", distance= 25876.66, lng=-80, lat=26 },
 };
        }
        public static void Write(Post person)
        {
            Debug.WriteLine("INSERT person with name " + person._id);
        }
        public static void Delete(Post person)
        {
            Debug.WriteLine("DELETE person with name " + person._id);
        }
    }
}

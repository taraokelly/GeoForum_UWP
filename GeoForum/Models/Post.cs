using System;

namespace Models
{
    public class Post
    {
        public string _id { get; set; }
        public string content { get; set; }
        public string lazy_load { get; set; }
        public DateTime date { get; set; }
        public Geometry geometry { get; set; }
        public int __v { get; set; }
        public float dis { get; set; }
    }
}

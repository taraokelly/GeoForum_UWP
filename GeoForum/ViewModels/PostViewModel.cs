using System;

using Models;

namespace ViewModels
{
    public class PostViewModel : NotificationBase<Post>
    {
        #region Contructor

        public PostViewModel(Post person = null) : base(person) { }

        #endregion

        #region Getters and Setters

        public String content
        {
            get { return This.content; }
            set { SetProperty(This.content, value, () => This.content = value); }
        }
        public String _id
        {
            get { return This._id; }
            set { SetProperty(This._id, value, () => This._id = value); }
        }
        public String lazy_load
        {
            get { return This.lazy_load; }
            set { SetProperty(This.lazy_load, value, () => This.lazy_load = value); }
        }
        public DateTime date {
            get { return This.date; }
            set { SetProperty(This.date, value, () => This.date = value); }
        }
 
        public Geometry geometry
        {
            get { return This.geometry; }
            set { SetProperty(This.geometry, value, () => This.geometry = value); }
        }
        public int __v
        {
            get { return This.__v; }
            set { SetProperty(This.__v, value, () => This.__v = value); }
        }
        public float dis
        {
            get { return This.dis; }
            set { SetProperty(This.dis, value, () => This.dis = value); }
        }

        #endregion
    }
}
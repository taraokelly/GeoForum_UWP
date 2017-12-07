using System;

using Data;
namespace ViewModels
{
    public class PostViewModel : NotificationBase<Post>
    {
        public PostViewModel(Post person = null) : base(person) { }
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
        public Double distance
        {
            get { return This.distance; }
            set { SetProperty(This.distance, value, () => This.distance = value); }
        }
        public Double lng
        {
            get { return This.lng; }
            set { SetProperty(This.lng, value, () => This.lng = value); }
        }
        public Double lat
        {
            get { return This.lat; }
            set { SetProperty(This.lat, value, () => This.lat = value); }
        }
    }
}
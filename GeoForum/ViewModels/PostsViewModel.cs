using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Models;
using Windows.UI.Core;
using Windows.UI.Xaml.Input;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.System;

namespace ViewModels
{
    public class PostsViewModel : NotificationBase
    {
        #region Variables

        // Posts Model Object holding the real values and their methods to handle them.
        Posts Posts_Obj;
        // Observable Collection to hold the post that will be binded to the UI.
        ObservableCollection<PostViewModel> _Posts;
        PostViewModel _Post;
        bool _IsVisible;
        bool _LoadMore;
        bool _Error;

        #endregion

        #region Constructor

        public PostsViewModel()
        {
            IsVisible = false;
            _Error = false;
            Posts_Obj = new Posts();
            _Posts = new ObservableCollection<PostViewModel>();
            _Post = new PostViewModel();
            GetPosts();

        }

        #endregion

        #region Getters and Setters

        public bool IsVisible
        {
            get { return _IsVisible; }
            set { SetProperty(ref _IsVisible, value); }
        }

        public bool Error
        {
            get { return _Error; }
            set { SetProperty(ref _Error, value); }
        }

        public ObservableCollection<PostViewModel> Posts
        {
            get { return _Posts; }
            set { SetProperty(ref _Posts, value); }
        }

        public PostViewModel Post
        {
            get { return _Post; }
            set { SetProperty(ref _Post, value); }
        }

        #endregion

        #region Methods

        public async void Add()
        {
            // Check if string is null or empty before adding
            if (!string.IsNullOrEmpty(Post.content))
            {
                // Put Data in another object and save.
                var p = new PostViewModel() { content = Post.content };
                var post = new PostViewModel() { content = Post.content };
                var response = await Posts_Obj.Add(post);
                if (response != null)
                {
                    Posts.Insert(0, p);
                    Post.content = "";
                }
            }
        }
        public async void GetPosts()
        {
            _LoadMore = true;
            Error = false;
            IsVisible = true;
            var response = await Posts_Obj.GetPosts();
            IsVisible = false;

            if (response == null)
            {
                Error = true;
            }
            else
            {
                if (response.LongCount() == 0) _LoadMore = false;
                // Load the database - Really from the Model that has loaded the db.
                foreach (var post in Posts_Obj.posts)
                {
                    var p = new PostViewModel(post);
                    _Posts.Add(p);
                }
            }
        }

        public async void GetMorePosts()
        {
            if (_LoadMore)
            {
                Error = false;
                IsVisible = true;
                var response = await Posts_Obj.GetMorePosts();
                IsVisible = false;

                if (response == null)
                {
                    Error = true;
                }
                else
                {
                    if (response.LongCount() == 0) _LoadMore = false;

                    foreach (var post in response)
                    {
                        var p = new PostViewModel(post);
                        _Posts.Add(p);
                    }
                }
            }
        }

        public async void RefreshPosts()
        {
            _Posts.Clear();

            _LoadMore = true;
            Error = false;
            IsVisible = true;
            var response = await Posts_Obj.RefreshPosts();
            IsVisible = false;

            if (response == null)
            {
                Error = true;
            }
            else
            {
                if (response.LongCount() == 0) _LoadMore = false;
                // Load the database - Really from the Model that has loaded the db.
                foreach (var post in Posts_Obj.posts)
                {
                    var p = new PostViewModel(post);
                    _Posts.Add(p);
                }
            }
        }

        #endregion
    }
}
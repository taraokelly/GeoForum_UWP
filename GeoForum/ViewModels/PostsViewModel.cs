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
        int Last_Item;

        #endregion

        #region Constructor

        public PostsViewModel()
        {
            IsVisible = false;
            Posts_Obj = new Posts();
            _Posts = new ObservableCollection<PostViewModel>();
            _Post = new PostViewModel();
            // Set SelectedIndex to out of scope index.
            //_SelectedIndex = -1;
            // Call async method to get posts.
            GetPosts();

        }

        #endregion

        #region Getters and Setters

        public bool IsVisible
        {
            get { return _IsVisible; }
            set { SetProperty(ref _IsVisible, value); }
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
        /*int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (SetProperty(ref _SelectedIndex, value))
                { RaisePropertyChanged(nameof(SelectedPerson)); }
            }
        }
        public PostViewModel SelectedPerson
        {
            get { return (_SelectedIndex >= 0) ? _Posts[_SelectedIndex] : null; }
        }*/

#endregion

        #region Methods

        public void Add()
        {
            /*var person = new PostViewModel();
            person.PropertyChanged += Person_OnNotifyPropertyChanged;
            //Posts.Add(person);
            Posts.Insert(0, person);
            Posts_Obj.Add(person);
            SelectedIndex = Posts.IndexOf(person);*/

            // Check if string is null or empty before adding
            if (!string.IsNullOrEmpty(Post.content))
            {
                // Put Data in another object and save.
                _Post.PropertyChanged += Person_OnNotifyPropertyChanged;
                var person = new PostViewModel() { content = Post.content };
                Posts.Insert(0, person);
                Posts_Obj.Add(person);
                _Post.PropertyChanged += Person_OnNotifyPropertyChanged;
                Post.content = "";
            }
        }
        /* public void Delete()
         {
             if (SelectedIndex != -1)
             {
                 var person = Posts[SelectedIndex];
                 Posts.RemoveAt(SelectedIndex);
                 Posts_Obj.Delete(person);
             }
         }*/
        /*public object IsVisible()
        {
            return Visibility.Collapsed;
        }*/
        public void LoadMore()
        {
            Debug.WriteLine("LOAD MORE");
        }
        public async void GetPosts()
        {
            var response = await Posts_Obj.GetPeople();
            if (response == null)
            {
                /*********************************
                 * TELL USER THERE IS NO DATA TO SHOW
                 *********************************/
                Debug.WriteLine("NULL");
            }
            else
            { 
                // Load the database - Really from the Model that has loaded the db.
                foreach (var post in Posts_Obj.posts)
                {
                    var np = new PostViewModel(post);
                    np.PropertyChanged += Person_OnNotifyPropertyChanged;
                    _Posts.Add(np);

                }
            }
        }

        #endregion

        #region Event Handlers

        void Person_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            Posts_Obj.Update((PostViewModel)sender);
        }

        #endregion
    }
}
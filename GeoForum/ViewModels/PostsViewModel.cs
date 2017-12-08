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

namespace ViewModels
{
    public class PostsViewModel : NotificationBase
    {
        Posts Posts_Obj;
        public PostsViewModel(/*String name*/)
        {
            Posts_Obj = new Posts(/*name*/);
            _SelectedIndex = -1;
            // Load the database
            foreach (var person in Posts_Obj.posts)
            {
                var np = new PostViewModel(person);
                np.PropertyChanged += Person_OnNotifyPropertyChanged;
                _Posts.Add(np);
            }
        }
        ObservableCollection<PostViewModel> _Posts
        = new ObservableCollection<PostViewModel>();
        public ObservableCollection<PostViewModel> Posts
        {
            get { return _Posts; }
            set { SetProperty(ref _Posts, value); }
        }
        PostViewModel _Post = new PostViewModel();//{ content = "", _id = "", lazy_load = "", distance = 0.00, lng = -80, lat = 26 };
        public PostViewModel Post
        {
            get { return _Post; }
            set { SetProperty(ref _Post, value); }
        }
        /*String _Name;
        public String Name
        {
            get { return Posts_Obj.Name; }
        }*/
        int _SelectedIndex;
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
        }
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
                _Post.PropertyChanged += Person_OnNotifyPropertyChanged;
                var person = new PostViewModel() { content = Post.content };
                Posts.Insert(0, person);
                Posts_Obj.Add(person);
                _Post.PropertyChanged += Person_OnNotifyPropertyChanged;
                Post.content = "";
            }
        }
        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var person = Posts[SelectedIndex];
                Posts.RemoveAt(SelectedIndex);
                Posts_Obj.Delete(person);
            }
        }
        void Person_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            Posts_Obj.Update((PostViewModel)sender);
        }
    }
}
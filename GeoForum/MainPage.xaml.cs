using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ViewModels;

namespace GeoForum
{
    public sealed partial class MainPage : Page
    {
       
        public MainPage()
        {
            this.InitializeComponent();
            PostsVM = new PostsViewModel();
        }
        public PostsViewModel PostsVM { get; set; }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            PostsVM.GetPosts();
        }
        // Adapted from: https://stackoverflow.com/questions/12683070/how-to-detect-if-the-scroll-viewer-reaches-bottom-in-winrt
        private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var verticalOffset = MyScrollView.VerticalOffset;
            var maxVerticalOffset = MyScrollView.ScrollableHeight;

            if (maxVerticalOffset < 0 ||
                verticalOffset == maxVerticalOffset)
            {
                PostsVM.GetMorePosts();
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            PostsVM.GetPosts();
        }
    }
}

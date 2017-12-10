﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ViewModels;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GeoForum
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       
        public MainPage()
        {
            this.InitializeComponent();
            Organization = new PostsViewModel();
        }
        public PostsViewModel Organization { get; set; }

        /*private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            //Organization.
        }*/
        // Adapted from: https://stackoverflow.com/questions/12683070/how-to-detect-if-the-scroll-viewer-reaches-bottom-in-winrt
        private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var verticalOffset = MyScrollView.VerticalOffset;
            var maxVerticalOffset = MyScrollView.ScrollableHeight;

            if (maxVerticalOffset < 0 ||
                verticalOffset == maxVerticalOffset)
            {
                Organization.GetPosts();
            }
        }
    }
}

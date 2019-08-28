using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mission.GUI
{
    /// <summary>
    /// Interaction logic for SplashWindowxaml.xaml
    /// </summary>
    /// 
    public partial class SplashWindow : Window
    {

        Storyboard Showboard;
        Storyboard Hideboard;
        private delegate void ShowDelegate(string txt);
        private delegate void HideDelegate();


        public SplashWindow()
        {
            InitializeComponent();
            Showboard = this.Resources["showStoryBoard"] as Storyboard;
            Hideboard = this.Resources["HideStoryBoard"] as Storyboard;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void showText(string txt)
        {
            //txtLoading.Text = txt;
            BeginStoryboard(Showboard);
        }
        private void hideText()
        {
            BeginStoryboard(Hideboard);
        }

        private void load()
        {
            Thread.Sleep(1000);
            this.Dispatcher.Invoke(showDelegate);
            Thread.Sleep(2000);
            //do some loading work
            this.Dispatcher.Invoke(hideDelegate);

            Thread.Sleep(2000);
            this.Dispatcher.Invoke(showDelegate);
            Thread.Sleep(2000);
            //do some loading work
            this.Dispatcher.Invoke(hideDelegate);

            Thread.Sleep(2000);
            this.Dispatcher.Invoke(showDelegate);
            Thread.Sleep(2000);
            //do some loading work
            this.Dispatcher.Invoke(hideDelegate);

            //close the window
            Thread.Sleep(2000);
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate () { Close(); });
        }

        private void hideDelegate()
        {
            throw new NotImplementedException();
        }

        private void showDelegate()
        {
            throw new NotImplementedException();
        }
    }
}

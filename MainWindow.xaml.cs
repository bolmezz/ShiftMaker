using Mission.BM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mission
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        MainBM mainV;
        public MainWindow()
        {
            InitializeComponent();
            
            mainV = new MainBM();
            mainV.Closing += Window_Closing;
            mainV.ShowDialog(); // ?
            this.Hide();
            this.Close();


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            typeof(Window).GetField("_isClosing", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, false);
            e.Cancel = true;
            this.Hide();

        }

    }
}

using Mission.BM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mission.GUI
{
    /// <summary>
    /// Interaction logic for wait.xaml
    /// </summary>
    public partial class wait : waitBM
    {
        public wait()
        {
            InitializeComponent();
            //this.Closing += new System.ComponentModel.CancelEventHandler(MyWindow_Closing);
        }


        //void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    e.Cancel = true;
        //}
    }
}

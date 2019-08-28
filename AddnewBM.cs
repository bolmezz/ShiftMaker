using Mission;
using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mission.BM
{
    public partial class AddnewBM : Window
    {
        private UserManager _userManager = new UserManager();
        private DepartmentManager _deptManager = new DepartmentManager();
        public event EventHandler UserUpdated;

        public AddnewBM()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        public void load_depts(object sender, RoutedEventArgs e)
        {

            List<departments> depts = _deptManager.GetDepartments();
            foreach (var x in depts)
            {
                combo_depts.Items.Add(x.department_name.ToString());
            }

        }

        public void load_rate(object sender, RoutedEventArgs e)
        {
            combo_rates.Items.Add("Primer");
            combo_rates.Items.Add("Seconder");

        }

        public void load_statu(object sender, RoutedEventArgs e)
        {
            combo_statu.Items.Add("Uygun");
            combo_statu.Items.Add("Uygun değil");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string nametb = txt_username.Text.ToString();
            string lastnametb = txt_lastname.Text.ToString();

            if (combo_depts.SelectedIndex != -1 && combo_rates.SelectedIndex != -1 && combo_statu.SelectedIndex != -1 && nametb != "" && lastnametb != "")
            {
                string selectedDept = combo_depts.Items[combo_depts.SelectedIndex].ToString();
                decimal deptID = _deptManager.DepartmetnID(selectedDept);

                string selectedRate = combo_rates.Items[combo_rates.SelectedIndex].ToString();
                int rate = (selectedRate == "Primer") ? 1 : 2;

                string selectedStatu = combo_statu.Items[combo_statu.SelectedIndex].ToString();
                bool statu = (selectedStatu == "Uygun") ? true : false;

                bool add = _userManager.Add(nametb, lastnametb, deptID, rate, statu);

                if (add == true)
                {
                    MessageBox.Show(this, "Yeni kişi eklendi", "Bilgi");
                    EventArgs ev = new EventArgs();
                    UserUpdated.Invoke(this, ev);
                    this.Close();
                }
                else
                    MessageBox.Show(this, "Kayıt başarısız", "Bilgi");
            }
            else
            {
                MessageBox.Show(this, "Alanlar boş bırakılamaz", "Bilgi");
                this.Show();

            }

        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    }
}

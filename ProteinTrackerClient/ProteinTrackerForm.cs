using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProteinTrackerClient.ProteinTrackerService;
using System.ServiceModel;

namespace ProteinTrackerClient
{
    public partial class ProteinTrackerForm : Form
    {
        private ProteinTrackerWebserviceSoapClient service = new ProteinTrackerWebserviceSoapClient();
        private User[] users;

        public ProteinTrackerForm()
        {
            InitializeComponent();
        }

        private void ProteinTrackerForm_Load(object sender, EventArgs e)
        {
            users = service.ListUser();
            cboSelectUser.DataSource = users;
            cboSelectUser.DisplayMember = "Name";
            cboSelectUser.ValueMember = "UserId";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            service.AddUser(txtName.Text, int.Parse(txtGoal.Text));
            users = service.ListUser();
            cboSelectUser.DataSource = users;
        }

        private void cboSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = cboSelectUser.SelectedIndex;
            lblTotal.Text = users[index].Total.ToString(); ;
            lblGoal.Text = users[index].Goal.ToString();
        }

        async private void OnAddProtein(object sender, EventArgs e)
        {
            var userId = users[cboSelectUser.SelectedIndex].UserId;
            try
            {
                var auth = new AuthenticationHeader { UserName = "bob", Password = "665" };
                var newTotal = await service.AddProteinAsync(auth,int.Parse(textBox3.Text), userId);
                users[cboSelectUser.SelectedIndex].Total = newTotal.AddProteinResult;
                lblTotal.Text = newTotal.ToString();
            }
            catch(FaultException ex)
            {
                Console.WriteLine("Could not add protein: {0}", ex);

            }
            
        }
    }
}

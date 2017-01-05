using CloudFlareUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFlareCookies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnMain_Click(object sender, EventArgs e)
        {
            btnMain.Text = "Getting the information...";
            btnMain.Enabled = false;

            var target = new Uri("http://poe.trade");

            var handler = new ClearanceHandler();

            var client = new HttpClient(handler);

            try
            {
                var content = await client.GetStringAsync(target);
                txtUserAgent.Text = "Client/1.0";
                txtCfduid.Text = handler.IDCookieValue;
                txtCfClearance.Text = handler.ClearanceCookieValue;
            }
            catch (AggregateException ex) when (ex.InnerException is CloudFlareClearanceException)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

            btnMain.Text = "Click me to get cookie info";
            btnMain.Enabled = true;
        }
    }
}

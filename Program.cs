using CloudFlareUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFlareCookies
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/

            var target = new Uri("http://poe.trade");

            var handler = new ClearanceHandler();

            var client = new HttpClient(handler);

            try
            {
                var content = client.GetStringAsync(target).Result;
                Console.WriteLine(handler.ClearanceCookieValue);
                Console.WriteLine(handler.IDCookieValue);
               
            }
            catch (AggregateException ex) when (ex.InnerException is CloudFlareClearanceException)
            {
                Console.WriteLine(ex.InnerException.Message);
            }

            Console.Read();
        }
    }
}

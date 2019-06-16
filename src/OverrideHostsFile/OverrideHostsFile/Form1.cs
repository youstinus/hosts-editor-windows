using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace OverrideHostsFileForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Enabled = false;
            try
            {
                OverrideHostsFile();
                MessageBox.Show("Override Successful");
            }
            catch (Exception)
            {
                MessageBox.Show("Override failed");
            }
            Enabled = true;
        }

        private void OverrideHostsFile()
        {
            const string link1 = "http://winhelp2002.mvps.org/hosts.txt";
            const string link2 = "https://someonewhocares.org/hosts/zero/hosts";
            var hostsFileLines = File.ReadAllLines(textBox1.Text);
            var custom = "";

            if (hostsFileLines.Contains("#<custom>") && hostsFileLines.Contains("#</custom>"))
            {
                string line;
                var i = 0;
                while ((line = hostsFileLines[i++]) != "#</custom>")
                {
                    custom += (line + "\n");
                }

                custom += line + "\n\n";
            }
            else
            {
                custom = "#<custom>\n#Add custom ip addresses here\n#0.0.0.0 50.50.50.50\n\n#</custom>\n\n";
            }

            custom += (new WebClient()).DownloadString(link2) + "\n\n";
            custom += (new WebClient()).DownloadString(link1);

            using (var v = new StreamWriter(File.Open("C:/Windows/system32/drivers/etc/hosts", FileMode.Create)))
            {
                v.WriteLine(custom);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program by youstinus.\nBlock malicious sites, ads and other harmful ip addresses.\nOverride hosts file using combination from the newest collections.\nReferences:\nhttps://someonewhocares.org/hosts/zero/hosts\nhttp://winhelp2002.mvps.org/hosts.txt");
        }
    }
}

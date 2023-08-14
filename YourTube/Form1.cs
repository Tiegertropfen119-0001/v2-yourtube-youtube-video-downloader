using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace YourTube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "Youtube Url")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete("loader.bat");
            }
            catch
            {
                
            }
            tx_cmdstring.Text = "";
            tx_cmdstring.Text += " ";
            tx_cmdstring.Text += textBox1.Text;
           
        
            if (comboBox1.Text == "Playlist Yes")
            {
                tx_cmdstring.Text += " --yes-playlist";
              

            }
            else
            {
                tx_cmdstring.Text += " --no-playlist";
            }
            if (comboBox2.Text == "MP3")
            {
                tx_cmdstring.Text += " --audio-format mp3 -x";
               

            }
            else
            {
                tx_cmdstring.Text += " -S res,ext:mp4:m4a --recode mp4";
                if (comboBox4.Text.Contains("144"))
                {
                    tx_cmdstring.Text += " -f ";
                    tx_cmdstring.Text += comboBox4.Text;
                }
                else if (comboBox4.Text.Contains("240"))
                {
                    tx_cmdstring.Text += " -f ";
                    tx_cmdstring.Text += comboBox4.Text;
                }
                else if (comboBox4.Text.Contains("360"))
                {
                    tx_cmdstring.Text += " -f ";
                    tx_cmdstring.Text += comboBox4.Text;
                }
                else if (comboBox4.Text.Contains("480"))
                {
                    tx_cmdstring.Text += " -f ";
                    tx_cmdstring.Text += comboBox4.Text;
                }
                else if (comboBox4.Text.Contains("720"))
                {
                    tx_cmdstring.Text += " -f ";
                    tx_cmdstring.Text += comboBox4.Text;
                }
                else if (comboBox4.Text.Contains("Best 4k / 1080"))
                {

                }

            }
           

            if (comboBox3.Text == "ID")
            {
                tx_cmdstring.Text += " -o %(id)s.%(ext)s";
            }
            else if (comboBox3.Text == "Title")
            {
                tx_cmdstring.Text += " -o %(title)s.%(ext)s";
            }
            else if (comboBox3.Text == "Title + ID")
            {
                tx_cmdstring.Text += " -o %(title)s.%(ext)s.%(id)s";
            }
            if(comboBox5.Text == "Add Thumbnail")
            {
                tx_cmdstring.Text += " --embed-thumbnail";
            }else if (comboBox5.Text == "Add Metadata")
            {
                tx_cmdstring.Text += " --embed-metadata";
                tx_cmdstring.Text += " --xattrs";
            }
            else if (comboBox5.Text == "Add Metadata + Thumbnail")
            {
                tx_cmdstring.Text += " --embed-thumbnail";
                tx_cmdstring.Text += " --embed-metadata";
                tx_cmdstring.Text += " --xattrs";
            }

            tx_cmdstring.Text += " --geo-bypass ";
            textBox2.Text = tx_cmdstring.Text;

            string strCmdText;
            strCmdText = tx_cmdstring.Text;
                 System.Diagnostics.Process.Start("yt-dlp.exe", strCmdText);
            }
        private void checkfiles()
        {
            File.Delete("youtube-dl.exe");
            Thread.Sleep(250);
            using (var client = new WebClient())
            {
                client.DownloadFile("https://github.com/yt-dlp/yt-dlp/releases/download/2023.07.06/yt-dlp.exe", "yt-dlp.exe");
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            checkfiles(); 
            toolTip1.SetToolTip(this.button1, "Download file or playlist");
            toolTip1.SetToolTip(this.textBox2, "yt-dlp.exe string");
            toolTip1.SetToolTip(this.textBox1, "Video or playlist url");
            toolTip1.SetToolTip(this.comboBox1, "Playlist Yes/No");
            toolTip1.SetToolTip(this.comboBox2, "Format MP3/MP4");
            toolTip1.SetToolTip(this.comboBox3, "Name format ID/Title/Title + ID");
            toolTip1.SetToolTip(this.comboBox4, "Video quality 144/240/360/480/720/Best 4k / 1080");
            toolTip1.SetToolTip(this.comboBox5, "Add Metadata");
            toolTip1.SetToolTip(this.pictureBox1, "Copy yt-dlp.exe string");
            toolTip1.SetToolTip(this.pictureBox2, "Click me ^.^");
            tx_cmdstring.Visible = false;
            try
            {
                Directory.CreateDirectory("files");
                Directory.CreateDirectory("files\\mp3");
                Directory.CreateDirectory("files\\mp4");
                Directory.CreateDirectory("files\\webm");
            }
            catch
            {

            }
            tx_cmdstring.Text = "";
            label1.Text = "";
            timer1.Start();
        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "MP3")
            {

                comboBox4.Hide();

            }
            else
            {

                comboBox4.Show();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "MP3")
            {

                comboBox4.Hide();

            }
            else
            {

                comboBox4.Show();
            }
        }

        private void tx_cmdstring_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tx_cmdstring.Text);
        }

      private void movefiles()
        {
            string paths = System.Reflection.Assembly.GetExecutingAssembly().Location;


            label1.Text = paths;
            label1.Text = label1.Text.Replace("YourTube.exe", "");
            //label1.Text = label1.Text.Replace("\\", "/");
            string filepath = label1.Text;
            DirectoryInfo d = new DirectoryInfo(filepath);

            foreach (var file in d.GetFiles("*.mp3"))
            {
                try
                {
                    Directory.Move(file.FullName, filepath + "\\files\\mp3\\" + file.Name);
                }
                catch
                {
                }

            }

            foreach (var file in d.GetFiles("*.mp4"))
            {
                try
                {
                    Directory.Move(file.FullName, filepath + "\\files\\mp4\\" + file.Name);
                }
                catch
                {
                }

            }

            foreach (var file in d.GetFiles("*.webm"))
            {
                try
                {
                    Directory.Move(file.FullName, filepath + "\\files\\webm\\" + file.Name);
                }
                catch
                {
                }

            }
        }
        public int mp3c = 0;
        public int mp4c = 0;
        public int webmc = 0;
        private void count()
        {
           
            string paths = System.Reflection.Assembly.GetExecutingAssembly().Location;


            label1.Text = paths;
            label1.Text = label1.Text.Replace("YourTube.exe", "");
            //label1.Text = label1.Text.Replace("\\", "/");
            string filepath = label1.Text;
            DirectoryInfo d = new DirectoryInfo(filepath);
            DirectoryInfo mp3 = new DirectoryInfo (filepath + "\\files\\mp3\\");
            DirectoryInfo mp4 = new DirectoryInfo(filepath + "\\files\\mp4\\");
            DirectoryInfo webm = new DirectoryInfo(filepath + "\\files\\webm\\");

            foreach (var file in mp3.GetFiles("*.mp3"))
            {
                try
                {
                    mp3c++;
                    label4.Text = mp3c.ToString();
                  
                }
                catch
                {
                }

            }

            foreach (var file in mp4.GetFiles("*.mp4"))
            {
                try
                {
                    mp4c++;
                    label7.Text = mp4c.ToString();
                }
                catch
                {
                }

            }

            foreach (var file in webm.GetFiles("*.webm"))
            {
                try
                {
                    webmc++;
                    label8.Text = webmc.ToString();
                }
                catch
                {
                }

            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox2.Text);
            }
            catch
            {

            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Tiegertropfen119-0001");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == false)
            {
                timer1.Stop();

            }
            else
            {
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            movefiles();

            webmc = 0;
            mp4c = 0;
            mp3c = 0;
            count();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            count();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", label1.Text+ @"files\mp3");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", label1.Text + @"files\mp4");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", label1.Text + @"files\webm");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/GyanD/codexffmpeg/releases/tag/6.0");
            MessageBox.Show("Download ffmpeg full build for windows and paste the ffmpeg.exe from bin folder into the YourTube folder");
        }
    }
}

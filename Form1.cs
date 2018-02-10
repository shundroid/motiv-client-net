using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace motiv_client
{
    public partial class Form1 : Form
    {
        int keydowns = 0;
        int clicks = 0;
        bool isQuit = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Hide();
            ServerUrlBox.Text = Properties.Settings.Default.ServerUrl;
            PasswordBox.Text = Properties.Settings.Default.Password;
            CountPerUploadBox.Value = Properties.Settings.Default.CountsPerUpload;
            MouseHook.AddEvent(hookMouseTest);
            MouseHook.Start();
            KeyboardHook.AddEvent(hookKeyboardTest);
            KeyboardHook.Start();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerUrl = ServerUrlBox.Text;
            Properties.Settings.Default.Password = PasswordBox.Text;
            Properties.Settings.Default.CountsPerUpload = (int)CountPerUploadBox.Value;
            Properties.Settings.Default.Save();
        }

        void hookMouseTest(ref MouseHook.StateMouse s)
        {
            if (s.Stroke == MouseHook.Stroke.LEFT_DOWN || s.Stroke == MouseHook.Stroke.RIGHT_DOWN)
            {
                clicks++;
                if (clicks >= Properties.Settings.Default.CountsPerUpload)
                {
                    Send();
                }
            }
        }

        void hookKeyboardTest(ref KeyboardHook.StateKeyboard s)
        {
            if (s.Stroke == KeyboardHook.Stroke.KEY_DOWN)
            {
                keydowns++;
                if (keydowns >= Properties.Settings.Default.CountsPerUpload)
                {
                    Send();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isQuit)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            clicksItem.Text = clicks.ToString() + " clicks";
            keydownsItem.Text = keydowns.ToString() + " keydowns";
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MouseHook.Stop();
            KeyboardHook.Stop();
            Post.Send(keydowns, clicks);
            isQuit = true;
            Application.Exit();
        }

        void Send()
        {
            Post.Send(keydowns, clicks);
            clicks = 0;
            keydowns = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace DAW_Presence
{
    public class Tray : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ContextMenuStrip ctx;
        private System.Windows.Forms.NotifyIcon icon;
        private System.Windows.Forms.ToolStripMenuItem whitelist, blacklist, rpc;
        public Tray()
        {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.ctx = new System.Windows.Forms.ContextMenuStrip();
            this.icon = new NotifyIcon(new Container());
            this.icon.ContextMenuStrip = ctx;
            InitializeMenu();
            icon.Visible = true;
            icon.Icon = new Icon("ableton.ico");
            Application.Run();
        }

        void InitializeMenu()
        {
            rpc = new ToolStripMenuItem();
            CheckRPCState();
            this.icon.ContextMenuStrip.Items.Add(rpc);
            this.icon.ContextMenuStrip.Items.Add("-"); // spacer
            /*this.icon.ContextMenuStrip.Items.Add("Add To Blacklist", null, this.ToggleBlacklist);
            blacklist = new ToolStripMenuItem();
            CheckBlacklist();
            this.icon.ContextMenuStrip.Items.Add(blacklist);
            this.icon.ContextMenuStrip.Items.Add("-"); // spacer
            this.icon.ContextMenuStrip.Items.Add("Add To Whitelist", null, this.ToggleBlacklist);
            whitelist = new ToolStripMenuItem();
            CheckWhitelist();
            this.icon.ContextMenuStrip.Items.Add(whitelist);
            */
            //this.icon.ContextMenuStrip.Items.Add("-"); // spacer
            this.icon.ContextMenuStrip.Items.Add("Exit", null, this.MenuExit);
        }
        void MenuExit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /*void ToggleBlacklist(object sender, EventArgs e)
        {
            blacklist.Text = "Disable Blacklist";
        }

        void CheckBlacklist()
        {
            blacklist.Text = "Enable Blacklist";
            blacklist.Click += new EventHandler(this.ToggleBlacklist);
        }

        void ToggleWhitelist(object sender, EventArgs e)
        {
            whitelist.Text = "Disable Whitelist";
        }

        void CheckWhitelist()
        {
            whitelist.Text = "Enable Whitelist";
            whitelist.Click += new EventHandler(this.ToggleWhitelist);
        }*/

        void ToggleRPC(object sender, EventArgs e)
        {
            //RPCActive = false;
            rpc.Text = "Enable RPC";
        }

        void CheckRPCState()
        {
            rpc.Text = "Disable RPC";
            rpc.Click += new EventHandler(this.ToggleRPC);
        }
    }
}

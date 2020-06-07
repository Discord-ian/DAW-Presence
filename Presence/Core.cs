using System;
using System.Threading;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;

namespace DAW_Presence
{
    class Core
    {
        private static DiscordRpcClient client;
        private static string current_info = null;

        static void Main(string[] args)
        {
            // stop there from being multiple copies running at the same time
            bool created = false;
            string mutexName = System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID.ToString();
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(false, mutexName, out created))
            {
                if (!created)
                {
                    return;
                }
            }

            //Tray main_tray = new Tray();
            //main_tray.WindowState = FormWindowState.Minimized; // hides form from being shown in taskbar
            //main_tray.ShowInTaskbar = false;
            Thread t_thread = new Thread(delegate() { Tray main_tray = new Tray();});
            t_thread.Start();
            MainLoop();
        }

        static void MainLoop()
        {
            DAW mainDAW = new DAW("Ableton");
            client = new DiscordRpcClient(mainDAW.Id);
            client.Logger = new ConsoleLogger() { Level = LogLevel.Info }; // log level (Info, Warning, Error)
            client.Initialize(); // start the rpc client
            // todo: ensure RPC connection is made before checking window titles
            while (true)
            {
                string title = DAW.GetProjectName(mainDAW.Template);
                Console.WriteLine("Project title: "+ title);
                Console.WriteLine(mainDAW.Title);
                if (!string.IsNullOrEmpty(title))
                {
                    if (mainDAW.Title != current_info)
                    {
                        updatePresence(mainDAW);
                    }
                }
                else
                {
                    // issue occurs if string is not reset: will never resume RPC until you open a different project 
                    current_info = null;
                    client.ClearPresence();
                }
                Thread.Sleep(15000);
                //Console.ReadKey(); // Only for dev purposes
            }
        }
        static void updatePresence(DAW info)
        {
            Console.WriteLine("Updating presence...");
            client.SetPresence(new RichPresence()
            {
                Details = info.Title,
                State = info.Info,
                Timestamps = Timestamps.Now,
                Assets = new Assets()
                {
                    LargeImageKey = info.Image.Name,
                    LargeImageText = info.Image.Text
                }
            });
            current_info = info.Title;
        }
    }
}

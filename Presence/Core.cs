using System;
using System.Threading;
using DiscordRPC;
using DiscordRPC.Logging;

namespace DAW_Presence
{
    class Core
    {
        private static DiscordRpcClient client;
        private static DAW mainDAW;
        private static string current_info = null;
        static void Main(string[] args)
        {
            MainLoop();
        }
        static void MainLoop()
        {
            DAW mainDAW = new DAW("Ableton");
            client = new DiscordRpcClient(mainDAW.Id);
            client.Logger = new ConsoleLogger() { Level = LogLevel.Error }; // log level (Debug, Info, Warning, Error)
            client.Initialize(); // start the rpc client
            while (true)
            {
                string title = DAW.GetProjectName(mainDAW.Template);
                Console.WriteLine("Project title: "+ title);
                if (!string.IsNullOrEmpty(title))
                {
                    if (mainDAW.Title != current_info)
                    {
                        updatePresence(mainDAW);
                    }
                }
                else
                {
                    current_info = null; // issue occurs if string is not reset: will never resume RPC until you open a different project 
                    client.ClearPresence();
                }
                //Console.WriteLine("Sleeping thread for 15 seconds...");
                //Thread.Sleep(15000);
                Console.ReadKey(); // Only for dev purposes
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

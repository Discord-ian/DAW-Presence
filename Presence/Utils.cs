using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DAW_Presence
{
    class Utils
    {
        public static string GetProcessList(string windowTitle) // TODO: Add filtering for processname as well, to ensure it is the DAW
        {
            Process[] plist = Process.GetProcesses();
            foreach (Process p in plist)
            {
                if (p.MainWindowTitle.Contains(windowTitle))
                {
                    return p.MainWindowTitle;
                }
            }
            return null;
        }

        public static bool CheckForUntitled(string windowTitle)
        {
            if (windowTitle.Contains("Untitled"))
            {
                return true;
            }

            return false;
        }
    }
}

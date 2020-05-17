using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DAW_Presence
{
    class DAW
    {
        private static string main_info;
        private static Image img;
        private static string _TitleTemplate;
        private static string _Title;
        private static string _Id;

        public DAW(string daw)
        {
            main_info = "Making Music"; // Going to be part of "global" config
            _Id = "609115046051840050";
            switch (daw.ToLower())
            {
                case "ableton":
                {
                    _TitleTemplate = "Ableton Live";
                    img = new Image("main", "Ableton Live");
                }
                    break;
            }
        }
        public Image Image
        {
            get { return img; }
        }
        public string Template
        {
            get { return _TitleTemplate; }
        }
        public string Info
        {
            get { return main_info; }
        }

        public string Title
        {
            get { return _Title;  }
            set { _Title = value; }
        }
        public string Id
        {
            get { return _Id; }
        }
        public static string GetProjectName(string template)
        {
            string temp_title = Utils.GetProcessList(template);
            if (string.IsNullOrEmpty(temp_title))
            {
                return null;
            }
            switch (template)
            {
                case "Ableton Live":
                {
                    if (!Utils.CheckForUntitled(temp_title))
                    {
                        string splitString = temp_title.Split("[")[0];
                        if (splitString.Contains("*"))
                        {
                            splitString = splitString.Split("*")[0];
                        }
                        splitString = splitString.Trim(); // remove any trailing spaces after stripping * 
                        _Title = splitString;
                        return splitString;
                    }
                    _Title = "Untitled";
                    return "Untitled";
                    }

            }
            return null;
        }
    }
    class Image
    {
        private string _Name;
        private string _Text;

        public Image(string imgName, string imgText)
        {
            _Name = imgName;
            _Text = imgText;
        }

        public string Name
            {
                get { return _Name; }
            }
        public string Text
        {
            get { return _Text; }
        }
    } 
}


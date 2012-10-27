using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Assets.Scripts
{
    class Song
    {
        public string Artist;
        public string Title;
        public string ChartPosition;
        public string PreviewUrl;

        public void Init()
        {
            FetchPreviewUrl();
        }

        public void FetchPreviewUrl()
        {
            string URI = "http://api.7digital.com/1.2/track/search?q=" + Artist + " " + Title + "&y&oauth_consumer_key=7d4vqmmnvjrt&country=GB&pagesize=2";
            WebClient webClient = new WebClient();
            string strXml = webClient.DownloadString(URI);
            int ix = strXml.IndexOf("track id=\"") + 10;
            string track_id = strXml.Substring(ix).Split('"')[0];
            //search by name
            //http://api.7digital.com/1.2/track/search?q=rihanna%20where%20have%20you%20been&y&oauth_consumer_key=7d4vqmmnvjrt&country=GB&pagesize=2
            //get preview
            //http://api.7digital.com/1.2/track/preview?trackid=10353559&oauth_consumer_key=7d4vqmmnvjrt

            PreviewUrl = track_id;
        }
    }
}

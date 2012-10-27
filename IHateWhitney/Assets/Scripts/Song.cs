using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

class Song
{
    public string Key; //a string that identifies the song. Usually Artist - Title
    public string Artist;
    public string Title;
    public int ChartPosition;
    public string PreviewUrl;
    public string ImageUrl;

    public void Init()
    {
        Key = Artist + " - " + Title;
    }

    public void FetchPreviewUrl()
    {
        string URI = "http://api.7digital.com/1.2/track/search?q=" + Artist + " " + Title + "&y&oauth_consumer_key=7d4vqmmnvjrt&country=GB&pagesize=2";
        WebClient webClient = new WebClient();
        string strXml = webClient.DownloadString(URI);
        int ix = strXml.IndexOf("track id=\"") + 10;
        string track_id = strXml.Substring(ix).Split('"')[0];

        PreviewUrl = "http://api.7digital.com/1.2/track/preview?trackid=" + track_id + "&oauth_consumer_key=7d4vqmmnvjrt";

        ix = strXml.IndexOf("<image>") + 7;
        ImageUrl = strXml.Substring(ix).Split('<')[0].Replace(".jpg", "0.jpg");

    }
}

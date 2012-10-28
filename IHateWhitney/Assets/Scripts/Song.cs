﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using MiniJSON;

internal class Song
{
    public static string CHART_URI = "http://developer.echonest.com/api/v4/song/search?api_key=CWXFYIFQ9HXXTW1K3&sort=song_hotttnesss-desc&bucket=song_hotttnesss&results=100";
    public string Key; //a string that identifies the song. Usually Artist - Title
    public string Artist;
    public string Title;
    public int ChartPosition;
    public string PreviewUrl;
    public string ImageUrl;
    public string Id;

    public void Init()
    {
        Key = Artist + " - " + Title;
    }

    public void FetchPreviewUrl()
    {
        string URI = "http://api.7digital.com/1.2/track/search?q=" + Artist + " " + Title + "&y&oauth_consumer_key=7d4vqmmnvjrt&country=GB&pagesize=2";
        WebClient webClient = new WebClient();
        string strXml = webClient.DownloadString(URI);
        webClient.Proxy = null;
        int ix = strXml.IndexOf("track id=\"") + 10;
        Id = strXml.Substring(ix).Split('"')[0];
        PreviewUrl = webClient.DownloadString("http://localhost/ogger/sample-class.php?trackid=" + Id).Trim(); //the response is another url
        
        ix = strXml.IndexOf("<image>") + 7;
        ImageUrl = strXml.Substring(ix).Split('<')[0].Replace(".jpg", "0.jpg");
    }

    public static Dictionary<string, Song> FetchSongs(string response)
    {
        Dictionary<string, Song> songs = new Dictionary<string, Song>();
        IDictionary search = (IDictionary)Json.Deserialize(response);

        IDictionary response2 = (IDictionary)search["response"];
        IList songsresponse = (IList)response2["songs"];
        int i = 1;
        foreach (IDictionary s in songsresponse)
        {
            Song song = new Song();
            song.Artist = (string)s["artist_name"];
            song.Title = (string)s["title"];
            song.Init();
            if (!songs.ContainsKey(song.Key))
            {
                song.ChartPosition = i++;
                song.FetchPreviewUrl();
                songs.Add(song.Key, song);
            }
        }
        return songs;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
 
public class ApiDude : MonoBehaviour {
    Dictionary<string, Song> songs = new Dictionary<string, Song>();

  void Start () {
   StartCoroutine("GetChart");     
  }
 
  IEnumerator GetChart() {
      WWW www = new WWW("http://developer.echonest.com/api/v4/song/search?api_key=CWXFYIFQ9HXXTW1K3&sort=song_hotttnesss-desc&bucket=song_hotttnesss&results=100");
    
    float elapsedTime = 0.0f;
    
    while (!www.isDone) {
      elapsedTime += Time.deltaTime;
      
      if (elapsedTime >= 10.0f) break;
      
      yield return null;  
    }
    
    if (!www.isDone || !string.IsNullOrEmpty(www.error)) {
      Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
      yield break;
    }
    
    string response = www.text;
    
    Debug.Log(elapsedTime + " : " + response);    
    
    IDictionary search = (IDictionary) Json.Deserialize(response);

    IDictionary response2 = (IDictionary)search["response"];
    IList songsresponse = (IList)response2["songs"];
    int i = 1;
    foreach (IDictionary s in songsresponse) {
        Song song = new Song();
        song.Artist = (string) s["artist_name"];
        song.Title = (string) s["title"];
        song.Init();
        if (!songs.ContainsKey(song.Key))
        {
            song.ChartPosition = i++;
            song.FetchPreviewUrl();
            songs.Add(song.Key, song);
        }
    }

    foreach (Song song in songs.Values)
    {
        Debug.Log(song.Key + " : " + song.ImageUrl);
    }
  }   
}

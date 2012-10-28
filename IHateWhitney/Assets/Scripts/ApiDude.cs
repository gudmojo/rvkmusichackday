using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ApiDude : MonoBehaviour {

    void Start () {
    StartCoroutine("GetChart");     
  }
 
  IEnumerator GetChart() {
      WWW www = new WWW(Song.CHART_URI);
    
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

    Dictionary<string, Song> songs = Song.FetchSongs(response);

    foreach (Song song in songs.Values)
    {
        Debug.Log(song.Id + " : " + song.PreviewUrl);
    }
  }
}

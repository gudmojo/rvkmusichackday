using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
 
public class ApiDude : MonoBehaviour {
  void Start () {
		//http://developer.echonest.com/api/v4/song/search?api_key=CWXFYIFQ9HXXTW1K3&sort=song_hotttnesss-desc&bucket=song_hotttnesss&results=10
             //search by name
            //http://api.7digital.com/1.2/track/search?q=rihanna%20where%20have%20you%20been&y&oauth_consumer_key=7d4vqmmnvjrt&country=GB&pagesize=2
            //get preview
            //http://api.7digital.com/1.2/track/preview?trackid=10353559&oauth_consumer_key=7d4vqmmnvjrt

            //get image

   StartCoroutine("GetTwitterUpdate");     
  }
 
  IEnumerator GetTwitterUpdate() {
      WWW www = new WWW("http://developer.echonest.com/api/v4/song/search?api_key=CWXFYIFQ9HXXTW1K3&sort=song_hotttnesss-desc&bucket=song_hotttnesss&results=10");
    
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
    Debug.Log(response2.GetType());
    IList songs = (IList)response2["songs"];
    
    foreach (IDictionary song in songs) {
      Debug.Log(string.Format("tweet: {0} : {1}", song["artist_name"], song["title"]));
    } 
  }   
}
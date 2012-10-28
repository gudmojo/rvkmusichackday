using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject artistPrefab;
    public GameObject lightPrefab;
    public GameObject cubePrefab;
    public int artistCount;
    public int lightCount;
    public Transform ground;
    Dictionary<string, Song> songs;

    private IEnumerable<Vector3> YieldRandomLocationsOnGround(int points, Vector3 offset)
    {
        var realGroundSize = Vector3.Scale(ground.GetComponent<MeshFilter>().mesh.bounds.extents, ground.transform.localScale);
        var spawnPoint = ground.transform.position + offset - realGroundSize * 0.5f;
        while (points > 0)
        {
            // Change the spawn point
            var newRand = Vector2.Scale(new Vector2(realGroundSize.x, realGroundSize.z), Random.insideUnitCircle);
            spawnPoint.x = ground.transform.position.x + newRand.x;
            spawnPoint.z = ground.transform.position.y + newRand.y;

            yield return spawnPoint;

            points -= 1;
        }
    }

    // Use this for initialization
    private void Start()
    {
        SpawnStuff();
        //FetchSongs();
    }

    private void FetchSongs()
    {
        StartCoroutine("GetChart");
    }

    public IEnumerator GetChart()
    {
        WWW www = new WWW(Song.CHART_URI);

        float elapsedTime = 0.0f;

        while (!www.isDone)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 10.0f) break;
            yield return new WaitForSeconds(0.2f);
        }

        if (!www.isDone || !string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
            yield break;
        }

        string response = www.text;
        Debug.Log(elapsedTime + " : " + response);
        songs = Song.FetchSongs(response);
        SpawnSongs();
    }

    /// <summary>
    /// Called once we have songs, go through the spawned artists and replace with stuff from songs
    /// </summary>
    private void SpawnSongs()
    {
        foreach (var randomPoint in YieldRandomLocationsOnGround(songs.Count, Vector3.up * 1.5f))
        {
            var groupPos = 5f * Random.insideUnitCircle;
            var go = (GameObject)Instantiate(artistPrefab, randomPoint, Quaternion.identity);
            go.transform.parent = this.transform;
        }

        var dataFetchers = GetComponentsInChildren<AsyncDataFetch>();
        int idx = 0;

        Debug.Log("dataFetchers length is " + dataFetchers.Length);
        foreach (var song in songs.Values)
        {
            if (idx < dataFetchers.Length)
            {
                var fetcher = dataFetchers[idx];
                Debug.Log("About to ask child to fetch textures");
                Debug.Log("Image url is: " + song.ImageUrl);
                fetcher.Fetch(song.ImageUrl, fetcher.ImageFetchCallback);
            }
            idx++;
        }
    }

    private void SpawnStuff()
    {
        /*var go = (GameObject)Instantiate(artistPrefab, new Vector3(5, 0.5f, 5), Quaternion.identity);
        go.transform.parent = this.transform;
         * */

        foreach (var randomPoint in YieldRandomLocationsOnGround(lightCount, Vector3.up * 10f))
        {
            var discoLight = (GameObject)Instantiate(lightPrefab, randomPoint, Quaternion.LookRotation(Vector3.down));
            discoLight.transform.parent = this.transform;
        }

        // Spawn some cubes and stuff
        foreach (var randomPoint in YieldRandomLocationsOnGround(lightCount, Vector3.up * 1.2f))
        {
            var discoLight = (GameObject)Instantiate(cubePrefab, randomPoint, Quaternion.identity);
            discoLight.transform.parent = this.transform;
        }
    }
}
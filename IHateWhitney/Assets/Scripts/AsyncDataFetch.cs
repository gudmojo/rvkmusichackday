using System.Collections;
using UnityEngine;

public class AsyncDataFetch : MonoBehaviour
{
    WWW seeker;
    public bool isDone;

    public void Fetch(string uri, System.Action callBack)
    {
        seeker = new WWW(uri);
        isDone = false;
        StartCoroutine(GetData(callBack));
    }

    public void ImageFetchCallback()
    {
        var image = seeker.texture;
        renderer.material.mainTexture = image;
        isDone = true;
    }

    private IEnumerator GetData(System.Action callBack)
    {
        while (!seeker.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log(seeker.text);

        string redirection = "";
        if (seeker.responseHeaders.TryGetValue("LOCATION", out redirection))
        {
            seeker = new WWW(redirection);
            yield return GetData(callBack);
        }

        if (seeker.error != null && seeker.error.Length > 0)
        {
            Debug.Log(seeker.error);
            Debug.Log(seeker.responseHeaders["Location"]);
        }

        while (!seeker.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Final url was: " + seeker.url);
        callBack();

        yield return null;
    }

    public void AudioCallback()
    {
        audio.clip = seeker.audioClip;
        isDone = true;
    }
}
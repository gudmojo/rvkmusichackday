using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour
{
    public List<Color> colors;
    public float secondsPerColor;

    private float timeStamp;

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeSinceLevelLoad - timeStamp > secondsPerColor)
        {
            timeStamp = Time.timeSinceLevelLoad;
            this.light.color = colors[Random.Range(0, colors.Count - 1)];
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject artistPrefab;
    public GameObject lightPrefab;
    public int artistCount;
    public int lightCount;
    public Transform ground;

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
        foreach (var randomPoint in YieldRandomLocationsOnGround(artistCount, Vector3.up * 0.5f))
        {
            var groupPos = 5f * Random.insideUnitCircle;
            var go = (GameObject)Instantiate(artistPrefab, randomPoint, Quaternion.identity);
            go.transform.parent = this.transform;
        }

        foreach (var randomPoint in YieldRandomLocationsOnGround(lightCount, Vector3.up * 10f))
        {
            var discoLight = (GameObject)Instantiate(lightPrefab, randomPoint, Quaternion.LookRotation(Vector3.down));
            discoLight.transform.parent = this.transform;
        }

        // Spawn some cubes and stuff
    }
}
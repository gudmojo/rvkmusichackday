using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class RocketBehavior : MonoBehaviour
{
    public GameObject spawnOnCollision;

    private void Start()
    {
        this.rigidbody.AddForce(transform.forward * 100f);
    }

    // Update is called once per frame
    private void Update()
    {
        this.rigidbody.AddForce(Random.onUnitSphere * 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BillboardBehavior>() != null)
        {
            Destroy(this.gameObject);
        }
    }
}
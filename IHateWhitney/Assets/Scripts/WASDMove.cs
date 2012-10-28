using UnityEngine;

public class WASDMove : MonoBehaviour
{
    public float horizontalRate;
    public float verticalRate;
    public Transform target;

    // Use this for initialization
    private void Start()
    {
        if (target == null)
        {
            target = this.transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * horizontalRate * Time.deltaTime;
        var deltaY = Input.GetAxis("Vertical") * verticalRate * Time.deltaTime;

        var translation = Vector3.right * deltaX + Vector3.forward * deltaY;

        translation = transform.TransformDirection(translation);
        transform.position += translation;
    }
}
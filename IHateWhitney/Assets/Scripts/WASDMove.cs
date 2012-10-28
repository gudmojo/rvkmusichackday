using UnityEngine;

public class WASDMove : MonoBehaviour
{
    public float horizontalRate;
    public float verticalRate;

    // Update is called once per frame
    private void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * horizontalRate * Time.deltaTime;
        var deltaY = Input.GetAxis("Vertical") * verticalRate * Time.deltaTime;

        var translation = deltaX * transform.right + deltaY * transform.forward;

        // Get the amount of rotation we have around the world Y axis
        var wRot = Quaternion.Euler(new Vector3(0, transform.localRotation.eulerAngles.y, 0));

        // Rotate the translation vector with that rotation
        translation = wRot * translation;

        //translation = target.TransformDirection(translation);

        transform.Translate(translation);
    }
}
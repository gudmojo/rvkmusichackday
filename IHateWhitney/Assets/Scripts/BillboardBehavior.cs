using UnityEngine;

public class BillboardBehavior : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.mainCamera.transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        this.transform.LookAt(cameraTransform);
        var euler = this.transform.eulerAngles;
        this.transform.rotation = Quaternion.Euler(90, euler.y, euler.z);
    }
}
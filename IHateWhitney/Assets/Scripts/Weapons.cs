using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject rocket;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var go = (GameObject)Instantiate(rocket, this.transform.position, Quaternion.LookRotation(transform.forward));
            //go.transform.Translate(this.transform.forward);
        }
    }
}
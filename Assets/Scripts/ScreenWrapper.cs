using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    float leftConstraint = Screen.width;
    float rightConstraint = Screen.width;
    float bottomConstraint = Screen.height;
    float topConstraint = Screen.height;

    float buffer = 1.0f;
    float distanceZ;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, distanceZ)).y;
        topConstraint = cam.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, distanceZ)).y;
    }

    private void FixedUpdate()
    {
        if(transform.position.x < leftConstraint - buffer)
        {
            transform.position = new Vector3(rightConstraint - 0.10f, transform.position.y, transform.position.z);
        }

        if(transform.position.x > rightConstraint + buffer)
        {
            transform.position = new Vector3(leftConstraint, transform.position.y, transform.position.z);
        }

        if(transform.position.y < bottomConstraint - buffer)
        {
            transform.position = new Vector3(transform.position.x, topConstraint + buffer, transform.position.z);
        }

        if(transform.position.y > topConstraint + buffer)
        {
            transform.position = new Vector3(transform.position.x, bottomConstraint - buffer, transform.position.z);
        }
    }
}

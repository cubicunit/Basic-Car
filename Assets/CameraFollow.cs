using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        this.transform.LookAt(target);
    }
}

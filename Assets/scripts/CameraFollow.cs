using UnityEngine;
using BasicCarParking;

public class CameraFollow : MonoBehaviour
{
    public GameObject gm; 

    private Transform target;

    public float smoothSpeed = 0.20f;

    [Header("Third Person View Offset")]
    public Vector3 thirdPersonOffset;

    public VIEWTYPE viewType = VIEWTYPE.THIRD_PERSON_VIEW;

    private void Start() {
        target = gm.GetComponent<GameMaster>().player.transform;
    }

    private void firstPersonView() {
        Transform lookingAt = target.Find("LookingAt");

        Vector3 desiredPosition = target.Find("First Person View Point").position;
        this.transform.position = desiredPosition;

        this.transform.LookAt(lookingAt);   
    }

    private void topDownView() {
        Vector3 desiredPosition = target.Find("Top Down View Point").position;
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        Vector3 z = new Vector3(0,0,1);
        Matrix4x4 t = target.transform.localToWorldMatrix;
        Vector3 up = t.MultiplyVector(z);

        this.transform.LookAt(target, up);
    }

    private void thirdPersonView() {
        Vector3 desiredPosition = target.position + thirdPersonOffset;
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        this.transform.LookAt(target);
    }

    public void changeView(VIEWTYPE view) {
        this.viewType = view;
    }


    // Update is called once per frame
    private void FixedUpdate() {
        switch (viewType) {
            case VIEWTYPE.FIRST_PERSON_VIEW:
                firstPersonView();
                break;
            case VIEWTYPE.TOP_DOWN_VIEW:
                topDownView();
                break;
            case VIEWTYPE.THIRD_PERSON_VIEW:
                thirdPersonView();
                break;
            default:
                thirdPersonView();
                break;
        }

    }
}

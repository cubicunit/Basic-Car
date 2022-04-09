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
        Transform viewPoint = target.Find("First Person View Point");
        Transform lookAt = viewPoint.Find("LookAt");

        Vector3 desiredPosition = viewPoint.position;
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        this.transform.LookAt(lookAt.position);   
    }

    private void topDownView() {
        Vector3 desiredPosition = target.Find("Top Down View Point").position;
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        this.transform.LookAt(target, target.forward);
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

    private void transitViewPoint(Vector3 desiredPosition){
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        this.transform.LookAt(target);
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

using UnityEngine;
using BasicCarParking;

public class CameraFollow : MonoBehaviour
{
    public Transform target { get; set; }

    public float smoothSpeed = 0.9f;
    public float lookSpeed = 0.2f;

    public VIEWTYPE viewType = VIEWTYPE.THIRD_PERSON_VIEW;

    [SerializeField]
    public GameObject carMirrorsUI;

    private void Start() {
        carMirrorsUI.SetActive(viewType == VIEWTYPE.FIRST_PERSON_VIEW);
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
        Vector3 topDownOffset = target.GetComponent<CarController>().topDownOffset;
        Vector3 desiredPosition = target.position + topDownOffset;
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        this.transform.LookAt(target, target.forward);
    }

    private void thirdPersonView() {
        Vector3 thirdPersonOffset = target.GetComponent<CarController>().thirdPersonOffset;
        Vector3 desiredPosition = target.forward*thirdPersonOffset.z+target.position+new Vector3(0,thirdPersonOffset.y,0);
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        this.transform.LookAt(target);
    }

    public void changeView(VIEWTYPE view) {
        this.viewType = view;

        if (view == VIEWTYPE.FIRST_PERSON_VIEW) {
            carMirrorsUI.SetActive(true);
        } else {
            carMirrorsUI.SetActive(false);
        }
    }

    public void applyLookToVisual() {
        Transform viewPoint = this.target.Find("First Person View Point");

        CarController carCtrl = this.target.GetComponent<CarController>();
        float maxLeftAngle = carCtrl.maxLeftAngle;
        float maxRightAngle = carCtrl.maxRightAngle;
        LOOKDIR lookAt = carCtrl.lookAt;

        if (lookAt == LOOKDIR.LEFT) {
            Quaternion carQuaterion = this.target.rotation;
            Quaternion desiredQuaterion = carQuaterion * Quaternion.AngleAxis(maxLeftAngle, transform.up);

            viewPoint.rotation = Quaternion.Lerp(viewPoint.rotation, desiredQuaterion, lookSpeed);
        } else if (lookAt == LOOKDIR.CENTER) {
            Quaternion carQuaterion = this.target.rotation;
            Quaternion desiredQuaterion = carQuaterion * Quaternion.AngleAxis(0, transform.up);

            viewPoint.rotation = Quaternion.Lerp(viewPoint.rotation, desiredQuaterion, lookSpeed);  
        } else if (lookAt == LOOKDIR.RIGHT) {
            Quaternion carQuaterion = this.target.rotation;
            Quaternion desiredQuaterion = carQuaterion * Quaternion.AngleAxis(maxRightAngle, transform.up);

            viewPoint.rotation = Quaternion.Lerp(viewPoint.rotation, desiredQuaterion, lookSpeed);
        } 
    }


    // Update is called once per frame
    private void FixedUpdate() {
        switch (viewType) {
            case VIEWTYPE.FIRST_PERSON_VIEW:
                firstPersonView();
                applyLookToVisual();
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

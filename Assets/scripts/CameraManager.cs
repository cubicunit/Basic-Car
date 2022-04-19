using UnityEngine;
using BasicCarParking;

public class CameraManager : MonoBehaviour
{
    public Transform target;

    public GameObject carMirrorsUI;

    [SerializeField]
    public float smoothSpeed = 0.9f;

    [SerializeField]
    public float lookSpeed = 0.2f;

    public VIEWTYPE viewType = VIEWTYPE.THIRD_PERSON_VIEW;

    public void changeNextView() {
        int v = (int)viewType;
        viewType = (VIEWTYPE)((++v) % 3);

        if (viewType == VIEWTYPE.FIRST_PERSON_VIEW) {
            //Enable Mirror UI
            carMirrorsUI.SetActive(true);
        } else {
            //Disable Mirror 
            carMirrorsUI.SetActive(false);
        }
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

    // private void thirdPersonView() {
    //     Vector3 thirdPersonOffset = target.GetComponent<CarController>().thirdPersonOffset;
    //     Vector3 desiredPosition = target.forward*thirdPersonOffset.z+target.position+new Vector3(0,thirdPersonOffset.y,0);
    //     Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
    //     this.transform.position = smoothPosition;

    //     this.transform.LookAt(target);
    // }

    private void thirdPersonView() {
        Vector3 thirdPersonOffset = target.GetComponent<CarController>().thirdPersonOffset;
        // Vector3 desiredPosition = target.forward*thirdPersonOffset.z+target.position+new Vector3(0,thirdPersonOffset.y,0);
        Vector3 desiredPosition = target.position + thirdPersonOffset;
        Vector3 smoothPosition = Vector3.Lerp(this.transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothPosition;

        this.transform.LookAt(target);
    }

    public void applyLookToVisual() {
        Transform viewPoint = target.Find("First Person View Point");

        CarController carCtrl = target.GetComponent<CarController>();
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

    private void Update() {
        if (SimpleInput.GetButtonDown("Change View")) {
            changeNextView();
        }
    }

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

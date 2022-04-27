using UnityEngine;
using BasicCarParking;
using UnityEngine.InputSystem;

public class MainCamCtrl : MonoBehaviour
{
    [SerializeField] public Transform target;

    [SerializeField] public float smoothSpeed = 0.9f;

    [SerializeField] public float lookSpeed = 0.2f;

    [SerializeField] public VIEWTYPE viewType = VIEWTYPE.THIRD_PERSON_VIEW;
    [SerializeField] private CAMERAMODE camMode = CAMERAMODE.PLAY;

    [Space(15)]
    public GameObject carMirrorsUI;
    public InputActionAsset inputActionAsset;

    [Space(15)]
    [SerializeField] public Vector3 maxClamp;
    [SerializeField] public Vector3 minClamp;

    public void look(LOOKDIR dir) {
        target.GetComponent<CarController>().look(dir);
    }

    public void changeNextView(InputAction.CallbackContext context) {
        if (context.started) {
            int v = (int)viewType;
            viewType = (VIEWTYPE)((++v) % 3);

            if (viewType == VIEWTYPE.FIRST_PERSON_VIEW) {
                //Enable Mirror UI
                carMirrorsUI.SetActive(true);
                inputActionAsset.FindAction("Game Control/Select Car").Disable();
            } else {
                //Disable Mirror 
                carMirrorsUI.SetActive(false);
                inputActionAsset.FindAction("Game Control/Select Car").Enable();
            }

            camMode = CAMERAMODE.PLAY;
        }
    }

    public void setCarMirrorsUI(GEARTYPE gear){
        if (viewType != VIEWTYPE.FIRST_PERSON_VIEW) {
            if (gear == GEARTYPE.REVERSE) {
                carMirrorsUI.SetActive(true);
            } else {
                carMirrorsUI.SetActive(false);
            }
        }
    }


    public void panCamera(Vector2 delta) {
        
        if (viewType == VIEWTYPE.FIRST_PERSON_VIEW){
            if (delta.x > 0) {
                this.look(LOOKDIR.RIGHT);
            } else {
                this.look(LOOKDIR.LEFT);
            }
        } else {
            camMode = CAMERAMODE.SPECTATE;
            delta = delta * -0.01f;
            if (viewType == VIEWTYPE.TOP_DOWN_VIEW) {
                Vector3 delta3d = new Vector3(delta.x, delta.y, 0);
                this.transform.Translate(delta3d, Space.Self);
            } else if (viewType == VIEWTYPE.THIRD_PERSON_VIEW) {
                Vector3 delta3d = new Vector3(delta.x, 0, delta.y);
                this.transform.Translate(delta3d, Space.World);
            }

            Vector3 newPos = this.transform.position;
            newPos.x = Mathf.Clamp(newPos.x, minClamp.x, maxClamp.x);
            newPos.z = Mathf.Clamp(newPos.z, minClamp.z, maxClamp.z);
            if (newPos != this.transform.position){
                this.transform.position = newPos;
            }
        }
    }

    public void resetCamMode(Vector2 position) {
        camMode = CAMERAMODE.PLAY;
        look(LOOKDIR.CENTER);
    }

    private void firstPersonView() {
        Transform viewPoint = target.Find("First Person View Point");
        Transform lookAt = viewPoint.Find("LookAt");

        this.transform.position = viewPoint.position;
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
    //     Vector3 smoothPosition = Vector3.Lerp(mainCamera.transform.position, desiredPosition, smoothSpeed);
    //     mainCamera.transform.position = smoothPosition;

    //     mainCamera.transform.LookAt(target);
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
        float maxLeftAngle = carCtrl.maxLookLeft;
        float maxRightAngle = carCtrl.maxLookRight;
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
    // private void Update() {
    //     if (SimpleInput.GetButtonDown("Change View")) {
    //         changeNextView();
    //     }
    // }    

    private void FixedUpdate() {
        if (camMode == CAMERAMODE.SPECTATE) return;

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

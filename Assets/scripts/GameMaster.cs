using UnityEngine;
using BasicCarParking;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    public GameObject mainCamera;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    public GameObject gearStepper;


    public void changeView() {
        VIEWTYPE viewType = mainCamera.GetComponent<CameraFollow>().viewType;

        int v = (int)viewType;
        viewType  = (VIEWTYPE)((++v) % 3);
        mainCamera.GetComponent<CameraFollow>().changeView(viewType);
    }

    private void Update() {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        player.GetComponent<CarController>().gasBrake(inputVertical);
        player.GetComponent<CarController>().steer(inputHorizontal);

        if (Input.GetButtonDown("Gear Up")) {
            gearStepper.GetComponent<GearStepper>().changeGear(GEARDIR.UP);
        } else if (Input.GetButtonDown("Gear Down")) {
            gearStepper.GetComponent<GearStepper>().changeGear(GEARDIR.DOWN);
        }
    }

}

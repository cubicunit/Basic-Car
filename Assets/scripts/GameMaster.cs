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

    public void setGear(GEARBOX gear) {
        gearStepper.GetComponent<GearStepper>().setGear(gear);
        player.GetComponent<CarController>().setGear(gear);
    }

    public void changeGear(GEARDIR dir) {
        gearStepper.GetComponent<GearStepper>().changeGear(dir);
        player.GetComponent<CarController>().changeGear(dir);
    }

    private void Update() {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        player.GetComponent<CarController>().gasBrake(inputVertical);
        player.GetComponent<CarController>().steer(inputHorizontal);

        if (Input.GetButtonDown("Gear Up")) {
            changeGear(GEARDIR.UP);
        } else if (Input.GetButtonDown("Gear Down")) {
            changeGear(GEARDIR.DOWN);
        }
    }

}

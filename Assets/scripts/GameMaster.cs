using UnityEngine;
using BasicCarParking;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    public GameObject mainCamera;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    public GearShifter gearStepper;

    public void changeView() {
        VIEWTYPE viewType = mainCamera.GetComponent<CameraFollow>().viewType;

        int v = (int)viewType;
        viewType  = (VIEWTYPE)((++v) % 3);
        mainCamera.GetComponent<CameraFollow>().changeView(viewType);
    }

    public void setGear(GEARTYPE gear) {
        gearStepper.GetComponent<GearShifter>().setGear(gear);
        player.GetComponent<CarController>().setGear(gear);
    }

    public void changeGear(GEARDIR dir) {
        gearStepper.GetComponent<GearShifter>().changeGear(dir);
        player.GetComponent<CarController>().changeGear(dir);
    }

    public void look(LOOKDIR dir) {
        player.GetComponent<CarController>().look(dir);
    }

    private void Update() {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        player.GetComponent<CarController>().gasBrake(inputVertical);
        player.GetComponent<CarController>().steer(inputHorizontal);

        if (Input.GetButton("Look Left")){
            look(LOOKDIR.LEFT);
        } else if (Input.GetButton("Look Right")){
            look(LOOKDIR.RIGHT);
        } else {
            look(LOOKDIR.CENTER);
        }

        if (Input.GetButtonDown("Gear Up")) {
            changeGear(GEARDIR.UP);
        } else if (Input.GetButtonDown("Gear Down")) {
            changeGear(GEARDIR.DOWN);
        }
    }

}

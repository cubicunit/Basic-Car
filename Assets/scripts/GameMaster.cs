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

    private void Start() {
        mainCamera.GetComponent<CameraFollow>().target = player.transform;
    }

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
        float inputHorizontal = SimpleInput.GetAxis("Horizontal");
        float inputVertical = SimpleInput.GetAxis("Vertical");

        player.GetComponent<CarController>().gasBrake(inputVertical);
        player.GetComponent<CarController>().steer(inputHorizontal);

        if (SimpleInput.GetButtonDown("Change View")) {
            changeView();
        }

        if (SimpleInput.GetButton("Look Left")){
            look(LOOKDIR.LEFT);
        } else if (SimpleInput.GetButton("Look Right")){
            look(LOOKDIR.RIGHT);
        } else {
            look(LOOKDIR.CENTER);
        }

        if (SimpleInput.GetButtonDown("Gear Up")) {
            changeGear(GEARDIR.UP);
        } else if (SimpleInput.GetButtonDown("Gear Down")) {
            changeGear(GEARDIR.DOWN);
        }
    }

}

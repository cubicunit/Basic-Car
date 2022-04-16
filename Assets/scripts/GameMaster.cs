using UnityEngine;
using BasicCarParking;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    public GameObject mainCamera;

    public GameObject player;

    [SerializeField]
    public GameObject[] candidates;
    [SerializeField]
    private int currentCarIdx;

    [SerializeField]
    public GameObject[] parks;


    [SerializeField]
    public GearShifter gearShifter;

    [SerializeField] 
    public GameObject nextCarButton;

    [SerializeField]
    public Transform steeringWheel;

    private void Start() {
        mainCamera.GetComponent<CameraFollow>().target = player.transform;
        foreach(GameObject car in candidates) {
            car.GetComponent<CarController>().takeControl(false);
        }

        if (candidates.Length > 1) {
            nextCarButton.SetActive(true);
        } else {
            nextCarButton.SetActive(false);
        }

        currentCarIdx = 0;
        candidates[0].GetComponent<CarController>().takeControl(true);
        player = candidates[0];
    }

    public void changeView() {
        VIEWTYPE viewType = mainCamera.GetComponent<CameraFollow>().viewType;

        int v = (int)viewType;
        viewType  = (VIEWTYPE)((++v) % 3);
        mainCamera.GetComponent<CameraFollow>().changeView(viewType);
    }

    public void setGear(GEARTYPE gear) {
        gearShifter.GetComponent<GearShifter>().setGear(gear);
        player.GetComponent<CarController>().setGear(gear);
    }

    public void changeGear(GEARDIR dir) {
        gearShifter.GetComponent<GearShifter>().changeGear(dir);
        player.GetComponent<CarController>().changeGear(dir);
    }

    public void look(LOOKDIR dir) {
        player.GetComponent<CarController>().look(dir);
    }

    public void nextCar(){
        int maxCarNum = this.candidates.Length;
        currentCarIdx = ++currentCarIdx%maxCarNum;

        GameObject successor = candidates[currentCarIdx];
        changeCar(successor);
    }

    public void changeCar(GameObject targetCar) {
        player.GetComponent<CarController>().takeControl(false);
        targetCar.GetComponent<CarController>().takeControl(true);

        player = targetCar;
        mainCamera.GetComponent<CameraFollow>().target = player.transform;
    }

    public void levelFinish() {
        SceneManager.LoadScene("Level2");
    }

    private void Update() {
        if (player.GetComponent<CarController>().isStop()) {
            steeringWheel.GetComponent<SimpleInputNamespace.SteeringWheel>().locked = true;
        } else {
            steeringWheel.GetComponent<SimpleInputNamespace.SteeringWheel>().locked = false;
        }

        if (parks[0].GetComponent<ParkTrigger>().isTargetCarParked()){
            levelFinish();
        }

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

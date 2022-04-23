using UnityEngine;
using BasicCarParking;
using SimpleInputNamespace;
using UnityEngine.InputSystem;

public class CarDriver : MonoBehaviour
{
    [SerializeField]
    public GameObject targetCar;

    [SerializeField]
    public Transform gearShifter;

    [SerializeField]
    public Transform steeringWheel;

    [SerializeField]
    public MainCamCtrl mainCamCtrl;

    public void pauseCar() {
        gearShifter.GetComponent<GearShifter>().setGear(GEARTYPE.PARK);
        targetCar.GetComponent<CarController>().setGear(GEARTYPE.PARK);
    }

    public void changeCar(GameObject newCar) {
        gearShifter.GetComponent<GearShifter>().setGear(GEARTYPE.PARK);

        //Do something on old car
        targetCar.GetComponent<CarController>().takeControl(false);

        //Do something on new car
        newCar.GetComponent<CarController>().takeControl(true);

        targetCar = newCar;
    }

    public void setGear(GEARTYPE gear) {
        CarController ctrl = targetCar.GetComponent<CarController>();
        GearShifter shifter = gearShifter.GetComponent<GearShifter>();

        ctrl.setGear(gear);
        shifter.setGear(gear);
    }

    // Update is called once per frame
    private void Update() {
        CarController ctrl = targetCar.GetComponent<CarController>();
        GearShifter shifter = gearShifter.GetComponent<GearShifter>();

        //Control Car
        steeringWheel.GetComponent<SteeringWheel>().locked = ctrl.isStop();

        if (SimpleInput.GetButton("Look Left")){
            mainCamCtrl.look(LOOKDIR.LEFT);
        } else if (SimpleInput.GetButton("Look Right")){
            mainCamCtrl.look(LOOKDIR.RIGHT);
        } else if (SimpleInput.GetButton("Look Center")){
            mainCamCtrl.look(LOOKDIR.CENTER);
        }

        float inputHorizontal = SimpleInput.GetAxis("Horizontal");
        float inputVertical = SimpleInput.GetAxis("Vertical");
        ctrl.gasBrake(inputVertical);
        ctrl.steer(inputHorizontal);
    }
}

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
            ctrl.look(LOOKDIR.LEFT);
        } else if (SimpleInput.GetButton("Look Right")){
            ctrl.look(LOOKDIR.RIGHT);
        } else {
            ctrl.look(LOOKDIR.CENTER);
        }

        if (SimpleInput.GetButtonDown("Gear Up")) {
            ctrl.changeGear(GEARDIR.UP);
            shifter.changeGear(GEARDIR.UP);
        } else if (SimpleInput.GetButtonDown("Gear Down")) {
            ctrl.changeGear(GEARDIR.DOWN);
            shifter.changeGear(GEARDIR.DOWN);
        }

        float inputHorizontal = SimpleInput.GetAxis("Horizontal");
        float inputVertical = SimpleInput.GetAxis("Vertical");
        ctrl.gasBrake(inputVertical);
        ctrl.steer(inputHorizontal);
    }
}

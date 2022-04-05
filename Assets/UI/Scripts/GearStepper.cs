using UnityEngine;
using UnityEngine.UI;
using BasicCarParking;

public class GearStepper : MonoBehaviour
{
    [SerializeField]
    public GameObject playerCar;

    [SerializeField]
    public Slider slider;

    [SerializeField]
    public GEARBOX gear = GEARBOX.PARK;

    public void onSilderValueChanged() {
        GEARBOX newGear = gear;

        if (0f <= slider.value && slider.value <= 15f) {       
            newGear = GEARBOX.PARK;
        } else if (15f < slider.value && slider.value <= 50f) {   
            newGear = GEARBOX.REVERSE;
        } else if (50f < slider.value && slider.value <= 85f) {          
            newGear = GEARBOX.NEUTRAL;
        } else if (85f < slider.value && slider.value <= 100f) {                           
            newGear = GEARBOX.DRIVE;             
        }

        if (newGear != gear) {
            gear = newGear;
            playerCar.GetComponent<CarController>().setGear(gear);
        }
    }

    public void setGear(GEARBOX gear) {
        this.gear = gear;
    }

    public void changeGear(GEARDIR dir) {
        int thisGear = (int)this.gear;
    
        if (dir == GEARDIR.UP) {
            thisGear++;
        } else {
            thisGear--;
        }

        this.gear = champGear(thisGear);
        playerCar.GetComponent<CarController>().setGear(gear);
    }

    private GEARBOX champGear(int gear) {
        int thisGear = gear;
        if (thisGear < (int)GEARBOX.PARK)  thisGear = (int)GEARBOX.PARK; 
        if (thisGear > (int)GEARBOX.DRIVE) thisGear = (int)GEARBOX.DRIVE;
        return (GEARBOX)thisGear;
    }

    private void Update() {
        switch (gear) {
            case GEARBOX.PARK:
                slider.value = 1;
                break;
            case GEARBOX.REVERSE:
                slider.value = 35;
                break;
            case GEARBOX.NEUTRAL:
                slider.value = 65;
                break;
            case GEARBOX.DRIVE:
                slider.value = 99;
                break;
        }
    }
}

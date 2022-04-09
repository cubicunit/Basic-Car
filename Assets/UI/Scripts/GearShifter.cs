using UnityEngine;
using UnityEngine.UI;
using BasicCarParking;

public class GearShifter : MonoBehaviour
{
    [SerializeField]
    public GameObject gm;

    [SerializeField]
    public Slider slider;

    [SerializeField]
    public GEARTYPE gear;

    private void Start() {
        this.gear = gm.GetComponent<GameMaster>().player.GetComponent<CarController>().gearBox;
    }

    public void onSilderValueChanged() {
        GEARTYPE newGear = gear;

        if (0f <= slider.value && slider.value <= 15f) {       
            newGear = GEARTYPE.PARK;
        } else if (15f < slider.value && slider.value <= 50f) {   
            newGear = GEARTYPE.REVERSE;
        } else if (50f < slider.value && slider.value <= 85f) {          
            newGear = GEARTYPE.NEUTRAL;
        } else if (85f < slider.value && slider.value <= 100f) {                           
            newGear = GEARTYPE.DRIVE;             
        }

        if (newGear != gear) {
            gear = newGear;
            gm.GetComponent<GameMaster>().setGear(gear);
        }
    }

    public void setGear(GEARTYPE gear) {
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
    }

    private GEARTYPE champGear(int gear) {
        int thisGear = gear;
        if (thisGear < (int)GEARTYPE.PARK)  thisGear = (int)GEARTYPE.PARK; 
        if (thisGear > (int)GEARTYPE.DRIVE) thisGear = (int)GEARTYPE.DRIVE;
        return (GEARTYPE)thisGear;
    }

    private void Update() {
        switch (gear) {
            case GEARTYPE.PARK:
                slider.value = 1;
                break;
            case GEARTYPE.REVERSE:
                slider.value = 35;
                break;
            case GEARTYPE.NEUTRAL:
                slider.value = 65;
                break;
            case GEARTYPE.DRIVE:
                slider.value = 99;
                break;
        }
    }
}

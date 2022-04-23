using UnityEngine;
using UnityEngine.UI;
using BasicCarParking;
using UnityEngine.Events;

public class GearShifter : MonoBehaviour
{
    [System.Serializable]
    public class GearEvent : UnityEvent<GEARTYPE> {} 

    [SerializeField]
    public Slider slider;

    [SerializeField]
    public GEARTYPE gear;

    [SerializeField]
    public GearEvent m_onGearChanged = new GearEvent();


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
            m_onGearChanged.Invoke(gear);
        }
    }

    public void pakeCar(){
        gear = GEARTYPE.PARK;
        m_onGearChanged.Invoke(gear);
    }

    public void setGear(GEARTYPE newGear) {
        gear = newGear;
    }

    public void changeGear(GEARDIR dir) {
        int thisGear = (int)gear;
    
        if (dir == GEARDIR.UP) {
            thisGear++;
        } else {
            thisGear--;
        }

        gear = champGear(thisGear);
    }

    private GEARTYPE champGear(int gear) {
        int thisGear = gear;
        if (thisGear < (int)GEARTYPE.PARK)  thisGear = (int)GEARTYPE.PARK; 
        if (thisGear > (int)GEARTYPE.DRIVE) thisGear = (int)GEARTYPE.DRIVE;
        return (GEARTYPE)thisGear;
    }

    private void Update() {
        if (SimpleInput.GetButtonDown("Gear Up")) {
            changeGear(GEARDIR.UP);
             m_onGearChanged.Invoke(gear);
        } else if (SimpleInput.GetButtonDown("Gear Down")) {
            changeGear(GEARDIR.DOWN);
             m_onGearChanged.Invoke(gear);
        }

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

using System.Collections;
using UnityEngine;
// using EasyUI.Dialogs;

public class ParkTrigger : MonoBehaviour
{
    [SerializeField] public Transform targetCar;

    [SerializeField] public bool withDir = false;

    public bool isTargetCarParked() {
        if (targetCar.GetComponent<CarController>().gearBox != BasicCarParking.GEARTYPE.PARK) {
            return false;
        }

        Vector3 fl = targetCar.Find("Bounds/B_fl").position;
        Vector3 fr = targetCar.Find("Bounds/B_fr").position;
        Vector3 rl = targetCar.Find("Bounds/B_rl").position;
        Vector3 rr = targetCar.Find("Bounds/B_rr").position;

        Bounds frontTrigger = this.transform.Find("Trigger Front").GetComponent<Collider>().bounds;
        Bounds rearTrigger = this.transform.Find("Trigger Rear").GetComponent<Collider>().bounds;

        if (withDir) {
            if (frontTrigger.Contains(fl)
                && frontTrigger.Contains(fr)
                && rearTrigger.Contains(rl)
                && rearTrigger.Contains(rr)) {
                return true;
            } else {
                return false;
            }            
        } else {
            Debug.Log("Hihi");
            if ((frontTrigger.Contains(fl) || rearTrigger.Contains(fl))
                && (frontTrigger.Contains(fr) || rearTrigger.Contains(fr))
                && (frontTrigger.Contains(rl) || rearTrigger.Contains(rl))
                && (frontTrigger.Contains(rr) || rearTrigger.Contains(rr))) { 
                return true;
            } else {
                return false;
            }
        }
    }
}

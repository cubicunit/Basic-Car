using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public enum GearBox
{
    Park = -2,
    Reverse = -1,
    None = 0,
    Drive = 1
}

public class car_root : MonoBehaviour
{
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxBrakeTorque;
    public float maxSteeringAngle;
    public GearBox gearBox;

    public float creepingGas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Gear Up")) {
            Debug.Log("Click Gear Up");
            if (gearBox == GearBox.Reverse) {
                gearBox = GearBox.None;
            } else if (gearBox == GearBox.None) {
                gearBox = GearBox.Drive;
            }  
        } else if (Input.GetButtonDown("Gear Down")) {
            Debug.Log("Click Gear Down");
            if (gearBox == GearBox.Drive) {
                 gearBox = GearBox.None;
            } else if (gearBox == GearBox.None) {
                gearBox = GearBox.Reverse;
            }
        }

        // Debug.Log("GearBox: "+gearBox);
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
        // Debug.Log(collider.transform.childCount);
        
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }


    public void FixedUpdate()
    {
        float inputVertical = Input.GetAxis("Vertical");
        float gas = (inputVertical > 0)? Mathf.Abs(inputVertical):creepingGas;
        float brake = (inputVertical < 0)? Mathf.Abs(inputVertical):0.0f;
        int gear = (int)gearBox;
 
        float motorGas = maxMotorTorque * gear * gas;
        float motorBrake = maxBrakeTorque * (brake>0?1:0);
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {            
                axleInfo.leftWheel.motorTorque = motorGas;
                axleInfo.rightWheel.motorTorque = motorGas;

                if (brake >= 0) {
                    axleInfo.leftWheel.brakeTorque = motorBrake;
                    axleInfo.rightWheel.brakeTorque = motorBrake;
                }
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }    
}

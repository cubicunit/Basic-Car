using System.Collections.Generic;
using UnityEngine;
using BasicCarParking;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxBrakeTorque;
    public float maxSteeringAngle;
    public GEARBOX gearBox;

    public float creepingGas;

    public void setGear(GEARBOX gear) {
        this.gearBox = gear;
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
        
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }


    public void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        //Steering
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        //Gas
        float gas = (inputVertical > 0)? Mathf.Abs(inputVertical):creepingGas;
        float brake = (inputVertical < 0)? Mathf.Abs(inputVertical):0.0f;
        int gear = (int)gearBox;

        float motorGas = 0; 
        float motorBrake = 0;
        if (gearBox == GEARBOX.PARK) {
            motorGas = 0;
            motorBrake = maxBrakeTorque;
        } else {
            if (brake > 0) {
                motorBrake = maxBrakeTorque * brake;
            } else {
                motorGas = (maxMotorTorque * gear * gas);
            }
        }

 Debug.Log("gas:" +gas + ", brake: "+ brake+", motorGas: "+ motorGas +", motorBrake: "+ motorBrake);

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor) {            
                axleInfo.leftWheel.motorTorque = motorGas;
                axleInfo.rightWheel.motorTorque = motorGas;
                axleInfo.leftWheel.brakeTorque = motorBrake;
                axleInfo.rightWheel.brakeTorque = motorBrake;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }    
}

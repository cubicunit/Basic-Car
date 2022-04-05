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

    private float inputGas;
    private float inputBrake;
    private float inputSteer;

    public void setGear(GEARBOX gear) {
        this.gearBox = gear;
    }

    public void gasBrake(float inputVertical) {
        //Gas
        inputGas = (inputVertical > 0)? Mathf.Abs(inputVertical):0.0f;
        inputBrake = (inputVertical < 0)? Mathf.Abs(inputVertical):0.0f;
    }

    public void steer(float inputHorizontal) {
        //Steering
        inputSteer = inputHorizontal;
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
        float motorGas = 0;
        float motorBrake = 0;
        float steering = inputSteer * maxSteeringAngle;
        int gear = (int)gearBox;

        if (gearBox == GEARBOX.PARK) {
            motorGas = 0;
            motorBrake = maxBrakeTorque;
        } else {
            if (inputBrake > 0) {
                motorGas = 0;
                motorBrake = maxBrakeTorque * inputBrake;
            } else {
                inputGas = (inputGas == 0)?creepingGas:inputGas;
                motorGas = (maxMotorTorque * gear * inputGas);
                motorBrake = 0;
            }
        }

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

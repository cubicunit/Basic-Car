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
    private float inputGas;
    private float inputBrake;
    private float inputSteer;

    [Space(10)]
    [SerializeField] public List<AxleInfo> axleInfos; 
    [SerializeField] public float maxMotorTorque;
    [SerializeField] public float maxBrakeTorque;
    [SerializeField] public float maxSteeringAngle;
    [SerializeField] public GEARTYPE gearBox;
    [SerializeField] public float creepingGas;

    [Space(10)]
    [SerializeField, Range(0,60)]  public int maxLookRight;
    [SerializeField, Range(-60,0)] public int maxLookLeft;
    [SerializeField] public float lookSpeed;
    [SerializeField] public LOOKDIR lookAt;

    [Space(10)]
    public Vector3 topDownOffset;
    public Vector3 thirdPersonOffset;

    private void OnCollisionEnter(Collision other) {
        if (!other.collider.isTrigger) { 
            Debug.Log("Car Hit. End Game");
            GameObject.Find("GameMaster").GetComponent<GameMaster>().failStage();
        }
    }

    public bool isStop() {        
        float fl = Mathf.Abs(axleInfos[0].leftWheel.GetComponent<WheelCollider>().rpm);
        float fr = Mathf.Abs(axleInfos[0].rightWheel.GetComponent<WheelCollider>().rpm);
        float rl = Mathf.Abs(axleInfos[1].leftWheel.GetComponent<WheelCollider>().rpm);
        float rr = Mathf.Abs(axleInfos[1].rightWheel.GetComponent<WheelCollider>().rpm);

        if (fl < 0.03 && fr < 0.03 && rl < 0.03 && rr < 0.03) {
            return true;
        } else {
            return false;
        }
    }

    public void takeControl(bool flag) {
        setGear(GEARTYPE.PARK);
    }

    public void setGear(GEARTYPE gear) {
        this.gearBox = gear;
    }

    public void changeGear(GEARDIR dir) {
        int thisGear = (int)this.gearBox;
    
        if (dir == GEARDIR.UP) {
            thisGear++;
        } else {
            thisGear--;
        }

        this.gearBox = champGear(thisGear);
    }

    private GEARTYPE champGear(int gear) {
        int thisGear = gear;
        if (thisGear < (int)GEARTYPE.PARK)  thisGear = (int)GEARTYPE.PARK; 
        if (thisGear > (int)GEARTYPE.DRIVE) thisGear = (int)GEARTYPE.DRIVE;
        return (GEARTYPE)thisGear;
    }

    public void gasBrake(float inputVertical) {
        //Gas
        inputGas = (inputVertical > 0)? Mathf.Abs(inputVertical):0.0f;
        inputBrake = (inputVertical < 0)? 1.0f:0.0f;
    }

    public void steer(float inputHorizontal) {
        //Steering
        inputSteer = inputHorizontal;
    }

    public void look(LOOKDIR dir) {
        lookAt = dir;
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void applyLocalPositionToVisuals(WheelCollider collider)
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

        if (gearBox == GEARTYPE.PARK) {
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

            applyLocalPositionToVisuals(axleInfo.leftWheel);
            applyLocalPositionToVisuals(axleInfo.rightWheel);
        }

    }    
}

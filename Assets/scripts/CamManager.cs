using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] public Transform mainCamera;

    [Space(15)]
    [SerializeField] public Transform centerMirrorCamera;
    [SerializeField] public Transform leftMirrorCamera;
    [SerializeField] public Transform rightMirrorCamera;

    public void setTargetCar(Transform car) {
        mainCamera.GetComponent<MainCamCtrl>().target = car;

        // Set Mirror
        centerMirrorCamera.SetParent(car.Find("Mirror Cameras/Center Mirror Container"),false);
        leftMirrorCamera.SetParent(car.Find("Mirror Cameras/Left Mirror Container"),false);
        rightMirrorCamera.SetParent(car.Find("Mirror Cameras/Right Mirror Container"),false);
    }
}

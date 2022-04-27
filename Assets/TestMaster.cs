using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMaster : MonoBehaviour
{
    public void clickV(InputAction.CallbackContext context){
        float v = context.ReadValue<float>();
        Debug.Log("Click V Button.: " + v);
    }

    public void move(InputAction.CallbackContext context) {
        Vector2 v = context.ReadValue<Vector2>();
        Debug.Log("Move:"+v);
    }
}

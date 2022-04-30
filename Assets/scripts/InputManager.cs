using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionAsset inputAsset;

    public void disableGameControl() {
        inputAsset.FindActionMap("Game Control").Disable();
    }

    public void enableGameControl() {
        inputAsset.FindActionMap("Game Control").Enable();
    }    
}

using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    private GameMaster gm;

    private MainCamCtrl mainCamCtrl;

    private InputManager inputManager;

    public void Awake()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        mainCamCtrl = GameObject.Find("Main Camera").GetComponent<MainCamCtrl>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        
    }

    public void selectCar(InputAction.CallbackContext context){
        if (context.started) {
            Vector2 screenPosition = Mouse.current.position.ReadValue();

            Mouse mouse = Mouse.current;
            if (mouse != null) {
                screenPosition = mouse.position.ReadValue();
            }

            Touchscreen touchScreen = Touchscreen.current;
            if (touchScreen != null) {
                screenPosition = touchScreen.position.ReadValue();
            }

            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit[] hitInfos = Physics.RaycastAll(ray,100);
            foreach (RaycastHit hitInfo in hitInfos) {
                Transform selection = hitInfo.transform;
                string layerName = LayerMask.LayerToName(selection.gameObject.layer);
                if (layerName == "Controllable") {
                    gm.setTargetCar(selection.gameObject);
                    mainCamCtrl.changeGameMode();
                    inputManager.enableGameControl();
                }
            }
        }
    }
}

using UnityEngine;
using BasicCarParking;

// [ExecuteAlways]
public class GameMaster : MonoBehaviour
{
    public GAMESTATE gameState = GAMESTATE.PAUSE;
    private CamManager camManager; 
    private CarDriver carDriver; 
    private InputManager inputManager;

    [Space(20)]
    [SerializeField] 
    public GameObject[] candidates;

    [SerializeField]
    public GameObject[] parks;

    [Space(20)]
    public Transform stageEndUI;
    public Transform failStageUI;

    [Space(20)]
    public Animator transition;
 
    public float transitionTime = 1.0f;


    public void pause() {
        Time.timeScale = 0;
    }

    public void continueGame() {
        Time.timeScale = 1;
    }

    public void failStage() { 
        pause();
        failStageUI.gameObject.SetActive(true);
    }

    private void Awake() {
        camManager = GameObject.Find("CameraManager").GetComponent<CamManager>();
        carDriver  = GameObject.Find("CarDriver").GetComponent<CarDriver>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        setTargetCar(carDriver.targetCar);
    }

    public void setTargetCar(GameObject nextCar) {
        carDriver.changeCar(nextCar);

        //Set camera
        camManager.setTargetCar(nextCar.transform);
    }

    private void Update() {
        foreach (GameObject park in parks) {
            Debug.Log(park.name);
            if (park.GetComponent<ParkTrigger>().isTargetCarParked()){
                Debug.Log("Car Parked.");
                //levelFinish();
                inputManager.disableGameControl();
                stageEndUI.gameObject.SetActive(true);
            }
        }
    }
}

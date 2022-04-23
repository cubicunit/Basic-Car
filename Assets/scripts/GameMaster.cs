using UnityEngine;
using BasicCarParking;
using UnityEngine.SceneManagement;
using System.Collections;

// [ExecuteAlways]
public class GameMaster : MonoBehaviour
{
    public GAMESTATE gameState = GAMESTATE.PAUSE;
    private CamManager camManager; 
    private CarDriver carDriver; 

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

    // public void startGame() {
    //     gameState = GAMESTATE.START;
    // }

    public void pause() {
        Time.timeScale = 0;
    }

    public void continueGame() {
        Time.timeScale = 1;
    }

    public void failStage() { 
        Time.timeScale = 0;
        failStageUI.gameObject.SetActive(true);
    }

    private void Awake() {
        camManager = GetComponent<CamManager>();
        carDriver  = this.transform.Find("CarDriver").GetComponent<CarDriver>();

        setTargetCar(carDriver.targetCar);
    }

    // Update is called once per frame
    IEnumerator loadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void levelFinish() {
        Debug.Log("Level Finished");
        int nextIndex = SceneManager.GetActiveScene().buildIndex+1;
        StartCoroutine(loadLevel(nextIndex));
    }

    public void reloadLevel() {
        Time.timeScale = 1;
        int thisIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(loadLevel(thisIndex));
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
                stageEndUI.gameObject.SetActive(true);
            }
        }
    }
}

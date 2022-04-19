using UnityEngine;
using BasicCarParking;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    public CameraManager cameraManager;

    [SerializeField]
    public CarDriver carDriver; 

    [Space(20)]
    [SerializeField]
    private int currentCarIdx = 0;

    [SerializeField]
    public GameObject[] candidates;

    [SerializeField]
    public GameObject[] parks;

    public Animator transition;
 
    public float transitionTime = 1.0f;

    // Update is called once per frame
    IEnumerator loadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void levelFinish() {
        Debug.Log("Level Finished");
        int nextIndex = SceneManager.GetActiveScene().buildIndex+1;
        StartCoroutine(loadLevel(1));
    }

    public void changeNextCar() {
        int maxCarNum = this.candidates.Length;
        currentCarIdx = ++currentCarIdx%maxCarNum;

        GameObject successor = candidates[currentCarIdx];
        carDriver.changeCar(successor);

        cameraManager.target = successor.transform;
    }

    private void Update() {
        if (SimpleInput.GetButtonDown("Change Car")){
            changeNextCar();
        }

        foreach (GameObject park in parks) {
            if (park.GetComponent<ParkTrigger>().isTargetCarParked()){
                Debug.Log("Car Parked.");
                levelFinish();
            }
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Space(20)]
    [SerializeField] public Animator transition;
    [SerializeField] public float transitionTime = 1.0f;

    IEnumerator loadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator loadLevel(string sceneName) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

    public void startLoadLevel(int index) {
        StartCoroutine(loadLevel(index));
    }

    public void startLoadLevel(string sceneName) {
        StartCoroutine(loadLevel(sceneName));
    }

    public void loadSelectStage() {
        StartCoroutine(loadLevel("Scenes/StageSelect"));
    }

    public void restartLevel() {
        int thisIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(loadLevel(thisIndex));
    }    

    public void levelPass() {
        int nextIndex = SceneManager.GetActiveScene().buildIndex+1;
        StartCoroutine(loadLevel(nextIndex));
    }    
}

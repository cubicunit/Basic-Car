using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1.0f;

    // Update is called once per frame
    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}

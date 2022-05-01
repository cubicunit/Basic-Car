using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform gameUI;
    public Transform birdUI;

    public void toGameMode() {
        gameUI.gameObject.SetActive(true);
        birdUI.gameObject.SetActive(false);
    }

    public void toBirdMode() {
        birdUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }
}

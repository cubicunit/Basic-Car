using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StartUICtrl : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClickEvent = new UnityEvent();

    void Awake()
    {
        // GameObject.Find("GameMaster").GetComponent<GameMaster>().pause();
    }

    public void OnPointerClick(PointerEventData eventData){
        // GameObject.Find("GameMaster").GetComponent<GameMaster>().continueGame();
        this.gameObject.SetActive(false);
        OnClickEvent.Invoke();
    }
}

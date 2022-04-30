using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StartUICtrl : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClickEvent = new UnityEvent();

    private InputManager inputManager;

    void Awake()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        inputManager.disableGameControl();
    }

    public void OnPointerClick(PointerEventData eventData){
        inputManager.enableGameControl();
        this.gameObject.SetActive(false);
        OnClickEvent.Invoke();
    }
}

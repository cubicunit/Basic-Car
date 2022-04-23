using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StartUICtrl : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnClickEvent = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData){
        this.gameObject.SetActive(false);
        OnClickEvent.Invoke();
    }
}

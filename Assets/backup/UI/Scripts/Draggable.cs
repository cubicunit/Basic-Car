using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();    
    }

    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("OnBeginDrag....");
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag...."+eventData.delta);

        // this.transform.position = eventData.position;
        rectTransform.anchoredPosition += eventData.delta;
    }


    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag....");
    }  
}

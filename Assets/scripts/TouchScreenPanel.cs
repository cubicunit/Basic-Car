using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

public class TouchScreenPanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [System.Serializable]
    public class TouchScreenEvent : UnityEvent<Vector2> {} 

    public TouchScreenEvent OnDragScreen = new TouchScreenEvent();

    public TouchScreenEvent OnTapScreen = new TouchScreenEvent();

    private bool isDrag = false;

    public void OnPointerDown( PointerEventData eventData )
    {
    }

    public void OnDrag( PointerEventData eventData )
    {
        isDrag = true;
        OnDragScreen.Invoke(eventData.delta);
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        if (isDrag == false) {
            OnTapScreen.Invoke(eventData.position);
        }
        isDrag = false;
    }
}

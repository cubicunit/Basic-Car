using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class StagePassCtrl : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();

    public void click() {
        OnClick.Invoke();
    }

}

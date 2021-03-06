using UnityEngine.Events;
using UnityEngine;

public class SettingsCtrl : MonoBehaviour
{
    public UnityEvent OnReloadClick = new UnityEvent();
    public UnityEvent OnExitClick = new UnityEvent();
    public UnityEvent OnSelectStageClick = new UnityEvent();

    public void clickReload() {
        OnReloadClick.Invoke();
    }

    public void clickExit() {
        OnExitClick.Invoke();
    }

    public void clickSelectStage() {
        OnSelectStageClick.Invoke();
    }
}

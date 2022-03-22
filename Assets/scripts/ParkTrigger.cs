using System.Collections;
using UnityEngine;
// using EasyUI.Dialogs;

public class ParkTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Object Enter");
        OnShowMessageDialog();
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Object Exit");
    }

    void OnShowMessageDialog() {
        Debug.Log("On Message Dialog");
        // ConfirmDialogUI.Instance
        //     .SetTitle ( "Message" )
        //     .SetMessage( "Car Enter Car Park" )
        //     .SetButtonsColor( DialogButtonColor.Magenta )
        //     .SetFadeDuration ( .4f )
        //     .Show();
    } 
}

using UnityEngine;
using HoloToolkit.Unity.InputModule;
using PopUpNames;

public class TurnOnOffPopUps : MonoBehaviour, IInputClickHandler
{
    public PopUpName _popUpnameToTurnOff;
    public PopUpName _popUpnameToTurnOn;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        PopUpsManager.Instance.ChangePopUp(_popUpnameToTurnOn, _popUpnameToTurnOff);    
    }
}

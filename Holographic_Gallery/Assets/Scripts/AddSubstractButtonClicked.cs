using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class AddSubstractButtonClicked : MonoBehaviour, IInputClickHandler
{
    public bool _isThisAddButton;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (_isThisAddButton == true)
        {
            GMScript.Instance.PhotosAmountAdd();
        }
        else
        {
            GMScript.Instance.PhotosAmountSubStract();
        }       
    }
}

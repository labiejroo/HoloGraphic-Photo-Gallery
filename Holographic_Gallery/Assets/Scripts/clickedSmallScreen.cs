using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class clickedSmallScreen : MonoBehaviour, IInputClickHandler
{
    public int index;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        DataCollector.Instance.LoadSplashScreen(index);
    }
}

using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class CategorySelectButtonClicked : MonoBehaviour, IInputClickHandler
{
    public int _indexOfCategory;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GMScript.Instance._IndexOfSelectedCategory = _indexOfCategory;
        ChangeColourOfSelectedCat.Instance.ChangeColour(_indexOfCategory);
    }
}

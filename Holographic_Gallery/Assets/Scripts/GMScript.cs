using System.Collections.Generic;
using UnityEngine;
using SingletonFile;
using System.Linq;

public class GMScript : Singleton<GMScript> {

    [SerializeField]
    private List<GameObject> _appPopUps;

    [SerializeField]
    private TextMesh _howManyPhotosText;

    [SerializeField]
    private TextMesh[] _categoryFieldsInUI;

    private int _indexOfSelectedCategory = 0;
    public int _IndexOfSelectedCategory
    {
        get { return _indexOfSelectedCategory; }
        set { _indexOfSelectedCategory = value; }
    }

    private Dictionary<string, string> _categoryDictionary = new Dictionary<string, string>();

    private void Start()
    {
        StartApplication();
        InitDictionaryOfCategories();
    }

    private void InitDictionaryOfCategories()
    {
        _categoryDictionary.Add("99671888@N00", "Sport");
        _categoryDictionary.Add("734043@N22", "Animals");
        _categoryDictionary.Add("2620781@N24", "Travel");
        _categoryDictionary.Add("1624782@N23", "Cars");
    }

    private void StartApplication()
    {
        _appPopUps[0].SetActive(true);    
    }

    public string GetCategoryCode()
    {
        return _categoryDictionary.Keys.ElementAt(_indexOfSelectedCategory);
    }

    public int GetAmountOfPhotos()
    {
        return int.Parse(_howManyPhotosText.text);
    }

    /// <summary>
    /// Fullfills empty text boxes with real category which user can choose
    /// </summary>
    public void FullFillCategoryUI()
    {
        int _arrayLength = _categoryFieldsInUI.Length;

        for (int i = 0; i < _arrayLength; i++)
        {
            _categoryFieldsInUI[i].text = _categoryDictionary.Values.ElementAt(i);
        }
    }

    /// <summary>
    /// Add amount on the counter which is on the pop up where user selecting how many photos he want and in which category
    /// </summary>
    public void PhotosAmountAdd()
    {
        int _currentAmount = int.Parse(_howManyPhotosText.text);

        if(_currentAmount < 20)
        {
            _currentAmount++;
            _howManyPhotosText.text = _currentAmount.ToString();
        }
        
    }

    /// <summary>
    /// Substract amount on the counter which is on the pop up where user selecting how many photos he want and in which category
    /// </summary>
    public void PhotosAmountSubStract()
    {
        int _currentAmount = int.Parse(_howManyPhotosText.text);

        if (_currentAmount != 1)
        {
            _currentAmount--;
            _howManyPhotosText.text = _currentAmount.ToString();
        }
       
    }
}

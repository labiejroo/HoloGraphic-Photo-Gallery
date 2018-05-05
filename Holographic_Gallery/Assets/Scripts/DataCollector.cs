using System.Collections;
using UnityEngine;
using SingletonFile;
using SimpleJSON;

public class DataCollector : Singleton<DataCollector>
{
    [SerializeField]
    private GameObject _loadingAnimation;

    [SerializeField]
    private GameObject _splashScreen;

    [SerializeField]
    private TextMesh _numberOfPagesText;

    [SerializeField]
    private MeshRenderer[] _UIFieldsForData;

    [SerializeField]
    private Texture2D _textureForEmptyBoxex;

    private string _URL = "http://api.flickr.com/services/feeds/photos_public.gne?";
    private string _jsonFormat = "&format=json&nojsoncallback=?";
    private string _wwwAddress;

    //init when runs
    private int _howManyPhotos = 1;
    private string _IDOfCategory = "";

    //textures of photos downloaded from Flickr
    private Texture2D[] _photosTextures;

    private int _numberOfPages = 1;
    private int _currentPage = 1;

    public void DownloadPhotosFromServer()
    {
        _loadingAnimation.SetActive(true);

        _howManyPhotos = GMScript.Instance.GetAmountOfPhotos();
        _IDOfCategory = GMScript.Instance.GetCategoryCode();
        _wwwAddress = _URL + _IDOfCategory + _jsonFormat;
        Debug.Log("www -" + _wwwAddress);

        StartCoroutine("DownloadRESTAPI");

        _numberOfPages = _howManyPhotos / _UIFieldsForData.Length;

        if(_howManyPhotos % _UIFieldsForData.Length != 0)
        {
            _numberOfPages++;
        }

        _numberOfPagesText.text = _currentPage.ToString()+"/"+_numberOfPages.ToString();
    }

    IEnumerator DownloadRESTAPI()
    {
        WWW RESTapi = new WWW(_wwwAddress);
        yield return RESTapi;

        string RESTText = RESTapi.text;
        ParseREST(RESTText);
    }

    void ParseREST(string _text)
    {
        JSONNode ParsedJSON = JSON.Parse(_text);
        
        if (string.IsNullOrEmpty(_text))
        {
            Debug.Log("REST --------------- Pobrany z API ciag informacji jest pusty");
        }

        string[] photosUrls = new string[_howManyPhotos];
        for (int i = 0; i < photosUrls.Length; i++)
        {
            photosUrls[i] = ParsedJSON["items"][i]["media"]["m"].Value;
            
        }

        StartCoroutine("DownloadPhoto", photosUrls);
    }
 
    IEnumerator DownloadPhoto(string[] _url)
    {
        _photosTextures = new Texture2D[_howManyPhotos];

        for (int i = 0; i < _howManyPhotos; i++)
        {
            WWW photoUrl = new WWW(_url[i]);
            yield return photoUrl;

            _photosTextures[i] = new Texture2D(1, 1);
            photoUrl.LoadImageIntoTexture(_photosTextures[i]);     
        }

        _loadingAnimation.SetActive(false);

        for (int i = 0; i < 9 && i < _howManyPhotos; i++)
        {
            _UIFieldsForData[i].material.mainTexture = _photosTextures[i];
        }      
    }

    public void LoadSplashScreen(int index)
    {
        int photoIndexInArray = (_currentPage - 1) * _UIFieldsForData.Length + index;
    
        if ((index + (_UIFieldsForData.Length * (_currentPage - 1)) )< _photosTextures.Length)
        {
            _splashScreen.SetActive(true);
            Sprite mySprite = Sprite.Create(_photosTextures[photoIndexInArray], new Rect(0.0f, 0.0f, _photosTextures[photoIndexInArray].width, _photosTextures[photoIndexInArray].height), new Vector2(0.5f, 0.5f), 100.0f);
            _splashScreen.GetComponent<SpriteRenderer>().sprite = mySprite;
        }

    }

    public void NextPageOfPhotos()
    {
        for (int i = 0; i < 9 && i < _howManyPhotos; i++)
        {
            _UIFieldsForData[i].material.mainTexture = _textureForEmptyBoxex;
        }

        if (_currentPage < _numberOfPages)  _currentPage++;
        _numberOfPagesText.text = _currentPage.ToString() + "/" + _numberOfPages.ToString();
        int multiPliayer = _currentPage - 1;
        for (int i = 0, j = _UIFieldsForData.Length * multiPliayer; i < _howManyPhotos-(_UIFieldsForData.Length*multiPliayer) && i < 9; i++,j++)
        {
            _UIFieldsForData[i].material.mainTexture = _photosTextures[j];
        }
    }
    public void PreviousPageOfPhotos()
    {
        for (int i = 0; i < 9 && i < _howManyPhotos; i++)
        {
            _UIFieldsForData[i].material.mainTexture = _textureForEmptyBoxex;
        }

        if (_currentPage > 1)  _currentPage--;
        _numberOfPagesText.text = _currentPage.ToString() + "/" + _numberOfPages.ToString();
        int multiPliayer = _currentPage - 1;
        for (int i = 0, j = _UIFieldsForData.Length * multiPliayer; i < _howManyPhotos - (_UIFieldsForData.Length * multiPliayer) && i < 9; i++, j++)
        {
            _UIFieldsForData[i].material.mainTexture = _photosTextures[j];
        }
    }

    public static bool IsNullOrEmpty(Texture2D texture)
    {
        return (texture == null );
    }
}
using UnityEngine;
using PopUpNames;
using SingletonFile;

namespace PopUpNames
{
    public enum PopUpName {WelcomePopUp,ChooseDataPopUp,ChoosePhotoPopUp,NextPage,PreviousPage,SplashScreen,Nothing}
}

public class PopUpsManager : Singleton<PopUpsManager>
{

    [SerializeField]
    private GameObject[] _popUpsGameObjects;

    [SerializeField]
    private GameObject _splashScreen;

    public void ChangePopUp(PopUpName popUpToTurnOn, PopUpName popUpToTurnOff)
    {
        switch (popUpToTurnOn)
        {
            case PopUpName.WelcomePopUp:
                {
                    _popUpsGameObjects[0].SetActive(true);
                    break;
                }
            case PopUpName.ChooseDataPopUp:
                {
                    GMScript.Instance.FullFillCategoryUI();
                    _popUpsGameObjects[1].SetActive(true);
                    break;
                }
            case PopUpName.ChoosePhotoPopUp:
                {
                    DataCollector.Instance.DownloadPhotosFromServer();
                    _popUpsGameObjects[2].SetActive(true);
                    break;
                }
            case PopUpName.NextPage:
                {
                    DataCollector.Instance.NextPageOfPhotos();                   
                    break;
                }
            case PopUpName.PreviousPage:
                {
                    DataCollector.Instance.PreviousPageOfPhotos();
                    break;
                }
            case PopUpName.SplashScreen:
                {
                    _splashScreen.SetActive(true);

                    break;
                }
            case PopUpName.Nothing:
                {              
                    break;
                }
        }

        switch (popUpToTurnOff)
        {
            case PopUpName.WelcomePopUp:
                {
                    _popUpsGameObjects[0].SetActive(false);
                    break;
                }
            case PopUpName.ChooseDataPopUp:
                {
                    _popUpsGameObjects[1].SetActive(false);
                    break;
                }
            case PopUpName.ChoosePhotoPopUp:
                {
                    _popUpsGameObjects[2].SetActive(false);
                    break;
                }
            case PopUpName.SplashScreen:
                {
                    _splashScreen.SetActive(false);
                    break;
                }
            case PopUpName.Nothing:
                {
                    break;
                }

        }
    }
}

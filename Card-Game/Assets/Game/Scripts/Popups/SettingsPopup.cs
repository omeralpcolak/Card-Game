using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsPopup : MonoBehaviour
{
    private static SettingsPopup Create()
    {
        SettingsPopup _instance = Instantiate(Resources.Load<SettingsPopup>("SettingsPopup"));
        _instance.name = "SettingsPopup";
        return _instance;
    }

    public static void Show()
    {
        Create().ShowPopup();
    }

    public RectTransform soundSettings;
    public RectTransform restartButton;
    public RectTransform mainMenuButton;



    private void ShowPopup()
    {
        Time.timeScale = 0;

        soundSettings.transform.DOScale(Vector3.one, 0.3f).ChangeStartValue(Vector3.zero).SetEase(Ease.OutBack).SetUpdate(true);
        restartButton.transform.DOScale(Vector3.one, 0.3f).ChangeStartValue(Vector3.zero).SetEase(Ease.OutBack).SetDelay(0.2f).SetUpdate(true);
        mainMenuButton.transform.DOScale(Vector3.one, 0.3f).ChangeStartValue(Vector3.zero).SetEase(Ease.OutBack).SetDelay(0.4f).SetUpdate(true);
    }

    public void OnRestartButtonClicked()
    {
        SceneTransition.Reload();
    }

    public void OnMainMenuButtonClicked()
    {
        //SceneTransition.Load("MainMenu");
    }
}

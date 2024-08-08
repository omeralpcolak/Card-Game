using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletePopup : MonoBehaviour
{
    private static LevelCompletePopup Create()
    {
        LevelCompletePopup _instance = Instantiate(Resources.Load<LevelCompletePopup>("LevelCompletePopup"));
        _instance.name = "LevelCompletePopup";
        return _instance;
    }

    public static void Show()
    {
        Create().ShowPopup();
    }

    public RectTransform title;
    public RectTransform restartButton;
    public RectTransform mainMenuButton;


    private void ShowPopup()
    {
        float targetY = title.anchoredPosition.y;

        title.anchoredPosition = new Vector2(0, 1200);
        title.DOAnchorPosY(targetY, 0.5f).SetEase(Ease.OutBack);


        restartButton.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).ChangeStartValue(Vector3.zero).SetDelay(0.3f);
        mainMenuButton.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).ChangeStartValue(Vector3.zero).SetDelay(0.6f);

        DOVirtual.DelayedCall(0.9f, () =>
        {
            restartButton.GetComponent<Button>().interactable = true;
            mainMenuButton.GetComponent<Button>().interactable = true;
        });
    }



    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMainMenuButtonClicked()
    {
        Debug.Log("Going to main menu !!");
    }
}

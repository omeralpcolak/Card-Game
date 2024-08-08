using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ComboCounter : MonoBehaviour
{
    private int count;

    public TMP_Text countTxt;
    public RectTransform comboTitle;


    public void Show()
    {
        count++;

        if(count >= 1)
        {
            if(comboTitle.gameObject.activeSelf == false)
            {
                comboTitle.gameObject.SetActive(true);
                comboTitle.transform.DOScale(Vector3.one, 0.3f).ChangeStartValue(Vector3.zero).SetEase(Ease.OutBack);                
            }

            if(countTxt.gameObject.activeSelf == false)
            {
                countTxt.gameObject.SetActive(true);
            }

            countTxt.text = "x" + count;
            countTxt.transform.DOScale(Vector3.one, 0.3f).ChangeStartValue(Vector2.zero).SetEase(Ease.OutBack);

        }
    }

    public void Reset()
    {
        count = 0;

        if(comboTitle.gameObject.activeSelf == false || countTxt.gameObject.activeSelf == false)
        {
            return;
        }

        countTxt.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        comboTitle.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).SetDelay(0.2f);
        DOVirtual.DelayedCall(0.6f, () =>
        {

            comboTitle.gameObject.SetActive(false);
            countTxt.gameObject.SetActive(false);

        });
    } 

    private void OnDestroy()
    {
        countTxt.DOKill();
        comboTitle.DOKill();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shatter : MonoBehaviour
{
    public static void Gold(Card _card, int _goldAmount)
    {
        GameManager.instance.shatter.GoldObject(_card,_goldAmount);
    }


    public static void Point(Card _card, int _pointAmount)
    {
        GameManager.instance.shatter.PointObject(_card, _pointAmount);
    }

    public GameObject goldObject;


    private void GoldObject(Card _card, int _goldAmount)
    {
        for(int i = 0; i< _goldAmount; i++)
        {
            var goldObjIns = Instantiate(goldObject, _card.transform.position + new Vector3(0,0,-2), Quaternion.identity);
            goldObjIns.transform.localScale = Vector3.zero;

            goldObjIns.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetDelay(i * 0.02f);
            
            goldObjIns.transform.DOLocalMove(_card.transform.position + new Vector3(0, Random.Range(1f,2f), Random.Range(-3f,-5f)), 0.3f).SetDelay(i * 0.02f + 0.2f).OnComplete(() =>
            {
                goldObjIns.transform.SetParent(GameManager.instance.goldIcon);
                goldObjIns.transform.DOLocalMove(Vector3.zero, 0.7f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    Destroy(goldObjIns);
                });
            });      
            
        }
    }

    private void PointObject(Card _card, int _pointAmount)
    {
         
    }
}

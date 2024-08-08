using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum CardState
{
    OPEN,
    CLOSED
}

public class Card : MonoBehaviour
{
    public int id;
    public Sprite sprite;
    public Image frontImg;

    public CardState state;
    public Level owner;
    public ParticleSystem glowEffect;

    [HideInInspector]public Vector3 targetPos;
    [HideInInspector] public bool canBeIntrectable;

    private bool canBeFlipped;
    

    public void Init(Level _owner)
    {
        frontImg.gameObject.SetActive(true);
        frontImg.sprite = sprite;
        state = CardState.CLOSED;
        owner = _owner;
        canBeFlipped = true;
        canBeIntrectable = true;        
    }
    private void OnMouseDown()
    {
        if (!canBeIntrectable)
        {
            return;
        }
        Flip();
    }

    public void Flip()
    {
        if (!canBeFlipped || owner.selectedCards.Contains(this))
        {
            return;
        }

        if (!owner.selectedCards.Contains(this))
        {
            owner.SelectCard(this);
            FlipMovement();
        }
        
    }

    public void FlipMovement()
    {
        canBeFlipped = false;
        state = state == CardState.OPEN ? CardState.CLOSED : CardState.OPEN;

        glowEffect.gameObject.SetActive(state == CardState.OPEN ? true : false);

        transform.DOMoveZ(-1, 0.3f).SetEase(Ease.Linear).ChangeStartValue(new Vector3(0, 0, 0));
        transform.DORotate(new Vector3(0, transform.eulerAngles.y + 180f, 0), 0.3f);
        transform.DOMoveZ(0, 0.3f).SetDelay(0.3f).SetEase(Ease.Linear);

        DOVirtual.DelayedCall(0.5f, () =>
        {
            canBeFlipped = true;
        });
    }

    public void Dissolve()
    {
        Shatter.Gold(this, 5);
        //Shatter.Point(this, 100);
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
            owner.CheckLevelState();
        });
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

}




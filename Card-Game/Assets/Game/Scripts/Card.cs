using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum CardState
{
    OPEN,
    CLOSED
}

public class Card : MonoBehaviour
{
    public int id;
    public CardState state;
    public Level owner;

    private bool canBeFlipped;
    [HideInInspector]public  bool canBeIntrectable = true;


    private void Start()
    {
        state = CardState.CLOSED;
        canBeFlipped = true;
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
        if (!canBeFlipped)
        {
            return;
        }

        if (owner.selectedCards.Contains(this))
        {
            FlipMovement();
            return;
        }
        else
        {
            owner.SelectCard(this);
        }

        FlipMovement();
    }

    public void FlipMovement()
    {
        canBeFlipped = false;
        state = state == CardState.OPEN ? CardState.CLOSED : CardState.OPEN;

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




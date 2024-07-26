using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;


[System.Serializable]
public class GridMaker
{
    public int rows;
    public int columns;

    public float spacingX;
    public float spacingY;

    public List<Vector3> CreateGrid()
    {
        List<Vector3> cardPoses = new List<Vector3>();
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(-3 + (col * spacingX), row * spacingY, 0);
                cardPoses.Add(position);
            }
        }

        return cardPoses;
    }

}


public class Level : MonoBehaviour
{
    public GridMaker grid;
    public List<Card> cardTypes = new List<Card>();

    [HideInInspector]public List<Card> levelCards = new List<Card>();
    [HideInInspector]public List<Card> selectedCards = new List<Card>();

    public List<Vector3> targetPoses => grid.CreateGrid();

    public Transform deckPos;


    private void Start()
    {
        CreateCards();
    }

    private void CreateCards()
    {
        for(int cardTypeIndex = 0; cardTypeIndex < cardTypes.Count; cardTypeIndex++)
        {
            for(int i = 0; i < 2; i++)
            {
                Card card = Instantiate(cardTypes[cardTypeIndex], deckPos.position,Quaternion.identity);
                levelCards.Add(card);
                card.transform.SetParent(transform);
                card.gameObject.SetActive(false);
            }
        }

        levelCards.Shuffle();
        deckPos.transform.parent = null;
        StartCoroutine(DealTheCards());
    }

    private IEnumerator DealTheCards()
    {
        levelCards.ForEach(x => x.gameObject.SetActive(true));
        yield return new WaitForSeconds(1f);        

        for (int i = 0; i < levelCards.Count; i++)
        {
            levelCards[i].gameObject.SetActive(true);
            levelCards[i].transform.DOMove(targetPoses[i], 0.5f).SetEase(Ease.OutBack).SetDelay(0.3f * i + 0.01f);
        }


        DOVirtual.DelayedCall((0.3f * (levelCards.Count - 1) + 0.01f) + 0.5f, () => levelCards.ForEach(x => x.Init(this)));
    }

    public void SelectCard(Card _card)
    {
        if (selectedCards.Count < 2)
        {
            selectedCards.Add(_card);

            if (selectedCards.Count == 2)
            {
                StartCoroutine(CheckPair());
            }
        }
    }

    public void DeselectCard(Card _card)
    {
        selectedCards.Remove(_card);
    }

    private IEnumerator CheckPair()
    {
        levelCards.ForEach(x => x.canBeIntrectable = false);

        yield return new WaitForSeconds(1f);

        if(selectedCards[0].id == selectedCards[1].id)
        {
            selectedCards.ForEach(x => x.Dissolve());
        }
        else
        {
            Shake();
            selectedCards.ForEach(x => x.FlipMovement());            
        }
        selectedCards.Clear();
        levelCards.ForEach(x => x.canBeIntrectable = true);
    }

    public void Shake()
    {
        Sequence shakeSequence = DOTween.Sequence();
        shakeSequence.Append(transform.DOMoveX(0.1f, 0.1f))
                     .Append(transform.DOMoveX(-0.1f, 0.1f))
                     .Append(transform.DOMoveX(0, 0.1f));
    }

    public void CheckLevelState()
    {
        bool allInactive = true;
        foreach (Card card in levelCards)
        {
            if (card.gameObject.activeSelf)
            {
                allInactive = false;
                break;
            }
        }

        if (allInactive)
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
        StopAllCoroutines();
    }

}

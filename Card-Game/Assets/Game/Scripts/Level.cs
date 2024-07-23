using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    public List<Card> levelCards = new List<Card>();
    public List<Card> selectedCards = new List<Card>();
    public Transform deckPos;

    private void Start()
    {
        StartCoroutine(DealTheCards());
    }

    private IEnumerator DealTheCards()
    {
        levelCards.ForEach(x => x.gameObject.SetActive(false));
        yield return new WaitForSeconds(1f);
        levelCards.ForEach(x => x.targetPos = x.transform.position);
        levelCards.ForEach(x => x.transform.position = deckPos.position);

        for (int i = 0; i < levelCards.Count; i++)
        {
            levelCards[i].gameObject.SetActive(true);
            levelCards[i].transform.DOMove(levelCards[i].targetPos, 0.5f).SetEase(Ease.OutBack).SetDelay(0.3f * i + 0.01f);
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
            Debug.Log("A MATCH");
            selectedCards.ForEach(x => x.Dissolve());
        }
        else
        {
            Debug.Log("NOT A MACTH!");
            selectedCards.ForEach(x => x.FlipMovement());            
        }
        selectedCards.Clear();
        levelCards.ForEach(x => x.canBeIntrectable = true);
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

}

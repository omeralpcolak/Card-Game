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


    private void Start()
    {
        levelCards.ForEach(x => x.owner = this);        
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

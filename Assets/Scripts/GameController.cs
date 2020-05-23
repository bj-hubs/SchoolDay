using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public Sprite bgImage;

    public List<Button> cards = new List<Button>();

    public Sprite[] sprites;

    public List<Sprite> gameCards = new List<Sprite>();

    public bool firstPick, secondPick;

    private int countGuesses, countCorrectGuesses, totalScore;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessCard, secondGuessCard;

    public Text attemptsTxt, scoreTxt;

    public Animator playBtnAni, gameSceneAni;

    public Fade fadeScript;

    private void Awake()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/cards");
    }

    void Start()
    {
        GetCards();
        AddListeners();
        AddGameCards();
        Shuffle(gameCards);
        totalScore = gameCards.Count / 2;
        attemptsTxt.text = "Intentos: 00";
        scoreTxt.text = "Puntos: 00";
        fadeScript.FadeIn();
    }

    public void GetCards()
    {
        GameObject[] btns = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < btns.Length; i++)
        {
            cards.Add(btns[i].GetComponent<Button>());
        }
    }
    
    void AddGameCards()
    {
        int looper = cards.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gameCards.Add(sprites[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in cards)
        {
            btn.onClick.AddListener(() => CardPicker()); 
        }
    }

    public void StartGame()
    {
        playBtnAni.SetTrigger("Erase");
        gameSceneAni.SetTrigger("GameOn");
    }

    public void CardPicker()
    {
        var currentSelection = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string name = currentSelection.name;
        currentSelection.GetComponent<AudioSource>().Play();

        if (!firstPick)
        {
            firstPick = true;
            firstGuessIndex = int.Parse(name);
            firstGuessCard = gameCards[firstGuessIndex].name;
            currentSelection.GetComponent<Animator>().SetTrigger("TurnUp");
        }
        else if (!secondPick)
        {
            secondPick = true;
            secondGuessIndex = int.Parse(name);
            secondGuessCard = gameCards[secondGuessIndex].name;
            currentSelection.GetComponent<Animator>().SetTrigger("TurnUp");
            countGuesses++;
            attemptsTxt.text = "Intentos: "+countGuesses.ToString().PadLeft(2,'0');
            StartCoroutine(CheckMatch());
        }
    }

    public Sprite GetSprite(int index)
    {
        return gameCards[index];
    }

    public Sprite GetSpriteBack()
    {
        return bgImage;
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);
        if (firstGuessCard == secondGuessCard)
        {
            cards[firstGuessIndex].interactable = false;
            cards[secondGuessIndex].interactable = false;
            cards[firstGuessIndex].GetComponent<Animator>().SetTrigger("Hide");
            cards[firstGuessIndex].GetComponent<AudioSource>().Play();
            cards[secondGuessIndex].GetComponent<Animator>().SetTrigger("Hide");
            cards[secondGuessIndex].GetComponent<AudioSource>().Play();
            checkIfGameFinished();
        }
        else
        {
            cards[firstGuessIndex].GetComponent<Animator>().SetTrigger("TurnDown");
            cards[firstGuessIndex].GetComponent<AudioSource>().Play();
            cards[secondGuessIndex].GetComponent<Animator>().SetTrigger("TurnDown");
            cards[secondGuessIndex].GetComponent<AudioSource>().Play();
            cards[firstGuessIndex].interactable = true;
            cards[secondGuessIndex].interactable = true;
        }
        firstPick = secondPick = false;
    }

    private void checkIfGameFinished()
    {
        countCorrectGuesses++;
        scoreTxt.text = "Puntos: "+countCorrectGuesses.ToString().PadLeft(2,'0');

        if (countCorrectGuesses == totalScore)
        {
            gameSceneAni.SetTrigger("GameOver");
        }
    }

    private void Shuffle(List<Sprite> lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            Sprite temp = lst[i];
            int randomIndex = Random.Range(i, lst.Count);
            lst[i] = lst[randomIndex];
            lst[randomIndex] = temp;
        }
    }

}

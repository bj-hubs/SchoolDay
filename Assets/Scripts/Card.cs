using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private GameObject gameControllerGO;
    private GameController gameControllerScript;

    private void Start()
    {
        gameControllerGO = GameObject.Find("GameController");
        gameControllerScript = gameControllerGO.GetComponent<GameController>();   
    }

    void ChangeSprite()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        int index = int.Parse(name);
        gameObject.GetComponent<Button>().image.sprite = gameControllerScript.GetSprite(index);
    }

    void ChangeSpriteBack()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        int index = int.Parse(name);
        gameObject.GetComponent<Button>().image.sprite = gameControllerScript.GetSpriteBack();
    }
}

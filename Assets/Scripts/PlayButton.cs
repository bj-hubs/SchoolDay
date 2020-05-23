using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public Animator playBtnAni, gameSceneAni;
    public void StartGame()
    {
        playBtnAni.SetTrigger("Erase");
        gameSceneAni.SetTrigger("GameOn");
    }
}

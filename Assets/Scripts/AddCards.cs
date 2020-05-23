using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCards : MonoBehaviour
{
    [SerializeField]
    private Transform _puzzleField;

    [SerializeField]
    private GameObject _card;

     void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject cardBTN = Instantiate(_card);
            cardBTN.name = ""+i;
            cardBTN.transform.SetParent(_puzzleField);
        }
    }
}

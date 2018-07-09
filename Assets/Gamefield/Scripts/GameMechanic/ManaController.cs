using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    [SerializeField] private GameObject[] manaArray = new GameObject[5];
    [SerializeField] private int _manaValue = 5;
    private void Start()
    {
        ManaChanged(_manaValue);
    }
    public void ManaChanged(int mana)
    {
        if (mana > 0 && mana <= 5)
        {
            for (int i = 0; i < manaArray.Length; i++)
            {
                if (i < _manaValue)
                {
                    manaArray[i].SetActive(true);
                }
                else
                {
                    manaArray[i].SetActive(false);
                }
            }
        }
        else { Debug.Log("Incorrect Mana Value"); }
    }

}

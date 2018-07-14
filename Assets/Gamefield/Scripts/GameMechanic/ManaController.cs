using UnityEngine;

namespace Gamefield.Scripts.GameMechanic
{
    public class ManaController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _manaArray = new GameObject[5];
        [SerializeField] private int _manaValue = 5;
        private void Start()
        {
            ManaChanged(_manaValue);
        }

        private void ManaChanged(int mana)
        {
            if (mana > 0 && mana <= 5)
            {
                for (var i = 0; i < _manaArray.Length; i++)
                {
                    _manaArray[i].SetActive(i < _manaValue);
                }
            }
            else { Debug.Log("Incorrect Mana Value"); }
        }

    }
}

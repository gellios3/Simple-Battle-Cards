using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Gamefield.Scripts.GameMechanic
{
    public class GameController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Button _endTurnButton;
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private GameObject _treitPrefab;
        public GameObject PlayerPanell;
        public GameObject EnemyPanell;
        public static List<GameObject> PlayerCards = new List<GameObject>();
        public static List<GameObject> EnemyCards = new List<GameObject>();
        private static List<GameObject> _playerTreit = new List<GameObject>();
        private static List<GameObject> _enemyTreit = new List<GameObject>();
        private static CardItemList _cardItemList;
        private static CardTreitList _cardTreitList;
        public static bool PlayerTurn = true;
        private static int _playerId;
        private static int _enemyId;
        public bool NewGame;

        #endregion

        private void Awake()
        {
            _cardItemList =
                AssetDatabase.LoadAssetAtPath("Assets/GameField/ScriptableObjects/CardItemList.asset",
                    typeof(CardItemList)) as CardItemList;
            _cardTreitList =
                AssetDatabase.LoadAssetAtPath("Assets/GameField/ScriptableObjects/CardTreitList.asset",
                    typeof(CardTreitList)) as CardTreitList;
        }

        private void Start()
        {
            NewGame = true;
            SetPlayerCards();
            SetEnemyCards();
        }

        private void SetPlayerCards()
        {
            var playerCardsCount = NewGame ? 3 : PlayerCards.Count;
            for (var i = 0; i < playerCardsCount; i++)
            {
                var cardIndex = UnityEngine.Random.Range(0, _cardItemList.cardList.Count);
                PlayerCards.Add(Instantiate(_cardPrefab, PlayerPanell.transform));
                var index = PlayerCards[i].GetComponent<CardView>();
               // index.indexCard = cardIndex;
                // playerCards[i].transform.SetParent(playerPanell.transform); 
            }
            SetTreitCardtoPlayer();
        }

        private void SetEnemyCards()
        {
            var enemyCardsCount = NewGame ? 3 : EnemyCards.Count;
            for (var i = 0; i < enemyCardsCount; i++)
            {
                var cardIndex = UnityEngine.Random.Range(0, _cardItemList.cardList.Count);
                EnemyCards.Add(Instantiate(_cardPrefab, EnemyPanell.transform));
               // var index = EnemyCards[i].GetComponent<CardView>();
               // index.indexCard = cardIndex;
                //enemyCards[i].transform.SetParent(enemyPanell.transform);
            }
            SetTreitCardtoEnemy();
        }
        private void SetTreitCardtoPlayer()
        {
            var treitListIndex = 0;
            var treitIndex = UnityEngine.Random.Range(0, _cardTreitList.treitList.Count);
            _playerTreit.Add(Instantiate(_treitPrefab, PlayerPanell.transform));
            var index = _playerTreit[treitListIndex].GetComponent<TrateView>();
           // index.indexCard = treitIndex;
            //playerTreit[treitListIndex].transform.SetParent(playerPanell.transform);
        }

        private void SetTreitCardtoEnemy()
        {
            var treitListIndex = 0;
            var treitIndex = UnityEngine.Random.Range(0, _cardTreitList.treitList.Count);
            _enemyTreit.Add(Instantiate(_treitPrefab, EnemyPanell.transform));
            var index = _enemyTreit[treitListIndex].GetComponent<TrateView>();
           // index.indexCard = treitIndex;
            //enemyTreit[treitListIndex].transform.SetParent(enemyPanell.transform);
        }

//        public void EndTURN()
//        {
//            _playerId = UnityEngine.Random.Range(0, PlayerCards.Count);
//            _enemyId = UnityEngine.Random.Range(0, EnemyCards.Count);
//            int crit = UnityEngine.Random.Range(0, 100);
//            if (PlayerTurn)
//            {
//                if (PlayerCards[_playerId].GetComponent<CardDisplay>().Legendary == true && crit > 0 && crit < 25)
//                {
//                    FightController.KriticalAttack(PlayerCards[_playerId], EnemyCards[_enemyId]);
//                    PlayerTurn = false;
//                    if (EnemyCards.Count != 0 || PlayerCards.Count != 0)
//                        EndTURN();
//                }
//                else
//                {
//                    FightController.NormalAttack(PlayerCards[_playerId], EnemyCards[_enemyId]);
//                    PlayerTurn = false;
//                    if (EnemyCards.Count != 0 || PlayerCards.Count != 0)
//                        EndTURN();
//                }
//            }
//            else
//            {
//                if (EnemyCards[_enemyId].GetComponent<CardDisplay>().Legendary == true && crit > 0 && crit < 25)
//                {
//                    FightController.KriticalAttack(EnemyCards[_enemyId], PlayerCards[_playerId]);
//                    PlayerTurn = true;
//                }
//                else
//                {
//                    FightController.NormalAttack(EnemyCards[_enemyId], PlayerCards[_playerId]);
//                    PlayerTurn = true;
//                }
//            }
//        }

        /*public void EndGame()
        {
            if (PlayerCards.Count == 0)
            {
                Debug.Log("Enemy Win");
                _endTurnButton.interactable = false;
                PlayerCards = null;
                EnemyCards = null;
                _playerTreit = null;
                _enemyTreit = null;
                NewGame = true;
            }
            else if (EnemyCards.Count == 0)
            {
                Debug.Log("You Win");
                ChangeEnemy();
            }
        }*/

        /*private void ChangeEnemy()
        {
            SetEnemyCards();
            foreach (var card in PlayerCards)
            {
                var hp = card.transform.Find("Health").GetComponent<Text>();
                hp.text = (Convert.ToInt32(Convert.ToInt32(hp.text) * 1.2f)).ToString();
            }
        }*/
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class GameController : MonoBehaviour
{
    public FightController fightController;
  

    [SerializeField] private Button endTurnButton;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject treitPrefab;
    public GameObject playerPanell;
    public GameObject enemyPanell;
    public static List<GameObject> playerCards = new List<GameObject>();
    public static List<GameObject> enemyCards = new List<GameObject>();
    public static List<GameObject> playerTreit = new List<GameObject>();
    public static List<GameObject> enemyTreit = new List<GameObject>();
    public static CardItemList cardItemList;
    public static CardTreitList cardTreitList;
    public static bool playerTurn = true;
    public static int playerID;
    public static int enemyID;
    public bool newGame;

    private void Awake()
    {
        cardItemList = AssetDatabase.LoadAssetAtPath("Assets/GameField/ScriptableObjects/CardItemList.asset", typeof(CardItemList)) as CardItemList;
        cardTreitList = AssetDatabase.LoadAssetAtPath("Assets/GameField/ScriptableObjects/CardTreitList.asset", typeof(CardTreitList)) as CardTreitList;
    }
    void Start()
    {
       // newGame = true;
        SetPlayerCards();
        SetEnemyCards();
    }

    void SetPlayerCards()
    {
        int playerCardsCount;
        if (newGame)
        {
            playerCardsCount = 3;
        }
        else { playerCardsCount = playerCards.Count; }
        for (int i = 0; i < playerCardsCount; i++)
        {
            int cardIndex;
            cardIndex = UnityEngine.Random.Range(0, cardItemList.cardList.Count);
            playerCards.Add(Instantiate(cardPrefab));
            var index = playerCards[i].GetComponent<CardDisplay>();
            index.indexCard = cardIndex;
            playerCards[i].transform.SetParent(playerPanell.transform);
        }
        SetTreitCardtoPlayer();
    }
    void SetEnemyCards()
    {
        int enemyCardsCount;
        if (newGame)
        {
            enemyCardsCount = 3;
        }
        else { enemyCardsCount = enemyCards.Count; }
        for (int i = 0; i < enemyCardsCount; i++)
        {
            int cardIndex;
            cardIndex = UnityEngine.Random.Range(0, cardItemList.cardList.Count);
            enemyCards.Add(Instantiate(cardPrefab));
            var index = enemyCards[i].GetComponent<CardDisplay>();
            index.indexCard = cardIndex;
            enemyCards[i].transform.SetParent(enemyPanell.transform);
        }
        SetTreitCardtoEnemy();
    }
    public void EndTURN()
    {
        playerID = UnityEngine.Random.Range(0, playerCards.Count);
        enemyID = UnityEngine.Random.Range(0, enemyCards.Count);
        int crit = UnityEngine.Random.Range(0, 100);
        if (playerTurn)
        {
            if (playerCards[playerID].GetComponent<CardDisplay>().Legendary == true && crit > 0 && crit < 25)
            {
                fightController.KriticalAttack(playerCards[playerID], enemyCards[enemyID]);
                playerTurn = false;
                if (enemyCards.Count != 0 || playerCards.Count != 0)
                    EndTURN();
            }
            else
            {
                fightController.NormalAttack(playerCards[playerID], enemyCards[enemyID]);
                playerTurn = false;
                if (enemyCards.Count != 0 || playerCards.Count != 0)
                    EndTURN();
            }
        }
        else
        {
            if (enemyCards[enemyID].GetComponent<CardDisplay>().Legendary == true && crit > 0 && crit < 25)
            {
                fightController.KriticalAttack(enemyCards[enemyID], playerCards[playerID]);
                playerTurn = true;
            }
            else
            {
                fightController.NormalAttack(enemyCards[enemyID], playerCards[playerID]);
                playerTurn = true;
            }
        }

    }
    public void EndGame()
    {
        if (playerCards.Count == 0)
        {
            Debug.Log("Enemy Win");
            endTurnButton.interactable = false;
            playerCards = null;
            enemyCards = null;
            playerTreit = null;
            enemyTreit = null;
            newGame = true;
        }
        else if (enemyCards.Count == 0)
        {
            Debug.Log("You Win");
            ChangeEnemy();
        }
    }
    private void ChangeEnemy()
    {
        SetEnemyCards();
        foreach (var card in playerCards)
        {
            var hp = card.transform.Find("Health").GetComponent<Text>();
            hp.text = (Convert.ToInt32(Convert.ToInt32(hp.text) * 1.2f)).ToString();
        }
    }
    private void SetTreitCardtoPlayer()
    {
        int treitListIndex = 0;
        int treitIndex;
        treitIndex = UnityEngine.Random.Range(0, cardTreitList.treitList.Count);
        playerTreit.Add(Instantiate(treitPrefab));
        var index = playerTreit[treitListIndex].GetComponent<TreitDisplay>();
        index.indexTreit = treitIndex;
        playerTreit[treitListIndex].transform.SetParent(playerPanell.transform);
        treitListIndex++;
    }
    private void SetTreitCardtoEnemy()
    {
        int treitListIndex = 0;
        int treitIndex;
        treitIndex = UnityEngine.Random.Range(0, cardTreitList.treitList.Count);
        enemyTreit.Add(Instantiate(treitPrefab));
        var index = enemyTreit[treitListIndex].GetComponent<TreitDisplay>();
        index.indexTreit = treitIndex;
        enemyTreit[treitListIndex].transform.SetParent(enemyPanell.transform);
        treitListIndex++;
    }

}

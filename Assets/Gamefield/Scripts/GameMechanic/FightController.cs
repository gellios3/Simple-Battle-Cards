/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using Gamefield.Scripts.GameMechanic;

public class FightController : MonoBehaviour
{
    public GameController gameController;
    private Text energyText;
    private Text healthText;
    private Text defenceText;
    private Text attackText;

    public void NormalAttack(GameObject agressor, GameObject victim)
    {
        Debug.Log("NormalAttak" + "Agressor " + agressor.name + "Victim " + victim.name);
        healthText = victim.transform.Find("Health").GetComponent<Text>();
        defenceText = victim.transform.Find("Defence").GetComponent<Text>();
        attackText = agressor.transform.Find("Damage").GetComponent<Text>();
        var defence = Convert.ToInt32(defenceText.text);
        var attack = Convert.ToInt32(attackText.text);
        var victimHealth = Convert.ToInt32(healthText.text);
        if (victimHealth > 0)
        {
            if (defence > 0 && defence >= attack)
            {
                defence = defence - attack;
            }
            else if (defence > 0 && defence < attack)
            {
                defence = defence - attack;
                victimHealth = victimHealth + defence;
            }
            else if (defence <= 0)
            {
                victimHealth = victimHealth - attack;
                if (victimHealth <= 0)
                {
                    Destroy(victim);
                    if (GameController.PlayerTurn)
                    {
                        GameController.EnemyCards.Remove(victim);
                        //Debug.Log(GameController.enemyCards.Count);
                    }
                    else
                    {
                        GameController.PlayerCards.Remove(victim);
                       // Debug.Log(GameController.playerCards.Count);
                    }
                }
            }
        }
        if (GameController.EnemyCards.Count == 0 || GameController.PlayerCards.Count == 0)
        {
            gameController.EndGame();
        }
       // Debug.Log(victimHealth + " health less");
        healthText.text = Convert.ToString(victimHealth);
        defenceText.text = defence.ToString();
    }

    public void KriticalAttack(GameObject agressor, GameObject victim)
    {
        Debug.Log("KriticalAttak" + "Agressor " + agressor.name + "Victim " + victim.name);
        healthText = victim.transform.Find("Health").GetComponent<Text>();
        defenceText = victim.transform.Find("Defence").GetComponent<Text>();
        attackText = agressor.transform.Find("Damage").GetComponent<Text>();
        var defence = Convert.ToInt32(defenceText.text);
        var attack = Convert.ToInt32(attackText.text);
        var victimHealth = Convert.ToInt32(healthText.text);
        if (victimHealth > 0)
        {

            if (defence > 0)
            {
                defence = 0;
                victimHealth = victimHealth - attack;
            }
            else if (defence == 0)
            {
                victimHealth = 0;
                Destroy(victim);
                if (GameController.PlayerTurn)
                {
                    GameController.EnemyCards.Remove(victim);
                  //  Debug.Log(GameController.enemyCards.Count);
                }
                else
                {
                    GameController.PlayerCards.Remove(victim);
                  //  Debug.Log(GameController.playerCards.Count);
                }
            }
        }
        if (GameController.EnemyCards.Count == 0 || GameController.PlayerCards.Count == 0)
        {
            gameController.EndGame();
        }
        //Debug.Log(victimHealth + " health less");
        healthText.text = Convert.ToString(victimHealth);
        defenceText.text = defence.ToString();
    }
    public void AddHealth()
    {

    }
    public void AddAttack()
    {

    }
    public void AddDefence()
    {

    }

}*/
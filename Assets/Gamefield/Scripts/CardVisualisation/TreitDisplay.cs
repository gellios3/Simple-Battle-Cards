using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class TreitDisplay : MonoBehaviour
{
    [SerializeField] private CardTreitList treit;
    public int indexTreit = 0;
    private Text treitNameText;
    private Text treitEnergyText;
    private Text treitHealthText;
    private Text treitDefenceText;
    private Text treitAttackText;
    private Image treitArtImage;
    private Image trietBackgroundImage;

    void Start()
    {
        TreitComponentFinder();
        TreitInitializer();
    }


    void TreitComponentFinder()
    {
        treitNameText = this.transform.Find("TreitName").GetComponent<Text>();
        treitEnergyText = this.transform.Find("TreitEnergy").GetComponent<Text>();
        treitHealthText = this.transform.Find("TreitHealth").GetComponent<Text>();
        treitDefenceText = this.transform.Find("TreitDefence").GetComponent<Text>();
        treitAttackText = this.transform.Find("TreitDamage").GetComponent<Text>();
        treitArtImage = this.transform.Find("ArtworkMask/TreitArtwork").GetComponent<Image>();
        trietBackgroundImage = this.transform.Find("CardTemplate/TreitBackground").GetComponent<Image>();
    }
    void TreitInitializer()
    {
        treitNameText.text = treit.treitList[indexTreit].cardName;
        treitEnergyText.text = treit.treitList[indexTreit].energy.ToString();
        treitHealthText.text = treit.treitList[indexTreit].healthModifier.ToString();
        treitDefenceText.text = treit.treitList[indexTreit].defenceModifier.ToString();
        treitAttackText.text = treit.treitList[indexTreit].damageModifier.ToString();
        treitArtImage.sprite = treit.treitList[indexTreit].artwork;
        trietBackgroundImage.sprite = treit.treitList[indexTreit].background;
    }
}

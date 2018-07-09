using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class CardDisplay : MonoBehaviour {

    //private Transform rootTransform;
    [SerializeField] private CardItemList card;
    public int indexCard=0;
    private Text nameText;
    private Text energyText;
    private Text healthText;
    private Text defenceText;
    private Text attackText;
    private Image artImage;
    private Image backgroundImage;
    public bool Legendary;

    void Start()
    {
        ComponentFinder();
        Initializer();
    }
    void ComponentFinder()
    {
        nameText = this.transform.Find("Name").GetComponent<Text>();
        energyText = this.transform.Find("Energy").GetComponent<Text>();
        healthText= this.transform.Find("Health").GetComponent<Text>();
        defenceText= this.transform.Find("Defence").GetComponent<Text>();
        attackText= this.transform.Find("Damage").GetComponent<Text>();
        artImage= this.transform.Find("ArtworkMask/Artwork").GetComponent<Image>();
        backgroundImage = this.transform.Find("CardTemplate/Background").GetComponent<Image>();
    }
    void Initializer()
    {
        nameText.text = card.cardList[indexCard].cardName;
        energyText.text = card.cardList[indexCard].energy.ToString();
        healthText.text = card.cardList[indexCard].health.ToString();
        defenceText.text = card.cardList[indexCard].defence.ToString();
        attackText.text = card.cardList[indexCard].damage.ToString();
        artImage.sprite = card.cardList[indexCard].artwork;
        backgroundImage.sprite = card.cardList[indexCard].background;
        Legendary = card.cardList[indexCard].isLegendary;
    }
}

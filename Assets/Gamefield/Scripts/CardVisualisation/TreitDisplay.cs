using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class TreitDisplay : MonoBehaviour
{
    //private Transform rootTransform;
    [SerializeField] private CardTreitList card;
    public int indexCard=0;
    private Text nameText;
    private Text energyText;
    private Text healthText;
    private Text defenceText;
    private Text attackText;
    private Image artImage;
    private Image backgroundImage;
    

    void Start()
    {
        ComponentFinder();
        Initializer();
    }
    void ComponentFinder()
    {
        nameText = this.transform.Find("Name").GetComponent<Text>();
        healthText= this.transform.Find("Health").GetComponent<Text>();
        defenceText= this.transform.Find("Defence").GetComponent<Text>();
        attackText= this.transform.Find("Damage").GetComponent<Text>();
        artImage= this.transform.Find("ArtworkMask/Artwork").GetComponent<Image>();
        backgroundImage = this.transform.Find("CardTemplate/Background").GetComponent<Image>();
    }
    void Initializer()
    {
        nameText.text = card.treitList[indexCard].cardName;
        healthText.text = card.treitList[indexCard].health.ToString();
        defenceText.text = card.treitList[indexCard].defence.ToString();
        attackText.text = card.treitList[indexCard].damage.ToString();
        artImage.sprite = card.treitList[indexCard].artwork;
        backgroundImage.sprite = card.treitList[indexCard].background;
    }
}

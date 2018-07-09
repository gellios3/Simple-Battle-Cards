using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateCardItem : MonoBehaviour
{

    [MenuItem("Assets/Create/Card Item List")]
    public static CardItemList Create()
    {
        var asset = ScriptableObject.CreateInstance<CardItemList>();

        AssetDatabase.CreateAsset(asset, "Assets/GameField/ScriptableObjects/CardItemList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
    [MenuItem("Assets/Create/Treit Card List")]
    public static CardTreitList CreateTreit()
    {
        var asset = ScriptableObject.CreateInstance<CardTreitList>();

        AssetDatabase.CreateAsset(asset, "Assets/GameField/ScriptableObjects/CardTreitList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

}

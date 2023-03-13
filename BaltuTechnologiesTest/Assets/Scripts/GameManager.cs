using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    private RestAPI restApi;
    public ItemDisplay itemDisplay;
    public TMP_Dropdown gEdropdown;
    public string currentGenderExpression;

    public Item[] torsoItems, legsItems, feetItems;
    public Item[][] itemsArrays;
    public Item[] suggestedOutfit = new Item[3];

    [Serializable]
    public class Item
    {
        public string title;
        public string link;
        public string imageURL;
    }

    public void CreateOutfit()
    {
        // Created suggested outfit
        for (int i = 0; i < 3; i++)
        {
            suggestedOutfit[i] = itemsArrays[i][UnityEngine.Random.Range(0, 48)];
        }
    }

    private void Start()
    {
        currentGenderExpression = gEdropdown.options[gEdropdown.value].text;

        itemsArrays = new Item[][] { torsoItems, legsItems, feetItems };
    }

    public void ChangeGenderExpression()
    {
        currentGenderExpression = gEdropdown.options[gEdropdown.value].text;
    }
}

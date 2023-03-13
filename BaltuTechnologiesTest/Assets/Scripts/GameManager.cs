using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    private RestAPI restApi;
    public ItemDisplay[] itemDisplay;

    // Settings
    public GameObject settingsPanel;
    public GameObject backgroundBlocker;
    public string currentGenderExpression;
    public TMP_Dropdown gEdropdown;
    public string currentPriceMax;
    public TMP_Dropdown priceMaxDropdown;

    public Item[] torsoItems, legsItems, feetItems;
    public Item[][] itemsArrays;
    public Item[] suggestedOutfit = new Item[3];

    [Serializable]
    public class Item
    {
        public string title;
        public string price;
        public string link;
        public string imageURL;
    }

    public void CreateOutfit()
    {
        // Created suggested outfit
        for (int i = 0; i < 3; i++)
        {
            int randomInt = UnityEngine.Random.Range(0, 48);
            suggestedOutfit[i] = itemsArrays[i][randomInt];
            float priceCheck = DeterminePrice(suggestedOutfit[i].price);

            // Filter out expensive items
            while (currentPriceMax == "< $50" && priceCheck >= 50.0f)
            {
                randomInt = UnityEngine.Random.Range(0, 48);
                suggestedOutfit[i] = itemsArrays[i][randomInt];
                priceCheck = DeterminePrice(suggestedOutfit[i].price);
            }

            // Set up item display
            itemDisplay[i].item = suggestedOutfit[i];
            itemDisplay[i].UpdateDisplay();
        }
    }

    private void Start()
    {
        currentGenderExpression = gEdropdown.options[gEdropdown.value].text;
        currentPriceMax = priceMaxDropdown.options[priceMaxDropdown.value].text;

        itemsArrays = new Item[][] { torsoItems, legsItems, feetItems };
    }

    float DeterminePrice(string priceText)
    {
        if (priceText == "" || priceText == null)
        {
            return 0;
        }

        return float.Parse(priceText);
    }

    public void ChangeGenderExpression()
    {
        currentGenderExpression = gEdropdown.options[gEdropdown.value].text;
    }

    public void ChangePriceMax()
    {
        currentPriceMax = priceMaxDropdown.options[priceMaxDropdown.value].text;
    }

    public void ToggleSettingsMenu()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
        backgroundBlocker.SetActive(settingsPanel.activeInHierarchy);
    }
}

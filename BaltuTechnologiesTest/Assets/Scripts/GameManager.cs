using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public RestAPI restApi;
    public Item[] items;
    public ItemDisplay itemDisplay;
    public Item headItem, torsoItem, legsItem, feetItem;
    public List<Item> testItems = new List<Item>();

    [Serializable]
    public class Item
    {
        public string title;
        public string link;
        public string imageURL;
    }

    public void CreateOutfit(int index)
    {
        itemDisplay.item = items[index];
    }
}

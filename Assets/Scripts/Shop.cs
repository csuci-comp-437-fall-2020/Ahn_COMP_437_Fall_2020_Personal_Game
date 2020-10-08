using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject itemContainerPrefab;
    public int numOfItems;
    public PlatformerPlayer player;

    private GridLayoutGroup grid;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        grid = GetComponentInChildren<GridLayoutGroup>();
        PopulateShop();
    }

    private void PopulateShop()
    {
        GameObject newItem;

        for(int i = 0; i < numOfItems; i++)
        {
            newItem = (GameObject)Instantiate(itemContainerPrefab, transform);
            Image[] icon = newItem.GetComponentsInChildren<Image>();
            //Index 1 is the icon image, not the background image
            icon[1].sprite = newItem.GetComponentInChildren<Item>().sprite;
            newItem.GetComponentInChildren<Text>().text = "x " + newItem.GetComponentInChildren<Item>().cost;

            newItem.transform.SetParent(grid.transform);
            
            //I had help for getting this line from 
            //https://answers.unity.com/questions/1288510/buttononclickaddlistener-how-to-pass-parameter-or.html
            newItem.GetComponent<Button>().onClick.AddListener(delegate{BuyItem(newItem);});
        }
    }

    private void BuyItem(GameObject item)
    {
        Item buyingItem = item.GetComponentInChildren<Item>();
        Item.ITEM_TYPE itemType = buyingItem.type;

        if(player.collectables >= buyingItem.cost)
        {
            switch(itemType)
            {
                case Item.ITEM_TYPE.GREEN_KEY:
                    player.hasGreenKey = true;
                    break;
            }
            player.collectables -= buyingItem.cost;
        }
    }
}

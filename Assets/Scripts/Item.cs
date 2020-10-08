using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ITEM_TYPE {DANGO, HEALTH, DOUBLE_JUMP, 
    GROUND_POUND, DASH, RED_KEY, BLUE_KEY, GREEN_KEY};

    public ITEM_TYPE type;
    
    [SerializeField] public int cost;
    [SerializeField] public Sprite sprite;

}

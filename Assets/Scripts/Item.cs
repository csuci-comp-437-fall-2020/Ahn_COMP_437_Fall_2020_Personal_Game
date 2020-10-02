using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ITEM_TYPE {NORMAL, DOUBLE_JUMP, GROUND_POUND, DASH, KEY};

    public ITEM_TYPE type = ITEM_TYPE.NORMAL;

    void OnTriggerEnter2D (Collider2D collider)
    {
        PlatformerPlayer player = collider.GetComponent<PlatformerPlayer>();

        if (player != null)
        {
            player.collectables += 1;
            switch(type)
            {
                case ITEM_TYPE.DOUBLE_JUMP:
                    player.canDoubleJump = true;
                    break;
                case ITEM_TYPE.GROUND_POUND:
                    player.canGroundPound = true;
                    break;
                case ITEM_TYPE.DASH:
                    player.canDash = true;
                    break;
                case 0:
                    break;
            }
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

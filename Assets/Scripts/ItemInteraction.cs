using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    //private Item item;
    public Item.ITEM_TYPE type = Item.ITEM_TYPE.DANGO;
    public float floatStrength = 0.5f;

    private Animator _animation;

    private float originalY;

    void OnTriggerEnter2D (Collider2D collider)
    {
        PlatformerPlayer player = collider.GetComponent<PlatformerPlayer>();

        if (player != null)
        {
            switch(type)
            {
                case Item.ITEM_TYPE.DANGO:
                    player.collectables++;
                    break;
                case Item.ITEM_TYPE.HEALTH:
                    player._health.health++;
                    break;
                case Item.ITEM_TYPE.DOUBLE_JUMP:
                    player.canDoubleJump = true;
                    break;
                case Item.ITEM_TYPE.GROUND_POUND:
                    player.canGroundPound = true;
                    break;
                case Item.ITEM_TYPE.DASH:
                    player.canDash = true;
                    break;
                case Item.ITEM_TYPE.RED_KEY:
                    player.hasRedKey = true;
                    break;
                case Item.ITEM_TYPE.GREEN_KEY:
                    player.hasGreenKey = true;
                    break;
                case Item.ITEM_TYPE.BLUE_KEY:
                    player.hasBlueKey = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animator>();
        this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(type != Item.ITEM_TYPE.DANGO && type != Item.ITEM_TYPE.HEALTH)
        {
            transform.position = new Vector2(transform.position.x, originalY 
                + ((float)Mathf.Sin(Time.time) * floatStrength));
        }
       
    }
}

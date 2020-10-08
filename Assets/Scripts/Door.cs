using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DOOR_TYPE {RED, BLUE, GREEN, GROUND};
    private SpriteRenderer _sprite;

    [SerializeField] private Color red;
    [SerializeField] private Color blue;
    [SerializeField] private Color green;
    [SerializeField] private Color ground;

    public DOOR_TYPE doorType;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            PlatformerPlayer player = collider.gameObject.GetComponent<PlatformerPlayer>();

            if(player.hasRedKey && doorType == DOOR_TYPE.RED)
            {
                Destroy(gameObject);
            }

            if(player.hasBlueKey && doorType == DOOR_TYPE.BLUE)
            {
                Destroy(gameObject);
            }

            if(player.hasGreenKey && doorType == DOOR_TYPE.GREEN)
            {
                Destroy(gameObject);
            }

            if(player.groundPounding && doorType == DOOR_TYPE.GROUND)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if(doorType == DOOR_TYPE.RED)
        {
            _sprite.color = red;
        }
        if(doorType == DOOR_TYPE.BLUE)
        {
            _sprite.color = blue;
        }
        if(doorType == DOOR_TYPE.GREEN)
        {
            _sprite.color = green;
        }
        if(doorType == DOOR_TYPE.GROUND)
        {
            _sprite.color = ground;
        }
    }
}

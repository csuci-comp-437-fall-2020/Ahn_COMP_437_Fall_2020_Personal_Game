using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public float talkRange = 1.5f;
    public LayerMask player;
    public GameObject shopUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, talkRange, player);
        if(hit != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //this freezes everything that moves
                Time.timeScale = 0;
                shopUI.SetActive(true);
            }
        }
    }
}

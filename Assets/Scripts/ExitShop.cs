using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitShop : MonoBehaviour
{
    public GameObject shopUI;
    // Start is called before the first frame update
    void Awake()
    {   
        GetComponent<Button>().onClick.AddListener(ExitWindow);
    }

    void ExitWindow()
    {
        //this unfreezes everything that moves
        Time.timeScale = 1;
        shopUI.SetActive(false);
    }

    
}

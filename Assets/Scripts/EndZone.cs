using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public Transform endIndicator;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D player = Physics2D.Raycast(endIndicator.position, Vector2.up, 0.2f);
        if(player.collider != null && player.collider.tag == "Player")
        {
            Time.timeScale = 0;
            player.collider.gameObject.GetComponent<PlatformerPlayer>().restartMenu.SetActive(true);
        }
    }
}

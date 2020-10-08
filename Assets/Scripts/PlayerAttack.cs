using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Most of this code is from this tutorial:
    //https://www.youtube.com/watch?v=sPiVz1k-fEs
    //Of course, with a few changes.

    private Animator _animation;
    private PlatformerPlayer player;
    
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animator>();
        player = GetComponent<PlatformerPlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !player.dashing && !player.groundPounding)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animation.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            //Maybe fix this so it feels better to kill?
            enemy.gameObject.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

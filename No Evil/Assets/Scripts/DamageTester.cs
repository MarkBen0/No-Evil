using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public AttributeManager playerAtm;
    public AttributeManager enemyAtm;
    public baseEnemyAI enemy;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            enemy.TakeDamage();
        }

        if (Input.GetKeyDown("n"))
        {
            enemyAtm.DealDamage(playerAtm.gameObject);
        }
    }
}

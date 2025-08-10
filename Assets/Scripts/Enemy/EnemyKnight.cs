using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnight : Enemy
{
    private List<Enemy> m_enemiesToBuff = new List<Enemy>();
    [SerializeField] private float bonusSpeed = 1f;


    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !m_enemiesToBuff.Contains(enemy))
        {
            enemy.ChangeSpeed(bonusSpeed);
            m_enemiesToBuff.Add(enemy);
        }
    }
    //Summary
    // removes enemy who walks out of range from list
    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && m_enemiesToBuff.Contains(enemy))
        {
            enemy.ChangeSpeed(-bonusSpeed);
            m_enemiesToBuff.Remove(enemy);
        }
    }

    protected override void Die()
    {
        ClearBonusList();
        base.Die();
    }

    private void ClearBonusList()
    {
        foreach (Enemy enemy in m_enemiesToBuff)
        {
            enemy.ChangeSpeed(-bonusSpeed);
            m_enemiesToBuff.Remove(enemy);
        }
    }
}

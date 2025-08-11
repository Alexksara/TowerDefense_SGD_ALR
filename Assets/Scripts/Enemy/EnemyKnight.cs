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

    public override void Die()
    {
        ClearBonusList();
        base.Die();
    }

    private void ClearBonusList()
    {
        for(int i = 0;i< m_enemiesToBuff.Count;i++)
        {
            if(m_enemiesToBuff[i] != null && i< m_enemiesToBuff.Count)
            {
                m_enemiesToBuff[i].ChangeSpeed(-bonusSpeed);
                m_enemiesToBuff.Remove(m_enemiesToBuff[i]);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BossesManager",menuName = "ScriptableObjects/BossesManager",order =1)]
public class BossesManager : ScriptableObject
{
    [System.Serializable]
    public struct Boss
    {
        public int id_boss;
        public string name_Boss;
        public float health;
        public float damage;
    }
    public Boss[] listBosses;
    public int GetIndexBoss(int _bossID)
    {
        for(int i=0;i<listBosses.Length;i++)
        {
            if (listBosses[i].id_boss == _bossID)
            {
                return i;
            }
        }
        return -1;
    }
}

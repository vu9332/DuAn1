using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Asset/StoreBosses",menuName ="StoreBosses",order =1)]
public class Bosses : ScriptableObject
{
    [System.Serializable]
   public struct Boss
   {
        public float damage;
        public float health;
   }
    public Boss[] bosses;
}

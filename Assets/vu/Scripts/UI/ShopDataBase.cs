using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="newItem",menuName ="Shopping/newItem")]
public class ShopDataBase : ScriptableObject
{
    public float NumberOfPurchases;
    public float price;
    public float howMuchMore;
  //  public Slider statusBar;
    public PlayerData playerData;
   // public PlayerHealth playerHealth;
}

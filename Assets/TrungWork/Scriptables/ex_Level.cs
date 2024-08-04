using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level & Experience Manager",menuName = "ScriptableObjects/Level & Experience Manager")]
public class ex_Level : ScriptableObject
{
    public int maxLevel;
    [Header("Fill the percent(%) you want to increase amount Exp need to up level")]
    public float rateExperiencesUpLevel;
}

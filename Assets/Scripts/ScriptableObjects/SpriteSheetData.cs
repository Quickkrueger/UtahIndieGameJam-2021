using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteSheetData", menuName = "ScriptableObjects/Sprite/SpriteSheetData", order = 1)]
public class SpriteSheetData : ScriptableObject
{
    public Sprite[] spriteSheet;
}

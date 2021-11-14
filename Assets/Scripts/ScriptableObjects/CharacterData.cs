using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Entity/CharacterData", order = 1)]
public class CharacterData : CollectableSO
{
    public TextureData spriteSheet;
    public int characterSpriteOffset = 0;
    public AnimationData[] animations;
    public Stats characterStats;
}

[System.Serializable]
public struct Stats
{
    public float speed;
    public float jumpHeight;
    public bool durable;
    public bool flies;
    public bool climbs;
}
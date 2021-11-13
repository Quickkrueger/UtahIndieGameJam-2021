using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Entity/CharacterData", order = 1)]
public class CharacterData : CollectableSO
{
    public TextureData spriteSheet;
    public int characterSpriteOffset = 0;
    public AnimationData[] animations;
    public Stats characterStats;

    public CharacterHusk currentHusk;
}

[System.Serializable]
public struct Stats
{
    public float speed;
    public float jumpHeight;
}
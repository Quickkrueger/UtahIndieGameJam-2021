using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Entity/CharacterData", order = 1)]
public class CharacterData : CollectableSO
{
    public SpriteSheetData spriteSheet;
    public AnimationData[] animations;
    public Stats characterStats;

    public AnimationData GetAnimationByName(string animationName)
    {
        for(int i = 0; i < animations.Length; i++)
        {
            if(animationName == animations[i].animationID)
            {
                return animations[i];
            }
        }

        return null;
    }

    public int GetIndexByName(string animationName)
    {
        for (int i = 0; i < animations.Length; i++)
        {
            if (animationName == animations[i].animationID)
            {
                return i;
            }
        }

        return -1;
    }
}

[System.Serializable]
public struct Stats
{
    public float speed;
    public float jumpHeight;
    public int numJumps;
    public bool durable;
    public bool flies;
    public bool climbs;
}
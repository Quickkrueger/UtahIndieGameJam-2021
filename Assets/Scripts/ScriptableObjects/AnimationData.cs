using UnityEngine;

[CreateAssetMenu(fileName = "AnimationData", menuName = "ScriptableObjects/Sprite/AnimationData", order = 1)]
public class AnimationData : ScriptableObject
{
    public string animationID;
    public int[] spriteIndexes;
    public float durationInSeconds = 1f;
}

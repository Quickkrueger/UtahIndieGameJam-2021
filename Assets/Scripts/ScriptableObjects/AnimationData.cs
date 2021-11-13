using UnityEngine;

[CreateAssetMenu(fileName = "AnimationData", menuName = "ScriptableObjects/Sprite/AnimationData", order = 1)]
public class AnimationData : ScriptableObject
{
    public string animationID;
    public Vector2Int[] spriteCoords;
    public float durationInSeconds = 1f;
}

using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character/Character Data")]
public class CharData : ScriptableObject
{
    public string charName;
    public Sprite icon;
    public Sprite vsArt;
}

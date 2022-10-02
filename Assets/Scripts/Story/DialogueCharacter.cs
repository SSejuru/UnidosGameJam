using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Dialogue Character")]
public class DialogueCharacter : ScriptableObject
{
    public string characterName;
    public Sprite sprite;
}

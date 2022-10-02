using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Dialogue ",menuName = "Dialogue")]
public class DialogueStory : ScriptableObject
{
    public List<Dialogue> _dialogue = new List<Dialogue>();
}

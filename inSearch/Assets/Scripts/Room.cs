using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Room")]
public class Room : ScriptableObject
{
    public string roomName;
    [TextArea] public string narration;
    [HideInInspector] public Word[] words;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Room")]
public class Room : ScriptableObject
{
    public string roomName;
    [TextArea] public string story;
    [HideInInspector] public Word[] words;
}

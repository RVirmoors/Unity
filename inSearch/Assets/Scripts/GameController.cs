using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text narration;
    [HideInInspector] public RoomNavigation roomNavigation;

    private void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }

    private void Start()
    {
        DisplayRoomText();
    }

    public void DisplayRoomText()
    {
        //narration.text = roomNavigation.currentRoom.narration;
    }
}

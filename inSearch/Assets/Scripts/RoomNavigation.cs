using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;
    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void changeRoom(Room to)
    {
        currentRoom = to;
    }
}

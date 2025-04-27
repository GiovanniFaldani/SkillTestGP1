using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour
{
    //place this script on the player gameobject

    [SerializeField] private int followDistance = 200;
    private Player player; // in the inspector drag the gameobject the will be following the player to this field
    private List<Vector2> storedPositions = new List<Vector2>();


    void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }

    void Update()
    {
        if (storedPositions.Count == 0)
        {
            storedPositions.Add(new Vector2(player.transform.position.x - 0.8f, player.transform.position.y + 0.8f)); //store target position relative to player
        }
        else if (storedPositions[storedPositions.Count - 1] != (Vector2)player.transform.position)
        {
            storedPositions.Add(new Vector2(player.transform.position.x - 0.8f, player.transform.position.y + 0.8f)); //store the position every frame
        }

        if (storedPositions.Count > followDistance)
        {
            transform.position = storedPositions[0]; //move
            storedPositions.RemoveAt(0); //delete the position the object just moved to
        }
    }
}
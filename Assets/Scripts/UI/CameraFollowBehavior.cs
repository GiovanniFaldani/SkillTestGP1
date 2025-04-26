using UnityEngine;

public class CameraFollowBehavior : MonoBehaviour
{
    private Transform playerRef;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = FindFirstObjectByType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(playerRef.position.x, 0, -10);
    }
}

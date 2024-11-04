using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 0, 2); // Offset in front of the player

    void Update()
    {
        if (player != null)
        {
            // Set the position of the interaction object to the player's position plus the offset
            transform.position = player.position + player.TransformDirection(offset);
            // Optionally, make it face the player
            transform.LookAt(player.position);
        }
    }
}

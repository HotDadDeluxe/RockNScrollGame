using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;      // The player the camera will follow
    [SerializeField] private float smoothSpeed = 0.125f;  // How smooth the camera follows
    [SerializeField] private Vector3 offset;       // Offset from the player (e.g. height or distance)

    [SerializeField] private Vector3 boundsMin;    // Minimum bounds for camera position
    [SerializeField] private Vector3 boundsMax;    // Maximum bounds for camera position

    private void LateUpdate()
    {
        if (player != null)
        {
            // Get the desired position based on player's position + offset
            Vector3 targetPos = player.position + offset;

            // Clamp the position to stay within the bounds
            targetPos.x = Mathf.Clamp(targetPos.x, boundsMin.x, boundsMax.x);
            targetPos.y = Mathf.Clamp(targetPos.y, boundsMin.y, boundsMax.y);

            // Interpolate smoothly between current position and target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

            // Set camera's position
            transform.position = smoothedPosition;
        }
    }
}
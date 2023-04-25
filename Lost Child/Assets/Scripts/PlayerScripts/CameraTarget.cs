using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    // Variables
    public Camera cam;
    public Transform player;
    public float threshold;

    // Update is called once per frame
    void Update()
    {
        // Get Mouse and Target Position
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos) / 2f;

        // Set Target Postion
        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        // Get Direction and angle
        Vector2 direction = mousePos - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        this.transform.position = targetPos;
        // Rotate Player if needed
        //player.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}

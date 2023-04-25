using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInformations : MonoBehaviour
{
    public Text fpsText;
    public Text speedText;
    public Rigidbody2D rb;
    private float fps;
    private float playerSpeed;  
    void Update()
    {
        float newFPS = 1.0f / Time.deltaTime;
        fps = Mathf.Lerp(fps, newFPS, 0.0005f);
        fpsText.text = "FPS: " + fps;
    }
}

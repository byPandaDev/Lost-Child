using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private Inventory_UI inventory;
    [Header("Player")]
    public GameObject player;
    [Header("Interaction Item")]
    public GameObject itemToInteract;
    [Header("Animation")]
    public Animator animatior;
    [Header("Informations")]
    public bool isLocked = true;
    public bool isOpen = false;

    private bool isEPressed = false;
    private bool isInTrigger = false;

    private void Awake()
    {
        inventory = player.GetComponent<Inventory_UI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) isEPressed = true;
        else isEPressed = false;
        // Check if in Trigger and E is pressed
        if (isEPressed && isInTrigger)
        {
            if (isLocked && Array.IndexOf(inventory.items, itemToInteract) > -1)
            {
                isLocked = false;
            } 
            if(!isLocked)
            {
                isOpen = !isOpen;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTrigger = false;
    }
}

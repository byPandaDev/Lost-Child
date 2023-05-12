using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    
    [SerializeField] private Key.KeyType keyType;
    [SerializeField] private Animator DoorAnim;

    private void Awake()
    {
        DoorAnim = GetComponent<Animator>();
    }
    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        DoorAnim.Play("OpenDoor");
    }

}

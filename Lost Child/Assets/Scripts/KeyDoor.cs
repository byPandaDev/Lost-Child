using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    
    [SerializeField] private Key.KeyType keyType;
    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        anim.IsPlaying("Open Door");
    }
    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        anim.Play("Open Door");
    }

}

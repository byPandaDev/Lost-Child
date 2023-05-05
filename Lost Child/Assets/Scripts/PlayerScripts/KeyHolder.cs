using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;

    private void Awake() 
    {
    keyList = new List<Key.KeyType>();
    }

    public void AddKey(Key.KeyType keyType)                     //Here we have a Methode to add the keytype to list 
    {
        keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType)                  //Here we have a Methode to remove the keytype from list 
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)                //Here we have a bool saying if we contain key or not
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D collider)          //Here the collision check happens to pick up key with collider trigger 
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)                                        //if collider doesnt exist nothing happens but if collider exists ,execute \/
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();     
        if (keyDoor != null)                                    //if collider doesnt exist nothing happens but if collider exists ,execute \/
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                //Currently holding key to open door
                keyDoor.OpenDoor();
            }
            
        }
    }
}

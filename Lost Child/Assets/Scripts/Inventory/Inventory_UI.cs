using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{

    public GameObject inventory;
    [Header("Inventory")]
    public Image[] slots;
    [Header("Item Info")]
    public Image itemInfoImage;
    public TMP_Text itemInfoName;
    private bool[] isUsed;
    private string[] itemNames;
    public GameObject[] items;

    private bool isShowing = false;

    private enum Items
    {
        Key
    }

    private void Awake()
    {
        isUsed = new bool[slots.Length];
        itemNames = new string[slots.Length];
        items = new GameObject[slots.Length];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isShowing = !isShowing;
            inventory.gameObject.SetActive(isShowing);
        }
    }

    public void SelectItem(int itemnmb)
    {
        int item = 0;
        if (itemnmb > 0 && itemnmb < slots.Length) item = itemnmb - 1;

        if (isUsed[item]) 
        {
            itemInfoImage.sprite = slots[item].sprite;
            itemInfoName.text = itemNames[item];
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            
            for (int i = 0; i < isUsed.Length; i++)
            {
                if (!isUsed[i])
                {
                    slots[i].GetComponent<Image>().enabled = true;
                    slots[i].sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                    itemNames[i] = collision.gameObject.name;
                    items[i] = collision.gameObject;
                    isUsed[i] = true;
                    Destroy(collision.gameObject);
                    break;
                }
            }
        }
    }
}

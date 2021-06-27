using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField]
    Item items_in_slot;
    [SerializeField]
    Button delete_item_button, substract_item_button, info_button;
    [SerializeField]
    TextMeshProUGUI item_count_text, item_name;
    [SerializeField]
    Image image_spot;
    [SerializeField]
    int item_count;
    public bool is_empty = true;

    public int Item_count { get => item_count; set => item_count = value; }

    private void Awake()
    {
        delete_item_button = GetComponentsInChildren<Button>()[0];
        info_button = GetComponentsInChildren<Button>()[1];
        substract_item_button = GetComponentsInChildren<Button>()[2];
        item_count_text = gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0];
        item_name = gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1];
        image_spot = gameObject.GetComponentsInChildren<Image>()[1];
    }
   
    public void add_existing_item()
    {
        item_count++;
        item_count_text.text = item_count.ToString();
    }

    public void add_new_item(Item new_item)
    {
        items_in_slot = new_item;
        item_count = 1;
        item_count_text.text = item_count.ToString();
        substract_item_button.interactable = true;
        info_button.interactable = true;
        delete_item_button.interactable = true;
        item_name.text = new_item.gameObject.name;
        image_spot.enabled = true;
        image_spot.sprite = new_item.GetComponent<SpriteRenderer>().sprite;
        is_empty = false;
    }

    public void substract_item(int count = 1)
    {
        item_count-=count;
        item_count_text.text = item_count.ToString();
        if (item_count <= 0)
        {
            delete_item();
        }
    }

    public void delete_item()
    {
        items_in_slot = new Item();
        item_count = 0;
        image_spot.enabled = false;
        item_name.text = "";
        item_count_text.text = "";
        substract_item_button.interactable = false;
        info_button.interactable = false;
        delete_item_button.interactable = false;
        is_empty = true;
    }

    public Item GetItem()
    {
        return items_in_slot;
    }
}

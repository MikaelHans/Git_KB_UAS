
using UnityEngine;

public class Item : MonoBehaviour, Interactables
{
    public string item_name;

    public Item(string item_name = "Not Set")
    {
        this.item_name = item_name;
    }

    public void interact(Player player)
    {
        Destroy(gameObject);
    }
}

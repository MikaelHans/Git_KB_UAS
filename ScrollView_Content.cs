using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView_Content : MonoBehaviour
{
    //ini di content btw
    public GameObject prefab;

    public void add_item(Quest added_quest)
    {
        Instantiate(prefab, transform);
        Quest_Container[] container_array = GetComponentsInChildren<Quest_Container>();
        container_array[container_array.Length - 1].Setup_Quest(added_quest);
    }

    public void clear_all()
    {
        GetComponentInParent<Canvas>().enabled = false;
        foreach (Quest_Container quest in GetComponentsInChildren<Quest_Container>())
        {
            Destroy(quest.gameObject);
        }
    }
}

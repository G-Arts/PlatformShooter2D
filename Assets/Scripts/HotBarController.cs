using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarController : MonoBehaviour
{
    [SerializeField] private List<Image> backgrounds;

    public void selectItem(int index)
    {
        if (index > backgrounds.Count - 1 || index < 0) return;

        for(int i = 0;i < backgrounds.Count; i++)
        {
            backgrounds[i].gameObject.SetActive(false);
        }

        backgrounds[index].gameObject.SetActive(true);
    }
}

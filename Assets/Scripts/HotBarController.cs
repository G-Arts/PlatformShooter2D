using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarController : MonoBehaviour
{
    [SerializeField] private List<Image> backgrounds;

    public static HotBarController instance { get; private set; }

    public int currentWeaponIndex { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            Debug.Log("Hot Bar Instance Oluþturuldu !!!!!");
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }


    public void selectItem(int index)
    {
        if (index > backgrounds.Count - 1 || index < 0) return;

        currentWeaponIndex = index;

        for(int i = 0;i < backgrounds.Count; i++)
        {
            backgrounds[i].gameObject.SetActive(false);
        }

        backgrounds[index].gameObject.SetActive(true);
    }


    private void OnDestroy()
    {
        // Reset instance if this object is destroyed
        if (instance == this)
        {
            instance = null;
        }
    }
}

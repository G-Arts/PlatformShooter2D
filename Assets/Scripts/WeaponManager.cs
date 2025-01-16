using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private WeaponDatabase weaponDatabase;
    //[SerializeField] private HotBarController hotBarController;
    private GameObject currentWeapon;
    private Dictionary<int, string> weaponMapping;
    public int currentIndex = 0;
    private bool isLoading = false;

    private void Start()
    {
        InitializeWeaponMapping();
    }

    private void Update()
    {
        if (!isLoading)
        {
            HandleWeaponScroll();
            HandleHotBarKeyInput();
        }  
    }

    private void InitializeWeaponMapping()
    {
        weaponMapping = new Dictionary<int, string>();

        for (int i = 0; i < weaponDatabase.weapons.Length; i++)
        {
            weaponMapping[i] = weaponDatabase.weapons[i].weaponName;
        }
    }

    private void HandleWeaponScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            UpdateHotBarIndex((int)Input.mouseScrollDelta.y);
            SelectWeaponByIndex(currentIndex);
        }
    }

    private void HandleHotBarKeyInput()
    {
        for (int i = 0; i < weaponMapping.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectWeaponByIndex(i);
                break;
            }
        }
    }

    private void UpdateHotBarIndex(int move)
    {
        currentIndex = (currentIndex + move + weaponMapping.Count) % weaponMapping.Count;
        //hotBarController.selectItem(currentIndex);
        HotBarController.instance.selectItem(currentIndex);
    }

    private void SelectWeaponByIndex(int index)
    {

        if(EquipWeaponWIndex(index)) HotBarController.instance.selectItem(index);
        else
        {
            Debug.LogWarning($"Index {index} i�in bir silah bulunamad�.");
            return;
        }
        currentIndex = index;
        ColyseusManager.instance.ServerMessageSend("weaponIndex", currentIndex.ToString());
    }

    public bool EquipWeaponWIndex(int index)
    {
        if (weaponMapping.TryGetValue(index, out string weaponKey))
        {
            EquipWeapon(weaponKey);
            return true;
        }
        return false;
    }

    public void SelectWeaponByName(string name)
    {
        // Silah veritaban�nda ad� e�le�en silah� bul
        WeaponData weapon = System.Array.Find(weaponDatabase.weapons, w => w.weaponName == name);
        if (weapon != null)
        {
            // Silah�n hotbar'daki indeksini bul
            int weaponIndex = System.Array.IndexOf(weaponDatabase.weapons, weapon);

            if (weaponIndex >= 0)
            {
                // Silah� ku�an
                EquipWeapon(weapon.weaponName);

                // Hotbar'da ilgili silah� se� ve vurgula
                //hotBarController.selectItem(weaponIndex);

                HotBarController.instance.selectItem(weaponIndex);
            }
            else
            {
                Debug.LogWarning($"Silah veritaban�nda bir indeks bulunamad�: {name}");
            }
        }
        else
        {
            Debug.LogError($"Silah bulunamad�: {name}");
        }
    }


    public void EquipWeapon(string weaponKey)
    {
        
        isLoading = true;
        Addressables.LoadAssetAsync<GameObject>(weaponKey).Completed += OnWeaponLoaded;
    }

    private void OnWeaponLoaded(AsyncOperationHandle<GameObject> handle)
    {
        
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            currentWeapon = Instantiate(handle.Result, transform.position, transform.rotation, transform);
            character.EquipWeapon(currentWeapon.GetComponent<Weapon>());
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError($"Silah y�klenemedi: {handle.DebugName}, Hata: {handle.OperationException}");
        }

        isLoading = false;
    }
}

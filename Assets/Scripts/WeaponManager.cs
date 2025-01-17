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
    private bool isServer = false;

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
        if (isServer) return;
        if (Input.mouseScrollDelta.y != 0)
        {
            UpdateHotBarIndex((int)Input.mouseScrollDelta.y);
            SelectWeaponByIndex(currentIndex);
        }
    }

    private void HandleHotBarKeyInput()
    {
        if(isServer) return;
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
            Debug.LogWarning($"Index {index} için bir silah bulunamadý.");
            return;
        }
        currentIndex = index;
        ColyseusManager.instance.ServerMessageSend("weaponIndex", currentIndex.ToString());
    }

    public bool EquipWeaponWIndex(int index)
    {
        if (weaponMapping.TryGetValue(index, out string weaponKey))
        {
            Debug.Log("WIndex Silah kuþanýldý!!!!");
            EquipWeapon(weaponKey);
            return true;
        }
        return false;
    }

    public void SelectWeaponByName(string name)
    {
        // Silah veritabanýnda adý eþleþen silahý bul
        WeaponData weapon = System.Array.Find(weaponDatabase.weapons, w => w.weaponName == name);
        if (weapon != null)
        {
            // Silahýn hotbar'daki indeksini bul
            int weaponIndex = System.Array.IndexOf(weaponDatabase.weapons, weapon);

            if (weaponIndex >= 0)
            {
                // Silahý kuþan
                EquipWeapon(weapon.weaponName);

                // Hotbar'da ilgili silahý seç ve vurgula
                //hotBarController.selectItem(weaponIndex);

                HotBarController.instance.selectItem(weaponIndex);
            }
            else
            {
                Debug.LogWarning($"Silah veritabanýnda bir indeks bulunamadý: {name}");
            }
        }
        else
        {
            Debug.LogError($"Silah bulunamadý: {name}");
        }
    }

    public void setWeaponRot(float x,float y,float z)
    {
        Quaternion rot = Quaternion.Euler(x,y,z);
        if (rot == null) return;
        currentWeapon.transform.localRotation = rot;
    }

    public Vector3 getWeaponRot()
    {
        if(currentWeapon == null)return Vector3.zero;

        Vector3 _wepRot = currentWeapon.transform.localRotation.eulerAngles;

        return _wepRot;
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
            Weapon _weapon = currentWeapon.GetComponent<Weapon>();
            _weapon.setIsServer(isServer);
            character.EquipWeapon(_weapon);
            currentWeapon.transform.localRotation = Quaternion.identity;

            Debug.Log(currentWeapon.name);
        }
        else
        {
            Debug.LogError($"Silah yüklenemedi: {handle.DebugName}, Hata: {handle.OperationException}");
        }

        isLoading = false;
    }

    public void setServer()
    {
        isServer = true;
    }
}

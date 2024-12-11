using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]private GameObject WeaponCollecter;
    [SerializeField]private WeaponDatabase _weaponDatabase;

    public void SpawnWeaponCollector()
    {
        GameObject newCollecter = Instantiate(WeaponCollecter, new Vector3(-10, -7, 0), Quaternion.identity);
        WeaponCollecter wcollector = newCollecter.GetComponent<WeaponCollecter>();
        wcollector.setWeaponCollecter(_weaponDatabase.weapons[(int)Random.Range(0, _weaponDatabase.weapons.Length)]);
    }
}

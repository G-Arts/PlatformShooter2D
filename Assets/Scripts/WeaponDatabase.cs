using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Database/Weapon Database")]
public class WeaponDatabase : ScriptableObject
{
    public WeaponData[] weapons; // T�m silahlar� i�eren bir dizi
}

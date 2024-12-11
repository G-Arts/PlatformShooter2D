using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Database/Weapon Database")]
public class WeaponDatabase : ScriptableObject
{
    public WeaponData[] weapons; // Tüm silahlarý içeren bir dizi
}

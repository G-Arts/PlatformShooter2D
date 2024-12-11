using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string weaponName;   // Silah adý
    public string address;      // Addressables adresi
    public int damage;          // Silah hasarý
    public float fireRate;      // Atýþ hýzý
    public int ammoCount;       // Mermi kapasitesi
    public Sprite sprite;
}

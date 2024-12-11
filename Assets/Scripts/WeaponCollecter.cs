using UnityEngine;
using UnityEngine.UI;

public class WeaponCollecter : MonoBehaviour
{
    [SerializeField] private Image _image;
    private WeaponData _weaponData;


    public void setWeaponCollecter(WeaponData weaponData)
    {
        _image.sprite = weaponData.sprite;
        _weaponData = weaponData;
    }

    public WeaponData getWeaponData()
    {
        return _weaponData;
    }



}

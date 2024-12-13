using UnityEngine;

public abstract class Gun : Weapon
{
    [SerializeField] protected SpriteRenderer gunSpriteRenderer;
    [SerializeField] protected Sprite gunSpriteNormal;
    [SerializeField] protected Sprite gunSpriteEmpty;
    [SerializeField] protected Transform _gunFirePoint;
    [SerializeField] protected int magazineSize = 30;
    [SerializeField] protected int magazine = 30;
    [SerializeField] protected float reloadTime = 3f;
    protected bool isReloding = false;
    protected bool isEmpty = false;


    protected void Reload()
    {
        if (Input.GetKey(KeyCode.R) && !isReloding)
        {
            Debug.Log("Reload Oluyor bekleyiniz");
            isReloding = true;
            Invoke("RefillMagazine", reloadTime);
        }
    }

    protected bool canIFire()
    {
        return Time.time - lastFireTime > _fireRate && !isReloding && !isEmpty;
    }

    protected void RefillMagazine()
    {
        magazine = magazineSize;
        isReloding = false;
        if (isEmpty) gunSpriteRenderer.sprite = gunSpriteNormal;
        isEmpty = false;
        Debug.Log("Reload oldu !!!");
    }
}

using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] protected SpriteRenderer gunSpriteRenderer;
    [SerializeField] protected Sprite gunSpriteNormal;
    [SerializeField] protected Sprite gunSpriteEmpty;
    [SerializeField] protected Transform _gunPoint;
    [SerializeField] protected GameObject _bulletTrail;
    [SerializeField] protected int magazineSize = 30;
    [SerializeField] protected int magazine = 30;
    [SerializeField] protected float reloadTime = 3f;
    [SerializeField] protected float bulletSpeed = 40f;
    protected bool isReloding = false;
    protected bool isEmpty = false;

    private void Update()
    {
        FollowMouse();
        Reload();
        flip();
    }

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

    public override void Fire()
    {
        if (!canIFire()) return;

        lastFireTime = Time.time;
        magazine--;
        if (magazine == 0)
        {
            isEmpty = true;
            gunSpriteRenderer.sprite = gunSpriteEmpty;
        } 
        var hit = Physics2D.Raycast(
            _gunPoint.position,
            transform.right,
            _weaponRange);

        var trail = Instantiate(
            _bulletTrail,
            _gunPoint.position,
            transform.rotation);

        var trailScript = trail.GetComponent<BulletTrail>();

        trailScript.SetBulletSpeed(bulletSpeed);

        if (hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);
            var target = hit.collider.GetComponent<Target>();
            target?.getDamage(_weaponDamage);

            Vector3 difference = (new Vector3(hit.point.x, hit.point.y, -1) - transform.position).normalized;
            Vector3 force = difference * _weaponKnockBack;
            target?.knockBack(force);
        }
        else
        {
            var endPosition = _gunPoint.position + transform.right * _weaponRange;
            trailScript.SetTargetPosition(endPosition);
        }
    }
}

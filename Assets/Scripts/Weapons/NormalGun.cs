using UnityEngine;

public class NormalGun : Gun
{
    [SerializeField] protected GameObject _bulletTrail;
    [SerializeField] protected float bulletSpeed = 40f;
    private void Update()
    {
        FollowMouse();
        Reload();
        flip();
    }

    public override void Fire(GameObject Player)
    {
        if (!canIFire()) return;

        lastFireTime = Time.time;
        magazine--;
        if (magazine == 0)
        {
            isEmpty = true;
            gunSpriteRenderer.sprite = gunSpriteEmpty;
        }

        int layerToIgnore = 1 << Player.layer; // Karakterin kendi Layer'ý
        int layerMask = ~layerToIgnore;

        var hit = Physics2D.Raycast(
            _gunFirePoint.position,
            transform.right,
            _weaponRange,
            layerMask);

        var trail = Instantiate(
            _bulletTrail,
            _gunFirePoint.position,
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
            var endPosition = _gunFirePoint.position + transform.right * _weaponRange;
            trailScript.SetTargetPosition(endPosition);
        }
    }

}

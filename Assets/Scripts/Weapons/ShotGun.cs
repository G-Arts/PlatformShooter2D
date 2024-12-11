using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] int shotBulletCount = 6;

    private int _damagePerBullet;

    private void Awake()
    {
        _damagePerBullet = (int)Mathf.Round(_weaponDamage / shotBulletCount);
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

        for (int i = 0; i < shotBulletCount; i++) // istediðimiz adet mermi
        {
            // Rastgele bir sapma açýsý hesapla (-10 derece ile 10 derece arasýnda)
            float randomOffset = Random.Range(-10f, 10f);
            Vector3 direction = Quaternion.Euler(0, 0, randomOffset) * transform.right;

            // Raycast iþlemi
            var hit = Physics2D.Raycast(_gunPoint.position, direction, _weaponRange, layerMask);

            // Mermi izi (trail) oluþtur
            var trail = Instantiate(_bulletTrail, _gunPoint.position, transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);

                // Hedefe hasar ver
                var target = hit.collider.GetComponent<Target>();
                target?.getDamage(_damagePerBullet);

                // Geri tepme (knockback) uygula
                Vector3 difference = (new Vector3(hit.point.x, hit.point.y, -1) - transform.position).normalized;
                Vector3 force = difference * _weaponKnockBack;
                target?.knockBack(force);
            }
            else
            {
                // Eðer hedef yoksa, mermi izini maksimum menzile kadar çiz
                var endPosition = _gunPoint.position + direction * _weaponRange;
                trailScript.SetTargetPosition(endPosition);
            }
        }
    }

}

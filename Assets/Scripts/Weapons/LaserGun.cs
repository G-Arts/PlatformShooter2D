using System.Collections;
using UnityEngine;

public class LaserGun : Gun
{
    [SerializeField] protected LineRenderer lineRenderer;
    [SerializeField] protected GameObject laser;
    private bool isFiring = false;

    private void Update()
    {
        FollowMouse();
        Reload();
        flip();
    }

    public override void Fire(GameObject Player)
    {
        if (isFiring) return;

        if (!canIFire()) return;

        lastFireTime = Time.time;
        magazine--;
        if (magazine == 0)
        {
            isEmpty = true;
        }

        StartCoroutine(Shoot(Player));
    }


    IEnumerator Shoot(GameObject Player)
    {
        int layerToIgnore = 1 << Player.layer; // Karakterin kendi Layer'ý
        int layerMask = ~layerToIgnore;

        RaycastHit2D hit = Physics2D.Raycast(_gunFirePoint.position, _gunFirePoint.right,_weaponRange, layerMask);

        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;

        if (hit)
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.getDamage(_weaponDamage);
            }

            lineRenderer.SetPosition(0,_gunFirePoint.position);
            lineRenderer.SetPosition(1,hit.point);


        }
        else
        {
            lineRenderer.SetPosition(0, _gunFirePoint.position);
            lineRenderer.SetPosition(1, _gunFirePoint.position + transform.right * _weaponRange);
        }

        laser.SetActive(true);
        isFiring = true;

        yield return new WaitForSeconds(1f);

        laser.SetActive(false);
        isFiring = false;


    }
}

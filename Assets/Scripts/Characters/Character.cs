using UnityEngine;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour
{
    protected float speed = 8f;
    protected float jumpingPower = 16f;
    protected float maxHealth = 100f;
    protected float health = 100f;

    [SerializeField] protected Weapon equippedWeapon;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;


    public abstract void Move(float horizontal);
    public abstract void Jump();
    public abstract void Attack();
    protected abstract bool IsGrounded();

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
    }

    protected abstract void Die();
}

using UnityEngine;

public class Target : MonoBehaviour
{
    private float maxHealth = 100f;
    private float health = 100f;
    private SpriteRenderer sprite;
    private Rigidbody2D rg2d;
    private float targetPoint = 0;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rg2d = gameObject.GetComponent<Rigidbody2D>();
    }

    public void getDamage(float damage)
    {
        health -= damage;
        targetPoint += damage / maxHealth;
        sprite.color = Color.Lerp(Color.white,Color.red,targetPoint);
        if (health <= 0)Destroy(gameObject);
        
    }

    public void knockBack(Vector2 force)
    {
        rg2d.AddForce(force, ForceMode2D.Impulse);
    }
}

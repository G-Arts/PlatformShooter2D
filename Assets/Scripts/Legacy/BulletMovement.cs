using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 20.0f;
    public float destroyTime = 3f;
    private bool crushed = false;
    void Update()
    {
        if(!crushed)
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        crushed = true;
    }

}

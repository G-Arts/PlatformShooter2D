using UnityEngine;

public class CharacterAlignment : MonoBehaviour
{
    public Transform characterBody; // Karakterin hizalanacak kýsmý (örneðin Sprite Renderer'ýn olduðu obje)
    public float rayLength = 1f; // Raycast'in uzunluðu
    public LayerMask groundLayer;
    public BoxCollider2D collider2d;
    public Rigidbody2D rg2d;

    private void Start()
    {
        rayLength = collider2d.bounds.size.y / 2 + 1;
    }

    void Update()
    {
        AlignToGround();
    }

    void AlignToGround()
    {
        // Karakterin altýna doðru bir ray gönder
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        if (hit.collider != null)
        {
            // Zeminin normalini al
            Vector2 groundNormal = hit.normal;

            // Normal vektörünün açýsýný hesapla
            float angle = Mathf.Atan2(groundNormal.y, groundNormal.x) * Mathf.Rad2Deg;

            // Karakteri bu açýya göre döndür
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
        else
        {
            // Eðer karakter havadaysa varsayýlan hizalama
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

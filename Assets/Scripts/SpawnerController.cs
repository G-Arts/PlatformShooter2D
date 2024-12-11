using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    float time;

    private void Start()
    {
        time = Time.time;
    }
    void Update()
    {
        if(Time.time - time > 3)
        {
            AddressableService.InstantiateObject<Rigidbody2D>("enemy", gameObject.transform, callback =>
            {
                callback.AddForce(Vector2.right, ForceMode2D.Impulse);
            });
            time = Time.time;


        }
        
    }
}

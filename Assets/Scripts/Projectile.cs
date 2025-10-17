using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;

    ObjectPooling objectPooling;
    private void Awake()
    {
        objectPooling = FindFirstObjectByType<ObjectPooling>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Tank" || collision.tag == "Dasher")
        {
            objectPooling.RemoveObject(gameObject);
        }
    }
}

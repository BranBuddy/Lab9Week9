using UnityEngine;

[ExecuteInEditMode]
public class DasherEnemy : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private float speed;
    [SerializeField] private int pointAmount;

    void Start()
    {
        Enemy regular = new Enemy.Builder().SetParameters(size = .7f, pointAmount = 10, speed = 7).Build();

        SizeAdjust();
    }

    void FixedUpdate()
    {
        Move();
    }

    void SizeAdjust()
    {
        this.transform.localScale = new Vector3(size, size, size);
    }

    void Move()
    {
        this.transform.position = new Vector3(this.transform.position.x + 1 * speed * Time.deltaTime, this.transform.position.y, 0);

        if (transform.position.x > 9.1f || transform.position.x < -9.1f)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            Destroy(this.gameObject);
            GameManager.Instance.Score(pointAmount);
        }
    }
}


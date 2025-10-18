using UnityEngine;
[ExecuteInEditMode]
public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private float speed;
    [SerializeField] private int pointAmount;

    void Start()
    {
        Enemy regular = new Enemy.Builder().SetParameters(size = 1f, pointAmount = 5, speed = 5).Build();

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

        if (transform.position.x > 11 || transform.position.x < -11)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bullet")
        {
            Destroy(this.gameObject);
            GameManager.Instance.Score(pointAmount);
        }
    }
}

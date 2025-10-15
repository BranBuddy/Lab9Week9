using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private float verticalInput;
    private float horizontalInput;
    [SerializeField] private float speed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDir = new Vector3(horizontalInput, verticalInput, 0);

        characterController.Move(moveDir * Time.deltaTime * speed);
    }
}

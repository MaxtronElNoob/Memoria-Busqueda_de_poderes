using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animador;
    private SpriteRenderer _spriteRenderer;
    private enum MovementState
    {
        Idle,
        WalkingFront,
        WalkingSide,
        WalkingBack
    }
    
    public float MovementSpeed = 10f, RotationSpeed = 5f;
    // private float _rotationY;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animador = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 movementVector)
    {
        Debug.Log("Moving: " + movementVector);
        if (movementVector.magnitude == 0f)
        {
            _animador.SetInteger("movement_state", (int)MovementState.Idle);
        }
        else
        {
            Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
            move = move * MovementSpeed * Time.deltaTime;
            if (movementVector.x == 0f && movementVector.y != 0f)
            {
                if (movementVector.y < 0f)
                {
                    Debug.Log("Walking Front");
                    _animador.SetInteger("movement_state", (int)MovementState.WalkingFront);
                }
                else
                {
                    Debug.Log("Walking Back");
                    _animador.SetInteger("movement_state", (int)MovementState.WalkingBack);
                }
            }
            else
            {
                Debug.Log("Walking Side");
                _animador.SetInteger("movement_state", (int)MovementState.WalkingSide);
                // _animador.SetBool("side", movementVector.x < 0f);
                _spriteRenderer.flipX = movementVector.x < 0f;
            }
            _characterController.Move(move);
        }
    }

    // public void Rotate(Vector2 rotationVector)
    // {
    //     _rotationY += rotationVector.x * RotationSpeed * Time.deltaTime;
    //     transform.localRotation = Quaternion.Euler(0, _rotationY, 0);
    // }
}

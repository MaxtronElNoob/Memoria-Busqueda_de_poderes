using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController CharacterController;
    [Tooltip("Añadir el objeto del juegador")]
    public GameObject interactClue;
    private InputAction _moveAction;
    public InputAction _interactAction;
    private InputAction _sprintAction;
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _interactAction = InputSystem.actions.FindAction("Interact");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
        // Cursor.visible = false; //Por mientras esto desactivado
    }

    void Update()
    {
        Vector2 movementVector = _moveAction.ReadValue<Vector2>();
        CharacterController.Move(movementVector, _sprintAction.IsPressed());
        
        if (_interactAction.WasPressedThisFrame() && interactClue.activeSelf)
        {
            
        }

    }
}

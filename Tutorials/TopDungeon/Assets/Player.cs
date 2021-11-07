using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static Vector3 face_right = new Vector3(1, 1, 1);
    private static Vector3 face_left = new Vector3(-1, 1, 1);

    private BoxCollider2D box_collider;
    private Vector3 move_delta;

    private PlayerInput player_input;
    private InputAction move_action;

    private void Awake()
    {
        player_input = GetComponent<PlayerInput>();
        move_action = player_input.actions["Move"];
        move_action.performed += Move;
        move_action.canceled += Move;
    }

    // Start is called before the first frame update
    private void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        bool left_down = (move_delta.x < 0);
        bool right_down = (move_delta.x > 0);

        if (left_down)
        {
            transform.localScale = face_left;
        }
        else if (right_down)
        {
            transform.localScale = face_right;
        }

        transform.Translate(move_delta * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input_vector = context.ReadValue<Vector2>();
        move_delta = new Vector3(input_vector.x, input_vector.y, 0);
    }
}

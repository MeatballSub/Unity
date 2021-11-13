using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static Vector3 face_right = new Vector3(1, 1, 1);
    private static Vector3 face_left = new Vector3(-1, 1, 1);
    private static Vector2 Vertical = new Vector2(0, 1);
    private static Vector2 Horizontal = new Vector2(1, 0);
    private static float move_granularity = 0.01F;

    private BoxCollider2D box_collider;
    private Vector3 move_delta;

    private PlayerInput player_input;
    private InputAction move_action;

    private RaycastHit2D hit_box;

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
        bool up_down = (move_delta.y < 0);
        bool down_down = (move_delta.y > 0);
        float delta_time = Time.deltaTime;

        if (left_down)
        {
            transform.localScale = face_left;
        }
        else if (right_down)
        {
            transform.localScale = face_right;
        }

        int horizontal_movement = (int)(move_delta.x * delta_time / move_granularity);
        int vertical_movement = (int)(move_delta.y * delta_time / move_granularity);

        hit_box = Physics2D.BoxCast(transform.position, box_collider.size, 0, Vertical, vertical_movement * move_granularity, LayerMask.GetMask("Actor", "Blocking"));
        if(hit_box.collider == null)
        {
            transform.Translate(0, vertical_movement * move_granularity, 0);
        }
        else if(up_down)
        {
            transform.position = new Vector3(transform.position.x, hit_box.collider.gameObject.transform.position.y + 0.16f);
        }
        else if(down_down)
        {
            transform.position = new Vector3(transform.position.x, hit_box.collider.gameObject.transform.position.y - 0.16f);
        }

        hit_box = Physics2D.BoxCast(transform.position, box_collider.size, 0, Horizontal, horizontal_movement * move_granularity, LayerMask.GetMask("Actor", "Blocking"));
        if (hit_box.collider == null)
        {
            transform.Translate(horizontal_movement * move_granularity, 0, 0);
        }
        else if (left_down)
        {
            transform.position = new Vector3(hit_box.collider.gameObject.transform.position.x + 0.16f, transform.position.y);
        }
        else if (right_down)
        {
            transform.position = new Vector3(hit_box.collider.gameObject.transform.position.x - 0.16f, transform.position.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input_vector = context.ReadValue<Vector2>();
        move_delta = new Vector3(input_vector.x, input_vector.y, 0);
    }
}

using System.Collections;
using UnityEngine;

public class BaseChar : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;

    //[SerializeField] public SpriteRenderer icon;
    //[SerializeField] public SpriteRenderer art;

    [SerializeField] int walkSpeed;
    
    int facing;
    bool walking;

    //timers
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        facing = 1;
        walking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Facing();
        Animate();
        InputHandling();
    }

    void InputHandling()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            walking = true;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.linearVelocity = Vector2.right * walkSpeed;
                facing = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)) 
            { 
                rb.linearVelocity = Vector2.left * walkSpeed;
                facing = -1;
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            walking = false;
            rb.linearVelocity = Vector2.zero;  // Stop movement when the key is released
        }
    }

    void Animate()
    {
        if (walking)
        {
            anim.SetInteger("walk", 1);
        }
        else
        {
            anim.SetInteger("walk", 0);
        }
    }

    void Facing()
    {
        if (facing == 1)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    IEnumerator Move(Vector2 direction, float speed, float duration)
    {
        rb.linearVelocity = Vector2.zero;
        float timer = 0f;
        while (timer < duration)
        {
            rb.linearVelocity = direction.normalized * speed; // Set movement speed
            timer += Time.deltaTime; // Track elapsed time
            yield return null; // Wait for next frame
        }
        rb.linearVelocity = Vector2.zero; // Stop movement after duration
    }
}

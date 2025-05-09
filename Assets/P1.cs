using UnityEngine;

public class P1 : MonoBehaviour
{
    [SerializeField] SpriteRenderer P1Sprite;
    [SerializeField] Animator anim;

    [SerializeField] int walkSpeed;
    int facing;
    Vector3 moveDirection;
    bool walking = false;
    bool stopping = false;
    bool stop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        facing = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Facing();    
        Movement();
        Animate();
    }

    void Animate()
    {
        if (walking || stopping)
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
            P1Sprite.flipX = false;
        }
        else
        {
            P1Sprite.flipX = true;
        }
    }

    void Movement()
    {
        walking = false;
        if (Input.GetKey(KeyCode.LeftArrow)) { 
            moveDirection = Vector3.left;
            stopping = false;
            stop = false;
            walking = true;
            facing = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            moveDirection = Vector3.right;
            stopping = false;
            stop = false;
            walking = true;
            facing = 1;
        }
        else if (!stopping && anim.GetCurrentAnimatorStateInfo(0).IsName("walk") && !stop)
        {
            // Allow stopping regardless of animation loops
            stopping = true;
        }
        else if (stopping && anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f >= 0.99f)
        {
            // Stop movement once the current animation cycle completes
            stopping = false;
            stop = true;
            moveDirection = Vector3.zero;
        }

        if (walking || stopping)
        {
            transform.position += moveDirection * walkSpeed * Time.deltaTime;
        }
    }
}

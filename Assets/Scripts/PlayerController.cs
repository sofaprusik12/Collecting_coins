using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;

    public GameObject spawner;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jump", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeadZone") {
            transform.position = new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z);
        }

        if (collision.gameObject.tag == "Coin")
        {
            GameObject particle = Instantiate(
               Resources.Load("prefabs/Particles/CollectionEffect"),
               new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z),
               Quaternion.identity
            ) as GameObject;

            Destroy(particle.gameObject, particle.gameObject.GetComponent<ParticleSystem>().duration);

            Destroy(collision.gameObject);
            // set score (based on coin type)
            // play sound
            // add particle

        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}

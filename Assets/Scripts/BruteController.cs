using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class BruteController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float turnSpeed = 0.5f;
    [SerializeField]
    private float runSpeed = 1f;

    private bool runBool = false;

    private FrisbeeThrower ft;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ft = GetComponent<FrisbeeThrower>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Run", true);
            runBool = true;
        }
        else
        {
            animator.SetBool("Run", false);
            runBool = false;
        }

        if (runBool == true)
        {
            runSpeed = 2;
        }
        else
        {
            runSpeed = 1;
        }

        //pick up
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Pick Up");
            ft.PickUp();
            animator.SetBool("Holding Item", true);
        }

        //throw
        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("Throw");
            ft.ThrowFrisbee();
            animator.SetBool("Holding Item", false);
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float speed = moveSpeed * Input.GetAxis("Vertical");
       
        controller.SimpleMove(forward * speed * runSpeed);

        animator.SetFloat("Speed", speed);
        
    }
}

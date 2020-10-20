using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
public float speed;
private Rigidbody2D heroRigidbody;
private Vector3 change;
private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
         animator = GetComponent<Animator>(); 
         heroRigidbody = GetComponent<Rigidbody2D>();
      

    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
       
           
    }

    void UpdateAnimationAndMove()
    { if(change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
            }else{animator.SetBool("Moving",false);}
    }

    void MoveCharacter()
    {
        heroRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }
}

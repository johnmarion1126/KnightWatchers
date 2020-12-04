using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheLeader : MonoBehaviour {
    public GameObject leader; // the game object to follow - assign in inspector
    public int steps; // number of steps to stay behind - assign in inspector

    private Queue<Vector3> record = new Queue<Vector3>();
    private Vector3 lastRecord;
    private Vector3 change;
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }
    void Update(){
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
    }

    void FixedUpdate() {
        // record position of leader
        record.Enqueue(leader.transform.position);

        // remove last position from the record and use it for our own
        if (record.Count > steps) {
            this.transform.position = record.Dequeue();
        }
    }

    void UpdateAnimationAndMove()
    { if(change != Vector3.zero)
        {
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
            }else{animator.SetBool("Moving",false);}
    }
}

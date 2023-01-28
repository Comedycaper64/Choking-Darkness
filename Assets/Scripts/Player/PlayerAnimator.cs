using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private bool moving;
    private Animator animator;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(moveCheck());
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0); 
        }
        animator.SetFloat("Horizontal", player.horizontalMovement);
        animator.SetFloat("Vertical", player.verticalMovement);
        if (player.facingDirection.x == 1)
        {
            animator.SetBool("facingUp", false);
            animator.SetBool("facingLeft", false);
            animator.SetBool("facingDown", false);
            animator.SetBool("facingRight", true);
        }
        else if (player.facingDirection.x == -1)
        {
            animator.SetBool("facingUp", false);
            animator.SetBool("facingLeft", true);
            animator.SetBool("facingDown", false);
            animator.SetBool("facingRight", false);
        }
        else if (player.facingDirection.y == -1)
        {
            animator.SetBool("facingUp", false);
            animator.SetBool("facingLeft", false);
            animator.SetBool("facingDown", true);
            animator.SetBool("facingRight", false);
        }
        else if (player.facingDirection.y == 1)
        {
            animator.SetBool("facingUp", true);
            animator.SetBool("facingLeft", false);
            animator.SetBool("facingDown", false);
            animator.SetBool("facingRight", false);
        }

    }

    private IEnumerator moveCheck()
    {
        float pos1 = player.gameObject.transform.position.sqrMagnitude;
        yield return new WaitForSeconds(0.05f);
        float pos2 = player.gameObject.transform.position.sqrMagnitude;
        if (Mathf.Abs(pos1 - pos2) > 0.05f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        StartCoroutine(moveCheck());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    private bool moving = false;
    private Vector2 currentMovement;
    // Start is called before the first frame update
    void Start()
    {
        currentMovement = new Vector2(0, 0);
        animator = GetComponent<Animator>();
        StartCoroutine(moveCheck());
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 1f);
        }

        if (currentMovement.x > 0.01f)
        {
            animator.SetFloat("Horizontal", 1f);
        }
        else if (currentMovement.x < -0.01f)
        {
            animator.SetFloat("Horizontal", -1f);
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
        }

        if (currentMovement.y > 0.01f)
        {
            animator.SetFloat("Vertical", 1f);
        }
        else if (currentMovement.y < -0.01f)
        {
            animator.SetFloat("Vertical", -1f);
        }
        else
        {
            animator.SetFloat("Vertical", 0);
        }
    }

    private IEnumerator moveCheck()
    {
        Vector2 pos1 = gameObject.transform.position;
        float pos1Mag = gameObject.transform.position.sqrMagnitude;
        yield return new WaitForSeconds(0.1f);
        Vector2 pos2 = gameObject.transform.position;
        float pos2Mag = gameObject.transform.position.sqrMagnitude;
        currentMovement = pos2 - pos1;
        if (pos1Mag != pos2Mag)
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

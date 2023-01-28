using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTERingTarget : MonoBehaviour
{
    public bool ringsTouching = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("QTE"))
        {
            ringsTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("QTE"))
        {
            ringsTouching = false;
        }
    }
}

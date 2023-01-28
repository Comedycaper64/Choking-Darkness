using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteGame : MonoBehaviour
{
    [SerializeField] private Text gameEndText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameEndText.text = "Game beat!";
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}

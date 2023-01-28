using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndCheck : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            levelManager.LoadNextLevel();
        }
    }
}

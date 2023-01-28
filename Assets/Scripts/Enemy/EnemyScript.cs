using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    private AIPath aiPathScript;
    private AIDestinationSetter destinationSetter;
    private PlayerController player;
    private DetectionSystem detection;

    // Start is called before the first frame update
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        destinationSetter.target = player.gameObject.transform;
        aiPathScript = GetComponent<AIPath>();
        detection = GameObject.FindGameObjectWithTag("Detection").GetComponent<DetectionSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        aiPathScript.maxSpeed = (5 * (detection.GetDetection() / 100));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.Die();
        }
    }
}

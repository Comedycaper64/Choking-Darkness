using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionSystem : MonoBehaviour
{
    public bool isDetected = false;
   
    private float detection;

    private PlayerController player;
    private BreathSystem breathSystem;
    [SerializeField] private Slider detectionSlider;

    // Start is called before the first frame update
    void Start()
    {
        detection = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        breathSystem = GameObject.FindGameObjectWithTag("Breath").GetComponent<BreathSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.moving)
        {
            detection += (Time.deltaTime * (breathSystem.GetBreath() * breathSystem.GetBreath()));
            if (detection > 99)
            {
                detection = 100;
                isDetected = true;
            }
            detectionSlider.value = detection;
        }
    }

    public void AddToDetection(float increaseAmount)
    {
        detection += increaseAmount;
        if (detection > 99)
        {
            detection = 100;
            isDetected = true;
        }
        detectionSlider.value = detection;
    }

    public float GetDetection()
    {
        return detection;
    }

    public void DecreaseDetection(float decreaseAmount)
    {
        detection -= decreaseAmount;
        if (detection < 1)
        {
            detection = 0;
        }
        detectionSlider.value = detection;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESystem : MonoBehaviour
{
    private bool QTERunning = false;
    private bool enlargeRing = false;

    private float ringGrowthRate;
    private float detectionDecrease;
    private float breathDecrease;
    private int timesQTEInitiated;

    private DetectionSystem detectionScript;
    private PlayerController player;
    private GameObject QTERing;
    private GameObject QTERingTarget;
    private QTERingTarget ringTargetScript;
    private BreathSystem breathSystem;
    private SFXPlayer sfx;

    // Start is called before the first frame update
    void Start()
    {
        timesQTEInitiated = 0;
        detectionDecrease = 5f;
        breathDecrease = 1f;
        QTERing = GameObject.FindGameObjectWithTag("QTE");
        QTERingTarget = GameObject.FindGameObjectWithTag("QTETarget");
        QTERing.SetActive(false);
        QTERingTarget.SetActive(false);
        ringTargetScript = QTERingTarget.GetComponent<QTERingTarget>();
        detectionScript = GameObject.FindGameObjectWithTag("Detection").GetComponent<DetectionSystem>();
        breathSystem = GameObject.FindGameObjectWithTag("Breath").GetComponent<BreathSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!QTERunning && !player.isDead && Input.GetKeyDown(KeyCode.Space))
        {
            InitiateQTE(); 
        }
        else if (enlargeRing)
        {
            QTERing.transform.localScale += new Vector3(ringGrowthRate, ringGrowthRate, ringGrowthRate) * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ringTargetScript.ringsTouching)
                {
                    EndQTE(true);
                }
                else
                {
                    EndQTE(false);
                }
            }
            else if (QTERing.transform.localScale.x > 1000)
            {
                EndQTE(false);
            }
        }
    }

    private void InitiateQTE()
    {
        sfx.PlayLongInhale();
        QTERunning = true;
        if (timesQTEInitiated < 4)
        {
            timesQTEInitiated++;
        }
        else
        {
            if (detectionDecrease > 0.1f)
            {
                detectionDecrease -= 2f;
            }
            if (breathDecrease > 0.3f)
            {
                breathDecrease -= 0.1f;
            }
        }
        float targetSize = GenerateTargetSize();
        QTERingTarget.transform.localScale = new Vector3(targetSize, targetSize, targetSize);
        QTERing.transform.localScale = new Vector3(1, 1, 1);
        ringGrowthRate = GetRingGrowthRate();
        QTERing.SetActive(true);
        QTERingTarget.SetActive(true);
        enlargeRing = true;
        
    }

    private void EndQTE(bool qtePassed)
    {
        enlargeRing = false;
        QTERunning = false;
        QTERing.SetActive(false);
        QTERingTarget.SetActive(false);
        if (qtePassed)
        {
            sfx.PlayLongExhale();
            detectionScript.DecreaseDetection(detectionDecrease);
            breathSystem.DecreaseBreath(breathDecrease);
        }
        else
        {
            timesQTEInitiated--;
            sfx.PlayPainedInhale();
            detectionScript.AddToDetection(10f);
        }
    }

    private float GetRingGrowthRate()
    {
        return (300f * (1.2f * timesQTEInitiated));
    }

    private float GenerateTargetSize()
    {
        return UnityEngine.Random.Range(500f, 850f);
    }
}

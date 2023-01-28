using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathSystem : MonoBehaviour
{
    private float breath;
    [SerializeField] private Slider breathSlider;

    // Start is called before the first frame update
    void Start()
    {
        breath = 1;
    }

    public void AddToBreath(float increaseAmount)
    {
        breath += increaseAmount;
        if (breath > 1.9)
        {
            breath = 2;
        }
        breathSlider.value = breath;
    }

    public void DecreaseBreath(float decreaseAmount)
    {
        breath -= decreaseAmount;
        if (breath < 1)
        {
            breath = 1;
        }
        breathSlider.value = breath;
    }

    public float GetBreath()
    {
        return breath;
    }
}

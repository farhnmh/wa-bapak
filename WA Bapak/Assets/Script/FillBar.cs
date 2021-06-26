using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillBar : MonoBehaviour
{

    // Unity UI References
    public Slider slider;
    public TextMeshProUGUI displayText;
    public bool progressSidik;

    // Create a property to handle the slider's value
    private float currentValue = 0f;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            slider.value = currentValue;
            displayText.text = (slider.value * 100).ToString("0") + "%";
        }
    }

    // Use this for initialization
    void Start()
    {
        CurrentValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (progressSidik)
        {
            if (CurrentValue >= 1f)
            {
                MissionManager.instance.HasilPenyidikan();
                print("Kelar");
            }
            CurrentValue += 0.00043f;
            
        }
        else
        {
            CurrentValue = 0f;
        }
        
    }
}
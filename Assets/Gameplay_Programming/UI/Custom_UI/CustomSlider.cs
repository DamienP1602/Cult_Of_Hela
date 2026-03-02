using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float sliderSpeed;

    [Header("References")]
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text text;
    [SerializeField] Image fillImage;

    float currentValue = 1.0f;
    float goalValue;
    bool needToChange;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if (needToChange)
        {
            bool _greater = goalValue > currentValue;
            float _deltatime = Time.deltaTime;

            currentValue += _greater ? _deltatime : -_deltatime;
            slider.value = currentValue;

            if (_greater ? currentValue >= goalValue : currentValue <= goalValue)
                needToChange = false;
        }
    }

    public void SetValue(int _current,int _max)
    {
        goalValue = (float)_current / (float)_max;
        needToChange = true;

        text.text = _current.ToString() + "/" + _max.ToString();
    }
}

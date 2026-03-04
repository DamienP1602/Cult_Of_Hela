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
        if (GetComponentInChildren<Slider>(true) is Slider _sliderFounded)
        {
            slider = _sliderFounded;
        }

        if (GetComponentInChildren<TMP_Text>(true) is TMP_Text _textFounded)
        {
            text = _textFounded;
        }

        currentValue = slider.value;
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

    public void SetGoalValue(int _current, int _max)
    {
        goalValue = (float)_current / (float)_max;
        needToChange = true;

        if (text)
            text.text = _current.ToString() + "/" + _max.ToString();
    }

    public void SetValue(int _current, int _max)
    {
        goalValue = (float)_current / (float)_max;
        slider.value = goalValue;

        if (text)
            text.text = _current.ToString() + "/" + _max.ToString();
    }
}

using TMPro;
using UnityEngine;

public class TutoWorldWidget : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] CustomButton button;

    public CustomButton Button => button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }

}

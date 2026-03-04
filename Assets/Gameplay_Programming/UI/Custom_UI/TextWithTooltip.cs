using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextWithTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Parameters")]
    [SerializeField] string titleText;
    [SerializeField] string tooltipText;
    [SerializeField] GameObject tooltipWindowRef;

    GameObject tooltipWindow;


    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipWindow = Instantiate(tooltipWindowRef);
        TMP_Text _tooltipText = tooltipWindow.GetComponentInChildren<TMP_Text>();
        _tooltipText.text = titleText + "\n" + tooltipText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(tooltipWindow.gameObject);
    }

    private void OnDisable()
    {
        if (tooltipWindow)
        {
            Destroy(tooltipWindow.gameObject);
        }
    }

}

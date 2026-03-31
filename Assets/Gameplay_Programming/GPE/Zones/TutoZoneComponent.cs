using System.Collections.Generic;
using UnityEngine;

public class TutoZoneComponent : MonoBehaviour
{
    [SerializeField] TutoWorldWidget widgetToOpen;
    [SerializeField] List<string> texts;
    bool hasBeenSeen = false;

    TutoWorldWidget currentWidget;
    int currentIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider _other)
    {
        if (!_other.GetComponent<PlayerEntity>()) return;

        if (!hasBeenSeen)
        {
            currentWidget = Instantiate(widgetToOpen);
            currentWidget.Button.AddLeftClickAction(ComputeNextAction);
            ComputeNextAction();
            GameManager.Instance.Player.ClickComponent.SetCanClick(false);
            hasBeenSeen = true;
        }
    }

    void ComputeNextAction()
    {
        currentIndex++;

        if (currentIndex >= texts.Count)
        {
            CloseWidget();
            return;
        }
        else if (currentIndex + 1 >= texts.Count)
            currentWidget.Button.ButtonText.SetText("Close");
        else
            currentWidget.Button.ButtonText.SetText("Next");

        currentWidget.SetText(texts[currentIndex]);
    }


    void CloseWidget()
    {
        GameManager.Instance.Player.ClickComponent.SetCanClick(true);
        Destroy(currentWidget.gameObject);
    }
}

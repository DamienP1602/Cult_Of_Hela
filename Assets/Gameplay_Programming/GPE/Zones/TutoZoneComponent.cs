using UnityEngine;

public class TutoZoneComponent : MonoBehaviour
{
    [SerializeField] TutoWorldWidget widgetToOpen;
    [SerializeField] string text;
    bool hasBeenSeen = false;

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
            TutoWorldWidget _widget = Instantiate(widgetToOpen);
            _widget.SetText(text);
            GameManager.Instance.Player.ClickComponent.SetCanClick(false);
            hasBeenSeen = true;
        }
    }
}

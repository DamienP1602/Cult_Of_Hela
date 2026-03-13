using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEvent
{
    public float requiredTime;
    public Action hoverEvent;
    public bool hasBeenActivated;

    public HoverEvent(float _minimumTime, Action _action)
    {
        requiredTime = _minimumTime;
        hoverEvent = _action;
        hasBeenActivated = false;
    }
}

[RequireComponent(typeof(Image))]
public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    event Action leftClickEvent;
    event Action downEvent;
    event Action rightClickEvent;
    event Action onEnterEvent;
    event Action onExitEvent;
    List<HoverEvent> allHoverEvents = new List<HoverEvent>();

    bool isHovered;
    float hoveredTime;

    Vector3 scaleToLerp = Vector3.one;

    [SerializeField] TMP_Text buttonText;
    [SerializeField] bool interactable = true;
    [SerializeField] Image graphic;
    [SerializeField] Color baseColor = Color.white;
    [SerializeField] Color hoverColor = new Color(0.8f, 0.8f, 0.8f);
    [SerializeField] Color pressedColor = new Color(0.5f, 0.5f, 0.5f);
    [SerializeField] Color disabledColor = new Color(0.3f, 0.3f, 0.3f);
    [SerializeField] float hoverScale = 1.1f;

    public Image Graphic => graphic;
    public TMP_Text ButtonText => buttonText;
    public bool IsInteractable => interactable;

    private void Awake()
    {
        graphic = GetComponent<Image>();
        buttonText = GetComponentInChildren<TMP_Text>(true);
    }

    private void OnEnable()
    {
        graphic.color = interactable ? baseColor : disabledColor;
    }

    private void OnDisable()
    {
        scaleToLerp = Vector3.one;
        transform.localScale = scaleToLerp;
    }

    public void SetInteractionColor(Color _color)
    {
        baseColor = _color;
        hoverColor = _color;
        pressedColor = _color;

        graphic.color = _color;
    }

    protected virtual void Update()
    {
        UpdateScale();

        if (!interactable) return;

        if (isHovered)
        {
            hoveredTime += Time.deltaTime;

            InvokeHoverEvent();
        }
    }

    void UpdateScale()
    {
        if (transform.localScale != scaleToLerp)
        {
            Vector3 _newScale = Vector3.Lerp(transform.localScale, scaleToLerp,Time.deltaTime * 10.0f);
            transform.localScale = _newScale;
        }
    }

    public void AddLeftClickAction(Action _action) => leftClickEvent += _action;
    public void AddOnDownAction(Action _action) => downEvent += _action;
    public void AddRightClickAction(Action _action) => rightClickEvent += _action;
    public void AddOnEnterAction(Action _action) => onEnterEvent += _action;
    public void AddOnExitAction(Action _action) => onExitEvent += _action;
    public void AddHoverAction(Action _action, float _minimumTime) => allHoverEvents.Add(new HoverEvent(_minimumTime, _action));

    void InvokeLeftClick() => leftClickEvent?.Invoke();
    void InvokeOnDown() => downEvent?.Invoke();
    void InvokeRightClick() => rightClickEvent?.Invoke();
    void InvokeOnEnter() => onEnterEvent?.Invoke();
    void InvokeOnExit() => onExitEvent?.Invoke();

    void InvokeHoverEvent()
    {
        int _size = allHoverEvents.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            HoverEvent _data = allHoverEvents[_i];

            if (_data.requiredTime <= hoveredTime)
            {
                if (_data.hasBeenActivated) return;

                _data.hoverEvent?.Invoke();
                _data.hasBeenActivated = true;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            InvokeLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            InvokeRightClick();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!interactable) return;

        graphic.color = hoverColor;
        isHovered = true;

        InvokeOnEnter();

        int _size = allHoverEvents.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            HoverEvent _data = allHoverEvents[_i];
            _data.hasBeenActivated = false;
        }

        scaleToLerp = Vector3.one * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable) return;

        graphic.color = baseColor;
        isHovered = false;
        hoveredTime = 0.0f;

        InvokeOnExit();
        
        scaleToLerp = Vector3.one;
    }


    public void SetInteractable(bool _value)
    {
        interactable = _value;
        graphic.color = _value ? baseColor : disabledColor;
        scaleToLerp = Vector3.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable) return;

        graphic.color = pressedColor;
        InvokeOnDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable) return;

        if (isHovered)
            graphic.color = hoverColor;
        else
            graphic.color = baseColor;
    }
}

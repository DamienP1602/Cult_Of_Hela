using UnityEngine;
using UnityEngine.UI;

public class SpellButtonWidget : MonoBehaviour
{
    [SerializeField] CustomButton button;
    [SerializeField] Image spellIcon;
    [SerializeField] int spellIndex;

    public CustomButton Button => button;
    public Image SpellIcon => spellIcon;
    public int Index => spellIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIndex(int _index) => spellIndex = _index;

    public void Init(Sprite _sprite, Color _color)
    {
        spellIcon.sprite = _sprite;
        spellIcon.color = _color;
    }

    public void Clear()
    {
        spellIcon.sprite = null;
        spellIcon.color = Color.clear;
        spellIndex = 0;
    }
}

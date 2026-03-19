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

    public void Init(Sprite _sprite,Color _color, int _index)
    {
        spellIcon.sprite = _sprite;
        spellIcon.color = _color;
        spellIndex = _index;
    }
}

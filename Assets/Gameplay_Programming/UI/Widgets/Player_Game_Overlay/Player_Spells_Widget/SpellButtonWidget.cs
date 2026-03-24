using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellButtonWidget : MonoBehaviour
{
    [SerializeField] CustomButton button;
    [SerializeField] Image spellIcon;
    [SerializeField] Image cooldownBackground;
    [SerializeField] TMP_Text cooldownText;
    [SerializeField] int spellIndex;
    [SerializeField] bool isInCooldown;
    float cooldown;

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
        CooldownUpdate();
    }

    void CooldownUpdate()
    {
        if (isInCooldown)
        {
            cooldown -= Time.deltaTime;

            if (cooldown < 0.0f)
            {
                isInCooldown = false;
                cooldownText.gameObject.SetActive(false);
                cooldownBackground.gameObject.SetActive(false); 
            }

            cooldownText.text = ((int)cooldown).ToString();
        }
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

    public void SetIsInCooldown(float _cooldown)
    {
        cooldown = _cooldown;
        cooldownBackground.gameObject.SetActive(true);
        cooldownText.gameObject.SetActive(true);
        isInCooldown = true;
    }
}

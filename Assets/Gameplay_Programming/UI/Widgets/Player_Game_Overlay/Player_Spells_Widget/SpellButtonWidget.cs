using UnityEngine;
using UnityEngine.UI;

public class SpellButtonWidget : MonoBehaviour
{
    [SerializeField] CustomButton button;
    [SerializeField] Image spellIcon;

    public CustomButton Button => button;
    public Image SpellIcon => spellIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

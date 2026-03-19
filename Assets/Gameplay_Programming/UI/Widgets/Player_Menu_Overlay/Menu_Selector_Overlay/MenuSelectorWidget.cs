using UnityEngine;

public class MenuSelectorWidget : MonoBehaviour
{
    [SerializeField] CustomButton abilitiesButton;
    [SerializeField] CustomButton inventoryButton;

    public CustomButton AbilitiesButton => abilitiesButton;
    public CustomButton InventoryButton => inventoryButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

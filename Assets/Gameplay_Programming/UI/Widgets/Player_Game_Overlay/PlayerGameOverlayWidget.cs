using UnityEngine;

public class PlayerGameOverlayWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] PlayerRessourcesWidget ressourcesWidget;
    [SerializeField] PlayerSpellsWidget spellsWidget;
    [SerializeField] PlayerAccessWidget accessWidget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitGameOverlay(PlayerEntity _player)
    {
        _player.StatsComponent.health.onValueChange += ressourcesWidget.ChangeHealthBar;
        _player.StatsComponent.ressource.onValueChange += ressourcesWidget.ChangeRessourceBar;
        _player.LevelComponent.OnGainExperience += ressourcesWidget.ChangeExperienceBar;

        spellsWidget.InitButtonSpells(_player);
    }
}

using UnityEngine;

public class PlayerGameOverlayWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] PlayerRessourcesWidget ressourcesWidget;
    [SerializeField] PlayerSpellsWidget spellsWidget;
    [SerializeField] PlayerAccessWidget accessWidget;
    [SerializeField] SpellDescriptionWidget spellDescriptionWidget;

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

        _player.SpellBookComponent.OnLearnSpell += () => spellsWidget.UpdateSpellsOnWidget(_player);

        spellsWidget.InitButtonSpells(_player, spellDescriptionWidget);
        ressourcesWidget.InitValues(_player.StatsComponent, _player.LevelComponent);
        accessWidget.Init(_player);
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformationWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] TMP_Text itemMainStat;
    [SerializeField] Transform secondStatsTransformParent;
    List<TMP_Text> itemSecondStats = new List<TMP_Text>();
    [SerializeField] TMP_Text itemSpecialEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(ItemInventoryData _data)
    {
        itemIcon.sprite = _data.data.itemIcon;
        itemName.text = _data.data.itemName;
        itemDescription.text = GenerateDescription(_data.data);
        itemMainStat.text = GenerateMainStat(_data.data);

        GenerateSecondStats(_data.data);
    }

    string GenerateDescription(Item _data)
    {
        string _s = "";

        _s += _data.rarity.ToString().Split('_')[1] + " " ;
        _s += _data.itemType.ToString().Split('_')[1];

        return _s;
    }

    string GenerateMainStat(Item _data)
    {
        string _s = "";

        switch (_data.equipmentType)
        {
            case EquipmentType.Damage_Equipment:
                _s += _data.damages.x.ToString() + "-" + _data.damages.y.ToString() + " Damages";
                break;
            case EquipmentType.Defense_Equipment:
                _s += _data.armor.ToString() + " Armor";
                break;
        }

        return _s;
    }

    void GenerateSecondStats(Item _data)
    {
        foreach(TMP_Text _text in itemSecondStats)
        {
            Destroy(_text.gameObject);
        }
        itemSecondStats.Clear();


        if (_data.strength > 0)
        {
            TMP_Text _newText = Instantiate(itemSpecialEffect, secondStatsTransformParent);
            _newText.text = "+ " + _data.strength.ToString() + " strength";
            itemSecondStats.Add(_newText);
        }
        if (_data.intelligence > 0)
        {
            TMP_Text _newText = Instantiate(itemSpecialEffect, secondStatsTransformParent);
            _newText.text = "+ " + _data.intelligence.ToString() + " intelligence";
            itemSecondStats.Add(_newText);
        }
        if (_data.dexterity > 0)
        {
            TMP_Text _newText = Instantiate(itemSpecialEffect, secondStatsTransformParent);
            _newText.text = "+ " + _data.dexterity.ToString() + " dexterity";
            itemSecondStats.Add(_newText);
        }
        if (_data.vitality > 0)
        {
            TMP_Text _newText = Instantiate(itemSpecialEffect, secondStatsTransformParent);
            _newText.text = "+ " + _data.vitality.ToString() + " vitality";
            itemSecondStats.Add(_newText);
        }
        if (_data.spirit > 0)
        {
            TMP_Text _newText = Instantiate(itemSpecialEffect, secondStatsTransformParent);
            _newText.text = "+ " + _data.spirit.ToString() + " spirit";
            itemSecondStats.Add(_newText);
        }

    }
}

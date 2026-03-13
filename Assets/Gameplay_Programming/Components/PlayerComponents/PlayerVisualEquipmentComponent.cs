using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualEquipmentComponent : MonoBehaviour
{
    [Serializable]
    public struct VisualEquipmentData
    {
        public Transform transform;
        public EquipmentSlotType slotType;

        public VisualEquipmentData(Transform _transform, EquipmentSlotType _slotType)
        {
            transform = _transform;
            slotType = _slotType;
        }
    }

    [SerializeField] List<VisualEquipmentData> equipmentSockets = new List<VisualEquipmentData>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMeshOnSlot(Item _item, EquipmentSlotType _slotType)
    {
        foreach (VisualEquipmentData _data in equipmentSockets)
        {
            if (_data.slotType == _slotType)
            {
                VisualEquipmentComponent _obj = Instantiate(GameManager.Instance.EmptyItemMesh, _data.transform);
                _obj.SetValues(_item);
                return;
            }
        }

    }

    public void RemoveMeshOnSlot(EquipmentSlotType _slotType)
    {
        foreach (VisualEquipmentData _data in equipmentSockets)
        {
            if (_data.slotType == _slotType)
            {
                VisualEquipmentComponent _equipment = _data.transform.GetComponentInChildren<VisualEquipmentComponent>();
                Destroy(_equipment.gameObject);
                return;
            }
        }
    }
}

using UnityEngine;

[CreateAssetMenu()]
public class Inventory_Equipment : Inventory_Item
{
    public enum EquipmentType { SWORD, SHIELD, DEFAULT }
    [SerializeField] private int damage = 0;
    [SerializeField] private int armor = 0;
    [SerializeField] private EquipmentType type = EquipmentType.DEFAULT;

    public int GetDamage()
    {
        return damage;
    }

    public int GetArmor()
    {
        return armor;
    }

    public EquipmentType GetEquipmentType()
    {
        return type;
    }
}

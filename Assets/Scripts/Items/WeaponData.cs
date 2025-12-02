using UnityEngine;

// Meant To Hold Constants That Don't Change
[CreateAssetMenu(menuName = "Items/Weapons")]
public class WeaponData : ScriptableObject
{
    public bool isBaseItem = true;

    [Space]
    public float damage;
    public float knockback;
    public float speedChange;
}

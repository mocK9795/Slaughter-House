using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyFighter : MonoBehaviour {
    public WeaponData baseDamage;
    public new Transform camera;
    public static EnemyScript lastHitEnemy;


    public void OnFight(InputAction.CallbackContext context) {
        if (!context.canceled) return;
        if (!PlayerLogic.raycast.collider) return;

        lastHitEnemy = PlayerLogic.raycast.collider.GetComponentInParent<EnemyScript>();

        bool activeWeapon = (ItemHolder.activeItem && ItemHolder.activeItem is Weapon);
		var damageObject = activeWeapon ? (ItemHolder.activeItem as Weapon).data : baseDamage;
        if (lastHitEnemy) lastHitEnemy.Attack(damageObject.damage, camera.forward, damageObject.knockback);
    }
}

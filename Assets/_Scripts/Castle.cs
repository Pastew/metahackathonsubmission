using UnityEngine;

namespace _Scripts
{
    public class Castle : Unit
    {
        protected override void Die()
        {
            Debug.Log("Game over");
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                Destroy(enemy.gameObject);
            }

            SetHp(_maxHp);
        }
    }
}

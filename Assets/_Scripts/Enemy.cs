
using UnityEngine;

namespace _Scripts
{
    public class Enemy : Unit
    {
        protected override void Awake()
        {
            base.Awake();
            MoveTarget = FindObjectOfType<Castle>().transform;
            float speedBooster = Random.Range(0.5f, 1f);
            _moveSpeed *= speedBooster;
            _animator.speed = speedBooster;

            float scaleBoost = 1 - speedBooster;
            transform.localScale = transform.localScale + scaleBoost * transform.localScale;
            //SetMaxHp((int)(_maxHp * scaleBoost));
            _navMeshAgent.isStopped = false;
            _navMeshAgent.radius *= Random.Range(1, 1.5f);
        }
    }
}

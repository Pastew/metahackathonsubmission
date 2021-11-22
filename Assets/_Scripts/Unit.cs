using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] protected float _moveSpeed = 0.5f;
        [SerializeField] protected int _hp = 100;

        [SerializeField] private int _damage = 20;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Material _frozenMaterial;

        protected Transform MoveTarget;

        private Unit _opponent;
        protected Animator _animator;
        protected int _maxHp;
        private static readonly int DieTrigger = Animator.StringToHash("Die");
        private static readonly int AttackAnimBool = Animator.StringToHash("Attack");
        protected NavMeshAgent _navMeshAgent;

        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            _maxHp = _hp;
            SetHp(_maxHp);
            if (_navMeshAgent)
                _navMeshAgent.isStopped = true;
        }

        protected virtual void Start()
        {
            if (_navMeshAgent)
            {
                _navMeshAgent.destination = MoveTarget.position;
                _navMeshAgent.speed = _moveSpeed;
            }
        }

        private void Update()
        {
            // if (!_navMeshAgent.isStopped)
            // _navMeshAgent.
        }

        public void AttackAnimEvent()
        {
            if(_opponent != null)
                _opponent.TakeDamage(_damage, this);
        }

        private void TakeDamage(int damage, Unit damageDealer = null)
        {
            SetHp(_hp - damage);

            if (_hp <= 0)
            {
                Die();

                if (damageDealer != null)
                    damageDealer.OnOpponentDied();
            }
        }

        protected void SetHp(int hp)
        {
            _hp = hp;
            _healthBar.SetHealthSlider((float) _hp / (float) _maxHp);
        }

        protected virtual void Die()
        {
            _animator.speed = 1;

            if(_navMeshAgent)
                _navMeshAgent.isStopped = true;
            
            _animator.SetTrigger(DieTrigger);
            _healthBar.gameObject.SetActive(false);
            Destroy(gameObject, 3);
        }

        private void OnOpponentDied()
        {
            _opponent = null;
            _animator.SetBool(AttackAnimBool, false);
        }

        private void OnTriggerEnter(Collider other)
        {
            Unit otherUnit = other.GetComponent<Unit>();
            bool enemyCondition = GetComponent<Enemy>() && other.GetComponent<Castle>();
            if (otherUnit != null && enemyCondition)
            {
                _navMeshAgent.isStopped = true;
                _opponent = other.GetComponent<Unit>();
                UpdateAttackAnim(true);
            }

            ETFXProjectileScript projectile = other.GetComponent<ETFXProjectileScript>();
            if (projectile != null && GetComponent<Enemy>() != null)
            {
                TakeDamage(projectile.Damage);
                if (GetComponent<Enemy>() && projectile.gameObject.name == "FrostMissileOBJ(Clone)")
                {
                    Debug.Log("Frozen");
                    _navMeshAgent.speed *= 0.5f;
                    _animator.speed *= 0.5f;
                    GetComponentInChildren<SkinnedMeshRenderer>().material = _frozenMaterial;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Unit otherUnit = other.GetComponent<Unit>();
            bool enemyCondition = GetComponent<Enemy>() && other.GetComponent<Castle>();
            if (otherUnit != null && enemyCondition)
            {
                _navMeshAgent.isStopped = true;
                _opponent = null;
                UpdateAttackAnim(true);
            }
        }

        private void UpdateAttackAnim(bool attack)
        {
            _animator.SetBool(AttackAnimBool, attack);
        }

        private void Move()
        {
            if (MoveTarget)
            {
                transform.LookAt(MoveTarget.transform);
                transform.position += transform.forward * (_moveSpeed * Time.deltaTime);
            }
        }

        protected void SetMaxHp(int hp)
        {
            _maxHp = hp;
            SetHp(_maxHp);
        }
    }
}
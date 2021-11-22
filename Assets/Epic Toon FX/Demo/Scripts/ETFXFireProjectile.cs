using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace EpicToonFX
{
    public class ETFXFireProjectile : MonoBehaviour
    {
        [SerializeField]
        public GameObject[] projectiles;

        [SerializeField] public List<ProjectilePair> projectilesDict;
        
        [Serializable]
        public class ProjectilePair
        {
            public string Name;
            public GameObject prefab;
        }
        
        [Header("Missile spawns at attached game object")]
        public Transform spawnPosition;
        
        private Dictionary<string, float> _shootingCooldown = new()
        {
            {"fireball", 1},
            {"frostbolt", 0.5f}
        };

        [SerializeField] private Transform _projectileLookAt;
        // [SerializeField] private float _scale = 0.5f;

        [HideInInspector]
        public int currentProjectile = 0;
        public float speed = 300;

        //    MyGUI _GUI;
        // ETFXButtonScript selectedProjectileButton;

        void Start()
        {
            // selectedProjectileButton = GameObject.Find("Button").GetComponent<ETFXButtonScript>();
            _currentProjectile = projectilesDict.FirstOrDefault(p => p.Name.Equals("fireball"));
        }

        RaycastHit hit;
        private float _cooldown;
        private ProjectilePair _currentProjectile;


        void Update()
        {
            _cooldown -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextEffect();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                nextEffect();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                previousEffect();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                previousEffect();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) //On left mouse down-click
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f)) //Finds the point where you click with the mouse
                {
                    // GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                    GameObject projectile = Instantiate(_currentProjectile.prefab, spawnPosition.position, Quaternion.identity); //Spawns the selected projectile
                    projectile.transform.LookAt(hit.point); //Sets the projectiles rotation to look at the point clicked
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                }
            }
            Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);

            if ((Input.GetKeyDown(KeyCode.S) || FingerActivator.ActiveAttack) && _cooldown <= 0)
            {
                Shoot();
                _cooldown = _shootingCooldown[_currentProjectile.Name];
            }
        }

        public void SetCurrentProjectile(string name)
        {
            _currentProjectile = projectilesDict.FirstOrDefault(p => p.Name.Equals(name));
        }
        
        public void Shoot()
        {
            // GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
            GameObject projectile = Instantiate(_currentProjectile.prefab, spawnPosition.position, Quaternion.identity); //Spawns the selected projectile
            // projectile.transform.localScale = Vector3.one * _scale;
            projectile.transform.LookAt(_projectileLookAt);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
        }
        
        public void nextEffect() //Changes the selected projectile to the next. Used by UI
        {
            if (currentProjectile < projectiles.Length - 1)
                currentProjectile++;
            else
                currentProjectile = 0;
			// selectedProjectileButton.getProjectileNames();
        }

        public void previousEffect() //Changes selected projectile to the previous. Used by UI
        {
            if (currentProjectile > 0)
                currentProjectile--;
            else
                currentProjectile = projectiles.Length - 1;
			// selectedProjectileButton.getProjectileNames();
        }

        public void AdjustSpeed(float newSpeed) //Used by UI to set projectile speed
        {
            speed = newSpeed;
        }
    }
}
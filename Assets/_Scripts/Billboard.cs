using UnityEngine;

namespace _Scripts
{
    public class Billboard : MonoBehaviour
    {
        private Transform _cam;

        private void Awake()
        {
            _cam = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _cam.forward);
        }
    }
}

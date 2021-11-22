using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{
   [SerializeField] private UnityEvent _event;

   private void OnTriggerEnter(Collider other)
   {
      _event.Invoke();
   }
}

using UnityEngine;

public class FingerActivator : MonoBehaviour
{
    private static int _activeAttackActivators;
    public static bool VoiceActivated = false;
    public static bool ActiveAttack => _activeAttackActivators == 2;
    
    [SerializeField] private FingerActivator _voicePair;
    [SerializeField] private FingerActivator _attackPair;
    [SerializeField] private Material _activeMaterial;
    [SerializeField] private Material _nonactiveMaterial;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        FingerActivator otherActivator = other.GetComponent<FingerActivator>();
        if (otherActivator != null && _voicePair != null && ReferenceEquals(otherActivator.gameObject, _voicePair.gameObject))
        {
                VoiceActivated = true;
        }
        
        else if (otherActivator != null && _attackPair != null && ReferenceEquals(otherActivator.gameObject, _attackPair.gameObject))
        {
            Debug.Log("OnTriggerEnter: " + gameObject.name + "_activeAttackActivators " + _activeAttackActivators);
            _activeAttackActivators++;
            _meshRenderer.material = _activeMaterial;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            VoiceActivated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        FingerActivator otherActivator = other.GetComponent<FingerActivator>();
        if (otherActivator != null && _voicePair != null && ReferenceEquals(otherActivator.gameObject, _voicePair.gameObject))
        {
            VoiceActivated = true;
        }
        
        else if (otherActivator != null && _attackPair != null && ReferenceEquals(otherActivator.gameObject, _attackPair.gameObject))
        {
            _activeAttackActivators--;
            _meshRenderer.material = _nonactiveMaterial;
        }
    }
}

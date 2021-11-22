using DG.Tweening;
using TMPro;
using UnityEngine;

public class SpellCircle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _centerOfFingerPoint;

    private Tween _tween;
    private Vector3 _startingScale;
    private string _startingText;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _startingScale = transform.localScale;
        transform.localScale = Vector3.zero;
        _startingText = _text.text;
    }

    private void Update()
    {
        UpdatePos();
    }

    private void UpdatePos()
    {
        transform.position = _centerOfFingerPoint.position;
        transform.LookAt(_camera.transform);
    }

    public void OnSpellInvoked(string spellName)
    {
        _text.text = spellName;
        TryKillTween();
        _tween = transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutExpo)
            // .OnComplete(()=>_text.text = "")
            // .OnKill(()=>_text.text="")
            ;
    }

    public void OnListeningActivated()
    {
        _text.alpha = 1;
        TryKillTween();
        _tween = transform.DOScale(_startingScale, 0.3f).SetEase(Ease.OutExpo);
        // _text.text = _startingText;
    }
    
    public void OnListeningDeactivated()
    {
        TryKillTween();
        _tween = transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutExpo);
    }

    private void TryKillTween()
    {
        if (_tween != null && _tween.IsActive())
            _tween.Kill();
    }
}
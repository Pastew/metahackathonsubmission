using System.Globalization;
using Facebook.WitAi;
using Facebook.WitAi.CallbackHandlers;
using Facebook.WitAi.Lib;
using UnityEngine;
using UnityEngine.Events;

public class MyIntentHandler : WitResponseHandler
{
    [SerializeField] public string intent;
    [Range(0, 1f)] [SerializeField] public float confidence = .9f;
    [SerializeField] private UnityEvent onIntentTriggered = new UnityEvent();

    public UnityEvent OnIntentTriggered => onIntentTriggered;

    protected override void OnHandleResponse(WitResponseNode response)
    {
        var intentNode = WitResultUtilities.GetFirstIntent(response);
        bool intentNameMatch = intent.Equals(intentNode["name"].Value);
        bool confidenceMatch = float.Parse(intentNode["confidence"], CultureInfo.InvariantCulture) > confidence;

        if (intentNameMatch && confidenceMatch)
        {
            onIntentTriggered.Invoke();
        }
    }
}
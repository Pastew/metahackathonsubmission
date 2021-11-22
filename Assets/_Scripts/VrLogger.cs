using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class VrLogger : MonoBehaviour
    {
        public static VrLogger Instance;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            Instance = this;
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void Log(string text)
        {
            _text.text += text;
            _text.text += "\n";
        }
    }
}

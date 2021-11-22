/**************************************************************************************************
 * Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.
 *
 * Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
 * under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 **************************************************************************************************/

using Facebook.WitAi;
using Facebook.WitAi.Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Oculus.Voice.Demo.UIShapesDemo
{
    public class InteractionHandler : MonoBehaviour
    {
        [Header("Default States"), Multiline] [SerializeField]
        private string freshStateText = "Try pressing the Activate button and saying \"Make the cube red\"";

        [Header("UI")] [SerializeField] private Text textArea;
        [SerializeField] private bool showJson;

        [Header("Voice")] [SerializeField] private AppVoiceExperience appVoiceExperience;

        [SerializeField] private OVRHand _rightHand;
        [SerializeField] private OVRHand _leftHand;

        [SerializeField] private SpellCircle _spellCircle;

        private string pendingText;

        private void OnEnable()
        {
            appVoiceExperience.events.OnRequestCreated.AddListener(OnRequestStarted);
        }

        private void OnDisable()
        {
            appVoiceExperience.events.OnRequestCreated.RemoveListener(OnRequestStarted);
        }

        private void OnRequestStarted(WitRequest r)
        {
            // The raw response comes back on a different thread. We store the
            // message received for display on the next frame.
            if (showJson) r.onRawResponse = (response) => pendingText = response;
        }

        private void Update()
        {
            if (null != pendingText)
            {
                textArea.text = pendingText;
                pendingText = null;
            }

            if (_rightHand.IsSystemGestureInProgress) return;

            if (FingerActivator.VoiceActivated)
            {
                SetListeningActive(true);
                FingerActivator.VoiceActivated = false;
            }
        }

        public void OnResponse(WitResponseNode response)
        {
            if (!string.IsNullOrEmpty(response["text"]))
            {
                textArea.text = "I heard: " + response["text"];
            }
            else
            {
                textArea.text = freshStateText;
            }
        }

        public void OnError(string error, string message)
        {
            textArea.text = $"<color=\"red\">Error: {error}\n\n{message}</color>";
        }

        public void SetListeningActive(bool setToActive)
        {
            if (!setToActive)
            {
                // appVoiceExperience.Deactivate();
                _spellCircle.OnListeningDeactivated();
            }
            else if (setToActive && !appVoiceExperience.Active)
            {
                appVoiceExperience.Activate();
                _spellCircle.OnListeningActivated();
            }
        }
    }
}
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ConsoleView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _logText;

        public void AddLog(string log)
        {
            _logText.SetText(log);
        }
    }
}

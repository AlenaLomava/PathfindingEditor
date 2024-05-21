using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ConsolePresenter
    {
        private readonly ConsoleView _consoleView;
        private StringBuilder _logBuilder = new StringBuilder();
        private readonly Queue<string> _logQueue = new Queue<string>();

        private const int MAX_LOG_COUNT = 3;

        public ConsolePresenter(ConsoleView consoleView)
        {
            _consoleView = consoleView;

            Application.logMessageReceived += HandleLog;
        }

        public void Dispose()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            _logBuilder.Clear();

            _logBuilder.Append(type.ToString());
            _logBuilder.Append(": ");

            string firstLine = logString.Split('\n')[0];
            _logBuilder.Append(firstLine);

            string log = _logBuilder.ToString();

            if (_logQueue.Count >= MAX_LOG_COUNT)
            {
                _logQueue.Dequeue();
            }

            _logQueue.Enqueue(log);

            UpdateConsoleView();
        }

        private void UpdateConsoleView()
        {
            _logBuilder.Clear();

            foreach (var log in _logQueue)
            {
                _logBuilder.AppendLine(log);
            }

            _consoleView.AddLog(_logBuilder.ToString());
        }
    }
}

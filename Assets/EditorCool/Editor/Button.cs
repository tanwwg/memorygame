using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EditorCools.Editor
{
    public abstract class EditorCoolsButton
    {
        public abstract void Draw(Object[] targets);
    }
    
    public class ActionButton: EditorCoolsButton
    {
        private readonly string displayName;

        private readonly Action<object> invoker;

        public ActionButton(string displayName, Action<object> invoker)
        {
            this.displayName = displayName;
            this.invoker = invoker;
        }

        public override void Draw(Object[] targets)
        {
            if (!GUILayout.Button(displayName)) return;

            foreach (var target in targets)
            {
                invoker.Invoke(target);
                // Method.Invoke(target, null);
            }
        }
    }
    
    public class StringButton: EditorCoolsButton
    {
        private readonly string displayName;

        private readonly Action<object, string> invoker;

        private string _text;

        public StringButton(string displayName, Action<object, string> invoker)
        {
            this.displayName = displayName;
            this.invoker = invoker;
        }

        public override void Draw(Object[] targets)
        {
            GUILayout.Label(displayName, EditorStyles.boldLabel);
            _text = GUILayout.TextField(_text);
            if (!GUILayout.Button("Invoke " + displayName)) return;

            foreach (var target in targets)
            {
                invoker.Invoke(target, _text);
            }
        }
    }
}
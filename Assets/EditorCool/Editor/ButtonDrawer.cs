using UnityEngine;
using UnityEngine.Events;

namespace EditorCools.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;

    public class ButtonsDrawer
    {
        // public readonly List<IGrouping<string, Button>> ButtonGroups;
        private readonly List<EditorCoolsButton> buttons = new();

        private string GetDisplayName(ButtonAttribute buttonAttribute, string name)
        {
            return string.IsNullOrEmpty(buttonAttribute.Name)
                ? ObjectNames.NicifyVariableName(name)
                : buttonAttribute.Name;
        }

        public ButtonsDrawer(object target)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = target.GetType().GetMethods(flags);

            foreach (MethodInfo method in methods)
            {
                HandleMethod(method);
            }

            var fields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var f in fields)
            {
                var buttonAttribute = f.GetCustomAttribute<ButtonAttribute>();
                if (buttonAttribute == null)
                    continue;

                // Debug.Log("Found " + f.Name);
                if (f.FieldType == typeof(UnityEvent))
                {
                    // Debug.Log("Found " + f.Name);
                    buttons.Add(new ActionButton(GetDisplayName(buttonAttribute, "UnityEvent: " + f.Name),
                        t =>
                        {
                            var evt = f.GetValue(t) as UnityEvent;
                            evt?.Invoke();
                        }));
                    
                }
                
                if (f.FieldType == typeof(UnityEvent<string>))
                {
                    buttons.Add(new StringButton(GetDisplayName(buttonAttribute, $"UnityEvent: {f.Name}"),
                        (t,s) =>
                        {
                            var evt = f.GetValue(t) as UnityEvent<string>;
                            evt?.Invoke(s);
                        }));
                    
                }
            }
        }

        private void HandleMethod(MethodInfo method)
        {
            var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
            if (buttonAttribute == null)
                return;

            var parms = method.GetParameters();
            if (parms.Length == 0)
            {
                buttons.Add(new ActionButton(GetDisplayName(buttonAttribute, method.Name),
                    t => method.Invoke(t, null)
                ));
            } 
            else if (parms.Length == 1 && parms[0].ParameterType == typeof(string))
            {
                buttons.Add(new StringButton(GetDisplayName(buttonAttribute, method.Name),
                    (t,s) => method.Invoke(t, new object[] {s})));
            }
            else
            {
                Debug.LogWarningFormat("Unable to handle Button attribute for {0}", method.Name);
            }

        }

        public void DrawButtons(Object[] targets)
        {
            // using (new EditorGUILayout.HorizontalScope())
            // {
                foreach (var button in buttons)
                {
                    button.Draw(targets);
                }
            // }
        }
    }
}
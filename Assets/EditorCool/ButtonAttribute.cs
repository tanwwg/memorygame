namespace EditorCools
{
    using System;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class ButtonAttribute : Attribute
    {
        public readonly string Name;

        // public readonly string StringArg;
        // public readonly string Row;
        // public readonly float Space;
        // public readonly bool HasRow;

        public ButtonAttribute(string name = null)
        {
            Name = name;
            // StringArg = stringArg;
        }
    }
}
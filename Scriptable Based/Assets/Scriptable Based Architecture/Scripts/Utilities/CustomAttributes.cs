using UnityEngine;

namespace ScriptableBasedArchitecture
{
    public class GreyOutAttribute : PropertyAttribute { }

    public class LabelAttribute : PropertyAttribute
    {
        public string label;

        public LabelAttribute(string label)
        {
            this.label = label;
        }
    }
}
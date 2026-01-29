using UnityEngine;

public class SubHeaderAttribute : PropertyAttribute {
    public readonly string text;

    public SubHeaderAttribute(string text)
    {
        this.text = text;
    }
}

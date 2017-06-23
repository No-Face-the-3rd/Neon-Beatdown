using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;




[CustomPropertyDrawer(typeof(EnumMaskAttribute))]
public class EffectTypeEditor : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        //base.OnGUI(position, property, label);
        EnumMaskAttribute mask = (EnumMaskAttribute)attribute;

        if(property.propertyType == SerializedPropertyType.Enum)
        {
            if(mask.mask)
            {
                property.intValue = (int)EditorGUI.MaskField(position, label.text, property.intValue, property.enumDisplayNames);
            }
            else
            {
                property.enumValueIndex = EditorGUI.Popup(position, label.text, property.enumValueIndex, property.enumDisplayNames);
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use EnumMask with enum.");
        }
    }

}

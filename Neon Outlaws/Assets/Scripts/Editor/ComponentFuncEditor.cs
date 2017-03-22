//using UnityEngine;
//using UnityEditor;

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//[CustomEditor(typeof(MonoBehaviour), true)]
//[CanEditMultipleObjects]
//public class MonoBehaviourEditor : Editor
//{
//}

//[CustomPropertyDrawer(typeof(ComponentFunc))]
//public class ComponentFuncEditor : PropertyDrawer
//{
//    string nameLabel = "Field Label";
//    GameObject targ;
//    string[] returnTypeChoices;
//    string returnType;
//    int returnTypeMask;
//    string[] funcChoices = new[] { "", "meow" };
//    int funcChoiceInd = 0;
//    string[] compChoices = new[] { "", "mew" };
//    int compChoiceInd = 0;
//    string component = "";
//    string function = "";
//    bool foldout = false;
//    int numParams = 0;
//    object[] parameters = new object[4];
//    string[] paramTypes = new string[4];
//    bool[] editParams = new bool[4];

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        //store current values
//        compChoiceInd = property.FindPropertyRelative("compInd").intValue;
//        funcChoiceInd = property.FindPropertyRelative("choiceInd").intValue;
//        targ = (GameObject)property.FindPropertyRelative("target").objectReferenceValue;
//        returnTypeMask = property.FindPropertyRelative("returnTypeMask").intValue;
//        returnType = property.FindPropertyRelative("returnType").stringValue;
//        component = property.FindPropertyRelative("component").stringValue;
//        function = property.FindPropertyRelative("choice").stringValue;
//        foldout = property.FindPropertyRelative("foldout").boolValue;
//        numParams = property.FindPropertyRelative("numParams").intValue;
//        int prevParamNum = numParams;
//        for(int i = 0;i < numParams;i++)
//        {
//            UnityEngine.Object targObj = property.serializedObject.targetObject;
//            Type targType = targObj.GetType();
//            SerializedProperty prop = property.FindPropertyRelative("parameters");

//            //((SliderController)targObj).percentFunction.parameters[i]

//            prop = property.FindPropertyRelative("parameters").GetArrayElementAtIndex(i);
//            string path = prop.propertyPath;
//            FieldInfo bleh = targType.GetField("percentFunction.parameters[0]");
//            FieldInfo para = targType.GetField(path);
//            parameters[i] = ((serializableObject)para.GetValue(targObj)).obj;
//            paramTypes[i] = property.FindPropertyRelative("paramTypes").GetArrayElementAtIndex(i).stringValue;
//            editParams[i] = property.FindPropertyRelative("editParams").GetArrayElementAtIndex(i).boolValue;
//        }


//        if (!foldout)
//        {

//            EditorGUILayout.PropertyField(property.FindPropertyRelative("label"));
//        }
//        nameLabel = property.FindPropertyRelative("label").stringValue;
//        nameLabel = String.Equals(nameLabel, "") ? "Field Label" : nameLabel;
//        property.FindPropertyRelative("label").stringValue = nameLabel;

//        //create string list for choices
//        List<string> choiceList = new List<String>();
//        List<string> choiceQualified = new List<String>();

//        foldout = EditorGUILayout.Foldout(foldout, nameLabel);

//        //do label
//        //choose gameobject
//        if (foldout)
//        {
//            EditorGUI.indentLevel++;
//            targ = (GameObject)EditorGUILayout.ObjectField(nameLabel + " Target ", targ, typeof(GameObject), true);

//            if (targ != null)
//            {
//                //choose component
//                Component[] components = targ.GetComponents<Component>();
//                foreach (Component comp in components)
//                {
//                    choiceList.Add(comp.GetType().ToString());
//                    choiceQualified.Add(comp.GetType().AssemblyQualifiedName);
//                }
//                compChoices = choiceList.ToArray();
//                int check = compChoiceInd;
//                compChoiceInd = EditorGUILayout.Popup(nameLabel + " Component", compChoiceInd, compChoices);
//                component = compChoiceInd >= 0 ? compChoices[compChoiceInd] : component;
//                compChoices = choiceQualified.ToArray();
//                string compQual = compChoiceInd >= 0 ? compChoices[compChoiceInd] : component;
//                if (compChoiceInd >= 0 && compChoiceInd == check)
//                {
//                    MethodInfo[] methods = Type.GetType(choiceQualified.ToArray()[compChoiceInd]).GetMethods();

//                    choiceList.Clear();
//                    choiceQualified.Clear();

//                    foreach (MethodInfo method in methods)
//                    {
//                        ParameterInfo[] p = method.GetParameters();
//                        string retType = method.ReturnType.ToString();
//                        int retPer = retType.IndexOf('.');
//                        if (retPer >= 0)
//                            retType = retType.Substring(retPer + 1);
//                        if (p.Length <= 4 && !choiceList.Exists(element => String.Equals(element, retType)) && !String.Equals(retType, "T") && !String.Equals(retType, "T[]"))
//                        {
//                            choiceList.Add(retType);
//                            choiceQualified.Add(method.ReturnType.AssemblyQualifiedName);
//                        }
//                    }
//                    check = returnTypeMask;
//                    returnTypeChoices = choiceList.ToArray();
//                    returnTypeMask = EditorGUILayout.MaskField(nameLabel + " Return Type", returnTypeMask, returnTypeChoices);
//                    string[] typeQual = choiceQualified.ToArray();

//                    choiceList.Clear();
//                    choiceQualified.Clear();

//                    if (returnTypeMask < 0)
//                    {
//                        returnTypeMask = (1 << returnTypeChoices.Length) - 1;
//                    }
//                    for (int j = 0; j < methods.Length; j++)
//                    {
//                        MethodInfo method = methods[j];
//                        ParameterInfo[] p = method.GetParameters();
//                        if (p.Length <= 4 && returnTypeMask > 0 && (method.IsPublic))
//                        {
//                            for (int i = 0; i < returnTypeChoices.Length; i++)
//                            {
//                                if ((returnTypeMask & (1 << i)) > 0)
//                                {
//                                    if (method.ReturnType == Type.GetType(typeQual[i]))
//                                    {
//                                        choiceList.Add(method.Name);
//                                        choiceQualified.Add(method.ReflectedType.Name);
//                                    }
//                                }
//                            }
//                        }
//                    }
//                    if (returnTypeMask == check && returnTypeMask > 0)
//                    {

//                        funcChoices = choiceList.ToArray();
//                        funcChoiceInd = EditorGUILayout.Popup(nameLabel + " Function", funcChoiceInd, funcChoices);

//                        function = funcChoiceInd >= 0 ? funcChoices[funcChoiceInd] : function;

//                        returnType = (funcChoiceInd >= 0 && compChoiceInd >= 0) ? Type.GetType(compQual).GetMethod(function).ReturnType.AssemblyQualifiedName : "";
//                    }
//                    else
//                    {
//                        funcChoiceInd = -1;
//                        function = "";
//                    }

//                    if(methods.Length > 0 && funcChoiceInd < methods.Length && funcChoiceInd >= 0)
//                    {
//                        MethodInfo method = methods[funcChoiceInd];
//                        ParameterInfo[] p = method.GetParameters();
//                        numParams = p.Length;
//                        if (p.Length <= 4 && p.Length > 0)
//                        {
//                            for(int i = 0;i < p.Length;i++)
//                            {
//                                editParams[i] = EditorGUILayout.Toggle(p[i].Name,editParams[i]);
//                            }
//                        }
//                    }

//                }
//                else if (compChoiceInd != check)
//                {
//                    returnTypeMask = 0;
//                    funcChoiceInd = -1;
//                }

//            }
//            else
//            {
//                compChoiceInd = -1;
//                funcChoiceInd = -1;
//                returnTypeMask = 0;
//                returnType = "";
//                component = "";
//                function = "";
//                parameters = new object[4];
//                editParams = new bool[4];
//            }
//            EditorGUI.indentLevel--;
//        }

//        //store changed values
//        property.FindPropertyRelative("compInd").intValue = compChoiceInd;
//        property.FindPropertyRelative("choiceInd").intValue = funcChoiceInd;
//        property.FindPropertyRelative("target").objectReferenceValue = targ;
//        property.FindPropertyRelative("returnTypeMask").intValue = returnTypeMask;
//        property.FindPropertyRelative("returnType").stringValue = returnType;
//        property.FindPropertyRelative("component").stringValue = component;
//        property.FindPropertyRelative("choice").stringValue = function;
//        property.FindPropertyRelative("foldout").boolValue = foldout;
//        property.FindPropertyRelative("numParams").intValue = numParams;
//        for(int i = 0;i < numParams && prevParamNum != 0;i++)
//        {
//            FieldInfo para = property.serializedObject.targetObject.GetType().GetField(property.FindPropertyRelative("parameters").GetArrayElementAtIndex(i).propertyPath);
//            para.SetValue(property.serializedObject.targetObject, parameters[i]);
//            property.FindPropertyRelative("paramTypes").GetArrayElementAtIndex(i).stringValue = paramTypes[i];
//            property.FindPropertyRelative("editParams").GetArrayElementAtIndex(i).boolValue = editParams[i];
//        }

//    }

//}

using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[CustomEditor(typeof(MonoBehaviour), true)]
[CanEditMultipleObjects]
public class MonoBehaviourEditor : Editor
{
}

[CustomPropertyDrawer(typeof(ComponentFunc))]
public class ComponentFuncEditor : PropertyDrawer
{
    string nameLabel = "Field Label";
    GameObject targ;
    string[] returnTypeChoices;
    string returnType;
    int returnTypeMask;
    string[] funcChoices = new[] { "", "meow" };
    int funcChoiceInd = 0;
    string[] compChoices = new[] { "", "mew" };
    int compChoiceInd = 0;
    string component = "";
    string function = "";
    bool foldout = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //store current values
        compChoiceInd = property.FindPropertyRelative("compInd").intValue;
        funcChoiceInd = property.FindPropertyRelative("choiceInd").intValue;
        targ = (GameObject)property.FindPropertyRelative("target").objectReferenceValue;
        returnTypeMask = property.FindPropertyRelative("returnTypeMask").intValue;
        returnType = property.FindPropertyRelative("returnType").stringValue;
        component = property.FindPropertyRelative("component").stringValue;
        function = property.FindPropertyRelative("choice").stringValue;
        foldout = property.FindPropertyRelative("foldout").boolValue;

        if (!foldout)
        {

            EditorGUILayout.PropertyField(property.FindPropertyRelative("label"));
        }
        nameLabel = property.FindPropertyRelative("label").stringValue;
        nameLabel = String.Equals(nameLabel, "") ? "Field Label" : nameLabel;
        property.FindPropertyRelative("label").stringValue = nameLabel;

        //create string list for choices
        List<string> choiceList = new List<String>();
        List<string> choiceQualified = new List<String>();

        foldout = EditorGUILayout.Foldout(foldout, nameLabel);

        //do label
        //choose gameobject
        if (foldout)
        {
            EditorGUI.indentLevel++;
            targ = (GameObject)EditorGUILayout.ObjectField(nameLabel + " Target ", targ, typeof(GameObject), true);

            if (targ != null)
            {
                //choose component
                Component[] components = targ.GetComponents<Component>();
                foreach (Component comp in components)
                {
                    choiceList.Add(comp.GetType().ToString());
                    choiceQualified.Add(comp.GetType().AssemblyQualifiedName);
                }
                compChoices = choiceList.ToArray();
                int check = compChoiceInd;
                compChoiceInd = EditorGUILayout.Popup(nameLabel + " Component", compChoiceInd, compChoices);
                component = compChoiceInd >= 0 ? compChoices[compChoiceInd] : component;
                compChoices = choiceQualified.ToArray();
                string compQual = compChoiceInd >= 0 ? compChoices[compChoiceInd] : component;
                if (compChoiceInd >= 0 && compChoiceInd == check)
                {
                    MethodInfo[] methods = Type.GetType(choiceQualified.ToArray()[compChoiceInd]).GetMethods();

                    choiceList.Clear();
                    choiceQualified.Clear();

                    foreach (MethodInfo method in methods)
                    {
                        ParameterInfo[] p = method.GetParameters();
                        string retType = method.ReturnType.ToString();
                        int retPer = retType.IndexOf('.');
                        if (retPer >= 0)
                            retType = retType.Substring(retPer + 1);
                        if (p.Length == 0 && !choiceList.Exists(element => String.Equals(element, retType)) && !String.Equals(retType, "T") && !String.Equals(retType, "T[]"))
                        {
                            choiceList.Add(retType);
                            choiceQualified.Add(method.ReturnType.AssemblyQualifiedName);
                        }
                    }
                    check = returnTypeMask;
                    returnTypeChoices = choiceList.ToArray();
                    returnTypeMask = EditorGUILayout.MaskField(nameLabel + " Return Type", returnTypeMask, returnTypeChoices);
                    string[] typeQual = choiceQualified.ToArray();

                    choiceList.Clear();
                    choiceQualified.Clear();

                    if (returnTypeMask < 0)
                    {
                        returnTypeMask = (1 << returnTypeChoices.Length) - 1;
                    }
                    for (int j = 0; j < methods.Length; j++)
                    {
                        MethodInfo method = methods[j];
                        ParameterInfo[] p = method.GetParameters();
                        if (p.Length == 0 && returnTypeMask > 0 && (method.IsPublic))
                        {
                            for (int i = 0; i < returnTypeChoices.Length; i++)
                            {
                                if ((returnTypeMask & (1 << i)) > 0)
                                {
                                    if (method.ReturnType == Type.GetType(typeQual[i]))
                                    {
                                        choiceList.Add(method.Name);
                                        choiceQualified.Add(method.ReflectedType.Name);
                                    }
                                }
                            }
                        }
                    }
                    if (returnTypeMask == check && returnTypeMask > 0)
                    {

                        funcChoices = choiceList.ToArray();
                        funcChoiceInd = EditorGUILayout.Popup(nameLabel + " Function", funcChoiceInd, funcChoices);

                        function = funcChoiceInd >= 0 ? funcChoices[funcChoiceInd] : function;

                        returnType = (funcChoiceInd >= 0 && compChoiceInd >= 0) ? Type.GetType(compQual).GetMethod(function).ReturnType.AssemblyQualifiedName : "";
                    }
                    else
                    {
                        funcChoiceInd = -1;
                        function = "";
                    }

                }
                else if (compChoiceInd != check)
                {
                    returnTypeMask = 0;
                    funcChoiceInd = -1;
                }

            }
            else
            {
                compChoiceInd = -1;
                funcChoiceInd = -1;
                returnTypeMask = 0;
                returnType = "";
                component = "";
                function = "";
            }
            EditorGUI.indentLevel--;
        }

        //store changed values
        property.FindPropertyRelative("compInd").intValue = compChoiceInd;
        property.FindPropertyRelative("choiceInd").intValue = funcChoiceInd;
        property.FindPropertyRelative("target").objectReferenceValue = targ;
        property.FindPropertyRelative("returnTypeMask").intValue = returnTypeMask;
        property.FindPropertyRelative("returnType").stringValue = returnType;
        property.FindPropertyRelative("component").stringValue = component;
        property.FindPropertyRelative("choice").stringValue = function;
        property.FindPropertyRelative("foldout").boolValue = foldout;


    }

}
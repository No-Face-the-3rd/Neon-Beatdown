//using UnityEngine;
//using System;
//using System.Collections;
//using System.Reflection;

//[System.Serializable]
//public class ComponentFuncs
//{
//    public ComponentFunc[] functions;

//    public object callFunc(int ind)
//    {
//        return functions[ind].callFunc();
//    }

//    public Type getType(int ind)
//    {
//        return functions[ind].GetType();
//    }
//}

//[System.Serializable]
//public class serializableObject : ISerializationCallbackReceiver
//{
//    [NonSerialized]
//    public object obj;
//    private string serialized;
//    private string serializedType;

//    public serializableObject()
//    {
//        obj = new object();
//        serialized = "";
//        serializedType = "";
//    }
//    public serializableObject(object objIn)
//    {
//        obj = objIn;
//        serialized = obj.ToString();
//        serializedType = obj.GetType().ToString();
//    }

//    public void OnBeforeSerialize()
//    {
//        serialized = obj.ToString();
//        serializedType = obj.GetType().ToString();
//    }
//    public void OnAfterDeserialize()
//    {
//        obj = Convert.ChangeType(serialized, Type.GetType(serializedType));
//    }
//}

//[System.Serializable]
//public class ComponentFunc
//{
//    public string label = "Field Label";
//    public GameObject target;
//    public string component;
//    public int compInd;
//    public string returnType;
//    public int returnTypeMask;
//    public string choice;
//    public int choiceInd;
//    public bool foldout;
//    public int numParams;
//    public serializableObject[] parameters = new serializableObject[4] { new serializableObject(1), new serializableObject(1) , new serializableObject(1) , new serializableObject(1) };
//    public string[] paramTypes = new string[4];
//    public bool[] editParams = new bool[4];

//    public object callFunc()
//    {
//        object rawr = 5.0f;
//        rawr = "bleh";
//        if (component.Length == 0 || choice.Length == 0 || target == null)
//        {
//            throw new Exception("Part not Chosen");
//        }
//        var tmp = target.GetComponent(component);

//        MethodInfo method = Type.GetType(component).GetMethod(choice);

//        object outCur = null;
//        if (numParams > 0)
//        {
//            object[] paramArray = new object[numParams];
//            for (int i = 0; i < numParams; i++)
//                paramArray[i] = parameters[i];
//            outCur = method.Invoke(tmp, paramArray);
//        }
//        else if (numParams == 0)
//        {
//            outCur = method.Invoke(tmp, null);
//        }


//        return outCur;
//    }

//    public Type getType()
//    {
//        return Type.GetType(returnType);
//    }
//}

using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

[System.Serializable]
public class ComponentFuncs
{
    public ComponentFunc[] functions;

    public object callFunc(int ind)
    {
        return functions[ind].callFunc();
    }

    public Type getType(int ind)
    {
        return functions[ind].GetType();
    }
}

[System.Serializable]
public class ComponentFunc
{
    public string label = "Field Label";
    public GameObject target;
    public string component;
    public int compInd;
    public string returnType;
    public int returnTypeMask;
    public string choice;
    public int choiceInd;
    public bool foldout;

    public object callFunc()
    {
        if (component.Length == 0 || choice.Length == 0 || target == null)
        {
            throw new Exception("Part not Chosen");
        }
        var tmp = target.GetComponent(component);

        MethodInfo method = Type.GetType(component).GetMethod(choice);
        object outCur = method.Invoke(tmp, null);


        return outCur;
    }

    public Type getType()
    {
        return Type.GetType(returnType);
    }
}
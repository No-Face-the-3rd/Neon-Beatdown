using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumMaskAttribute : PropertyAttribute {
    public bool mask;

    public EnumMaskAttribute(bool isMask = true)
    {
        mask = isMask;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace CustomInspector
{
    /// <summary>
    /// Removes or adds an indentlevel from current indentLevel
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class IndentAttribute : PropertyAttribute
    {
        public readonly int additionalIndentLevel;
        public IndentAttribute(int additionalIndentLevel)
        {
            order = -10;

            this.additionalIndentLevel = additionalIndentLevel;
        }
    }
}

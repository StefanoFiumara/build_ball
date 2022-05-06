using System;
using UnityEngine;

#if UNITY_EDITOR
using System.Collections.Generic;
using Extensions;
using UnityEditor;
#endif

namespace Extensions
{
    /// <summary>
    /// Use this property on a ScriptableObject type to allow the editors drawing the field to draw an expandable
    /// area that allows for changing the values on the object without having to change editor.
    /// </summary>
    public class ExpandableAttribute : PropertyAttribute
    {

    }
}


#if UNITY_EDITOR
/// <summary>
/// Draws the property field for any field marked with ExpandableAttribute.
/// </summary>
[CustomPropertyDrawer (typeof (ExpandableAttribute), true)]
public class ExpandableAttributeDrawer : PropertyDrawer
{
    // Use the following area to change the style of the expandable ScriptableObject drawers;
    #region Style Setup
    private enum BackgroundStyles
    {
        HelpBox,
        Darken,
        Lighten
    }

    /// <summary>
    /// Whether the default editor Script field should be shown.
    /// </summary>
    private static bool SHOW_SCRIPT_FIELD = false;

    /// <summary>
    /// The spacing on the inside of the background rect.
    /// </summary>
    private static float INNER_SPACING = 6.0f;

    /// <summary>
    /// The spacing on the outside of the background rect.
    /// </summary>
    private static float OUTER_SPACING = 4.0f;

    /// <summary>
    /// The style the background uses.
    /// </summary>
    private static BackgroundStyles BACKGROUND_STYLE = BackgroundStyles.HelpBox;

    /// <summary>
    /// The colour that is used to darken the background.
    /// </summary>
    private static readonly Color Darken_Color = new (0.0f, 0.0f, 0.0f, 0.2f);

    /// <summary>
    /// The colour that is used to lighten the background.
    /// </summary>
    private static readonly Color Lighten_Color = new(1.0f, 1.0f, 1.0f, 0.2f);
    #endregion

    /// <summary>
    /// Cached editor reference.
    /// </summary>
    private Editor _editor;

    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        float totalHeight = 0.0f;

        totalHeight += EditorGUIUtility.singleLineHeight;

        if (property.objectReferenceValue == null)
            return totalHeight;

        if (!property.isExpanded)
            return totalHeight;

        if (_editor == null)
            Editor.CreateCachedEditor (property.objectReferenceValue, null, ref _editor);

        if (_editor == null)
            return totalHeight;

        SerializedProperty field = _editor.serializedObject.GetIterator ();

        field.NextVisible (true);

        if (SHOW_SCRIPT_FIELD)
        {
            totalHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        while (field.NextVisible (true))
        {
            totalHeight += EditorGUI.GetPropertyHeight (field, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        totalHeight += INNER_SPACING * 2;
        totalHeight += OUTER_SPACING * 2;

        return totalHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        var fieldRect = new Rect (position)
        {
            height = EditorGUIUtility.singleLineHeight
        };

        EditorGUI.PropertyField (fieldRect, property, label, true);

        if (property.objectReferenceValue == null)
        {
            return;
        }

        property.isExpanded = EditorGUI.Foldout (fieldRect, property.isExpanded, GUIContent.none, true);

        if (!property.isExpanded)
            return;

        if (_editor == null)
            Editor.CreateCachedEditor (property.objectReferenceValue, null, ref _editor);

        if (_editor == null)
        {
            Debug.Log ("Couldn't fetch editor");
            return;
        }


        #region Format Field Rects
        List<Rect> propertyRects = new List<Rect> ();
        Rect marchingRect = new Rect (fieldRect);

        Rect bodyRect = new Rect (fieldRect);
        bodyRect.xMin += EditorGUI.indentLevel * 18;
        bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
            + OUTER_SPACING;

        SerializedProperty field = _editor.serializedObject.GetIterator ();
        field.NextVisible (true);

        marchingRect.y += INNER_SPACING + OUTER_SPACING;

        if (SHOW_SCRIPT_FIELD)
        {
            propertyRects.Add (marchingRect);
            marchingRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        while (field.NextVisible (true))
        {
            marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
            marchingRect.height = EditorGUI.GetPropertyHeight (field, true);
            propertyRects.Add (marchingRect);
        }

        marchingRect.y += INNER_SPACING;

        bodyRect.yMax = marchingRect.yMax;
        #endregion

        DrawBackground (bodyRect);

        #region Draw Fields
        EditorGUI.indentLevel++;

        int index = 0;
        field = _editor.serializedObject.GetIterator ();
        field.NextVisible (true);

        if (SHOW_SCRIPT_FIELD)
        {
            //Show the disabled script field
            EditorGUI.BeginDisabledGroup (true);
            EditorGUI.PropertyField (propertyRects[index], field, true);
            EditorGUI.EndDisabledGroup ();
            index++;
        }

        //Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor
        while (field.NextVisible (true))
        {
            try
            {
                EditorGUI.PropertyField (propertyRects[index], field, true);
            }
            catch (StackOverflowException)
            {
                field.objectReferenceValue = null;
                Debug.LogError ("Detected self-nesting causing a StackOverflowException, avoid using the same " +
                    "object inside a nested structure.");
            }

            index++;
        }

        EditorGUI.indentLevel--;
        #endregion
    }

    /// <summary>
    /// Draws the Background
    /// </summary>
    /// <param name="rect">The Rect where the background is drawn.</param>
    private void DrawBackground (Rect rect)
    {
        switch (BACKGROUND_STYLE) {

        case BackgroundStyles.HelpBox:
            EditorGUI.HelpBox (rect, "", MessageType.None);
            break;

        case BackgroundStyles.Darken:
            EditorGUI.DrawRect (rect, Darken_Color);
            break;

        case BackgroundStyles.Lighten:
            EditorGUI.DrawRect (rect, Lighten_Color);
            break;
        }
    }
}

/// <summary>
/// Required for the fetching of a default editor on MonoBehaviour objects.
/// </summary>
[CanEditMultipleObjects]
[CustomEditor(typeof(MonoBehaviour), true)]
public class MonoBehaviourEditor : Editor { }

/// <summary>
/// Required for the fetching of a default editor on ScriptableObject objects.
/// </summary>
[CanEditMultipleObjects]
[CustomEditor(typeof(ScriptableObject), true)]
public class ScriptableObjectEditor : Editor { }
#endif

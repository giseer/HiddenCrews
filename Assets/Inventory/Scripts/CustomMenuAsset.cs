using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class CustomMenuAsset : ScriptableObject 
{

    

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Items/Armas")]
    public static void CreateArmaItem()
    {
        WeaponArm item = CreateInstance<WeaponArm>();
        AssetDatabase.CreateAsset(item, "Assets/NewArmaItem.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }

    [MenuItem("Assets/Create/Items/Productos")]
    public static void CreateProductoItem()
    {
        Products item = CreateInstance<Products>();
        AssetDatabase.CreateAsset(item, "Assets/NewProductoItem.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }
#endif
}

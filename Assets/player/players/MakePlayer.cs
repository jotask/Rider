using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MakePlayer {

    [MenuItem("Game/Player")]
    public static void CreatePlayer()
    {
        Players asset = ScriptableObject.CreateInstance<Players>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

}

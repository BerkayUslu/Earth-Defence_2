using UnityEditor;
using UnityEngine;

namespace snail
{
    static class SearchAssets
    {
        public static T[] SearchAssetsForScriptableObjectInstances<T>() where T: ScriptableObject
        {
            T[] result;
            string[] guids = AssetDatabase.FindAssets("t:"+typeof(T).Name);
            int length = guids.Length;
            if (length == 0)
            {
                Debug.LogError("No ScriptableObject instance is found");
                return null;
            }
            else
            {
                result = new T[length];
            }
            for (int i = 0; i < length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                result[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            return result;
        }
    }

    static class ManageGame
    {
        public static void TogglePause()
        {
            Time.timeScale = Time.timeScale == 1 ? 0f : 1f;
        }
    }

}


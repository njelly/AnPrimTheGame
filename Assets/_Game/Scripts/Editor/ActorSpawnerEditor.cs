using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tofunaut.AnPrimTheGame.Editor
{
    [CustomEditor(typeof(ActorSpawner))]
    [CanEditMultipleObjects]
    public class ActorSpawnerEditor : UnityEditor.Editor
    {
        private SerializedProperty _actorNameProperty;
        private SerializedProperty _actorViewProperty;
        private GameConfig _gameConfigAsset;
        
        private void OnEnable()
        {
            _actorNameProperty = serializedObject.FindProperty("actorName");
            _actorViewProperty = serializedObject.FindProperty("actorViewPrefab");

            _gameConfigAsset = AssetDatabase.FindAssets("t:GameConfig").Select(x =>
                AssetDatabase.LoadAssetAtPath<GameConfig>(AssetDatabase.GUIDToAssetPath(x))).FirstOrDefault();
            
            if(!_gameConfigAsset)
                Debug.LogError("No GameConfig in project!");
        }

        public override void OnInspectorGUI()
        {
            if (!_gameConfigAsset)
                return;

            serializedObject.Update();
            
            var allNames = _gameConfigAsset.config.Actors.Select(x => x.Name);
            var namesArray = allNames as string[] ?? allNames.ToArray();
            var currentIndex = namesArray.Select((x, i) => (x, i)).FirstOrDefault(x => x.x == _actorNameProperty.stringValue).i;
            var selectedIndex = EditorGUILayout.Popup(currentIndex, namesArray);
            _actorNameProperty.stringValue = namesArray[selectedIndex];

            _actorViewProperty.objectReferenceValue =
                EditorGUILayout.ObjectField(_actorViewProperty.objectReferenceValue, typeof(ActorView), true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
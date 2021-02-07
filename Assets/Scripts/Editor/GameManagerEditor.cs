using System;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    GameManager gameManager;
    bool showOperationalMechanics = true;

    void OnEnable()
    {
        gameManager = (GameManager)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        showOperationalMechanics = EditorGUILayout.Foldout(showOperationalMechanics, "Operational Mechanics");
        if (showOperationalMechanics)
        {
            EditorGUI.indentLevel += 1;
            foreach (BreakableMechanic mechanic in Enum.GetValues(typeof(BreakableMechanic)))
            {
                bool isOperational = gameManager.IsMechanicOperationalForInstance(mechanic);
                isOperational = EditorGUILayout.Toggle(mechanic.ToString(), isOperational);
                if (isOperational)
                    gameManager.SetMechanicOperationalForInstance(mechanic);
                else
                    gameManager.SetMechanicBrokenForInstance(mechanic);
            }
            EditorGUI.indentLevel -= 1;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

using UnityEditor;

/// <summary>
/// Custom editor for the Ai class, this exists to make it easier for
/// designers and non-programmers to fill in the unity values correctly
/// Please do not go for references here as it really doesn't do anything more than changes the UI for the panel in Unity
/// By Eric C. Malmerstrom <malmerino> Contact: eric.malmerstrom@gmail.com
/// </summary>
[CustomEditor(typeof(EM_FSM_Ai)), CanEditMultipleObjects]
public class EM_FSM_AiEditor : Editor
{
    /// References from the innerclass EM_FSM_Ai.cs
    /// Only for editor
    public SerializedProperty Player;
    public SerializedProperty EnemyEntityStatisticEnemyType;
    public SerializedProperty EnemyEntityStatisticAttackType;
    public SerializedProperty EnemyEntityStatisticBoundAbility1;
    public SerializedProperty EnemyEntityStatisticBoundAbility2;
    public SerializedProperty EnemyEntityStatisticBoundAbility3;
    public SerializedProperty EnemyEntityStatisticAttackDamage;
    public SerializedProperty EnemyEntityStatisticHealth;
    public SerializedProperty EnemyEntityStatisticHealthRegen;
    public SerializedProperty EnemyEntityStatisticArmor;
    public SerializedProperty EnemyEntityStatisticAttackSpeed;
    public SerializedProperty EnemyEntityStatisticMovementSpeed;
    public SerializedProperty EnemyEntityStatisticEnemyFlag;
    public SerializedProperty EnemyEntityStatisticVisionRange;
    public SerializedProperty EnemyEntityStatisticEnemyVisionAngle;
    public SerializedProperty FsmAiStandardBehaviour;
    public SerializedProperty PatrollCheckpoints;
    public SerializedProperty MaxDistanceFromPath;

    /// <summary>
    /// Loads all the values for the editor
    /// </summary>
    void OnEnable()
    {
        // Creates a link between the visual elements on the panel ui and the inner-class propertys
        Player                                = serializedObject.FindProperty("Player");
        FsmAiStandardBehaviour                = serializedObject.FindProperty("Behaviour");
        PatrollCheckpoints                    = serializedObject.FindProperty("CheckPoints");
        MaxDistanceFromPath                   = serializedObject.FindProperty("MaximumDistance");
        EnemyEntityStatisticEnemyType         = serializedObject.FindProperty("EnemyEntityStatistic.EnemyType");
        EnemyEntityStatisticAttackType        = serializedObject.FindProperty("EnemyEntityStatistic.AttackType");
        EnemyEntityStatisticBoundAbility1     = serializedObject.FindProperty("EnemyEntityStatistic.BoundAbility1");
        EnemyEntityStatisticBoundAbility2     = serializedObject.FindProperty("EnemyEntityStatistic.BoundAbility2");
        EnemyEntityStatisticBoundAbility3     = serializedObject.FindProperty("EnemyEntityStatistic.BoundAbility3");
        EnemyEntityStatisticEnemyFlag         = serializedObject.FindProperty("EnemyEntityStatistic.EnemyFlag");
        EnemyEntityStatisticAttackDamage      = serializedObject.FindProperty("EnemyEntityStatistic.AttackDamage");
        EnemyEntityStatisticHealth            = serializedObject.FindProperty("EnemyEntityStatistic.Health");
        EnemyEntityStatisticHealthRegen       = serializedObject.FindProperty("EnemyEntityStatistic.HealthRegen");
        EnemyEntityStatisticArmor             = serializedObject.FindProperty("EnemyEntityStatistic.Armor");
        EnemyEntityStatisticAttackSpeed       = serializedObject.FindProperty("EnemyEntityStatistic.AttackSpeed");
        EnemyEntityStatisticMovementSpeed     = serializedObject.FindProperty("EnemyEntityStatistic.MovementSpeed");
        EnemyEntityStatisticVisionRange       = serializedObject.FindProperty("EnemyEntityStatistic.VisionRange");
        EnemyEntityStatisticEnemyVisionAngle  = serializedObject.FindProperty("EnemyEntityStatistic.VisionAngle");
    }

    /// <summary>
    /// Applies the UI style to the panel
    /// </summary>
    public override void OnInspectorGUI()
    {

        // Updates the property representation
        serializedObject.Update();

        // Build Ui
        EditorGUILayout.LabelField("Finite State Machine Ai (FSM) by Eric M", EditorStyles.centeredGreyMiniLabel);

        // [SUBPART] General Ai Options
        EditorGUILayout.LabelField("General Ai Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(Player);
        EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyFlag);
        EditorGUILayout.PropertyField(FsmAiStandardBehaviour);
        EditorGUILayout.PropertyField(EnemyEntityStatisticVisionRange);
        EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyVisionAngle);
        EditorGUILayout.LabelField("", EditorStyles.centeredGreyMiniLabel);

        // [SUBPART] Enemy Options (Statistics)
        EditorGUILayout.LabelField("Enemy Options", EditorStyles.boldLabel);

        // Choosable menu that chagnes the menu
          EM_FSM_EnemyEntityStatistic.AiEnemyFlag c1 =
            (EM_FSM_EnemyEntityStatistic.AiEnemyFlag)EnemyEntityStatisticEnemyFlag.enumValueIndex;


        EM_FSM_AiState.FsmAiStandardBehaviour c2 =
            (EM_FSM_AiState.FsmAiStandardBehaviour)FsmAiStandardBehaviour.enumValueIndex;

        switch (c1)
        {
            case EM_FSM_EnemyEntityStatistic.AiEnemyFlag.None:
                EditorGUILayout.LabelField("Enemy Flag: None", EditorStyles.centeredGreyMiniLabel);
                EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyType);
                EditorGUILayout.PropertyField(EnemyEntityStatisticAttackType);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility1);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility2);
                break;
            case EM_FSM_EnemyEntityStatistic.AiEnemyFlag.Boss:
                EditorGUILayout.LabelField("Enemy Flag: Boss", EditorStyles.centeredGreyMiniLabel);
                EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyType);
                EditorGUILayout.PropertyField(EnemyEntityStatisticAttackType);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility1);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility2);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility3);
                break;
        }


        EditorGUILayout.PropertyField(EnemyEntityStatisticAttackDamage);
        EditorGUILayout.PropertyField(EnemyEntityStatisticHealth);
        EditorGUILayout.PropertyField(EnemyEntityStatisticHealthRegen);
        EditorGUILayout.PropertyField(EnemyEntityStatisticArmor);
        EditorGUILayout.PropertyField(EnemyEntityStatisticAttackSpeed);
        EditorGUILayout.PropertyField(EnemyEntityStatisticMovementSpeed);
        EditorGUILayout.LabelField("", EditorStyles.centeredGreyMiniLabel);

        // [SUBPART] Behaviour Settings

        switch (c2)
        {
            case EM_FSM_AiState.FsmAiStandardBehaviour.Guard:
                EditorGUILayout.LabelField("Guard Options", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(MaxDistanceFromPath);
                EditorGUILayout.LabelField("Longest Distance Before calling retreat", EditorStyles.centeredGreyMiniLabel);

                break;

            case EM_FSM_AiState.FsmAiStandardBehaviour.Patrol:
                EditorGUILayout.LabelField("Patrol Options", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(PatrollCheckpoints);
                EditorGUILayout.PropertyField(MaxDistanceFromPath);
                EditorGUILayout.LabelField("Longest Distance Before calling retreat", EditorStyles.centeredGreyMiniLabel);

                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
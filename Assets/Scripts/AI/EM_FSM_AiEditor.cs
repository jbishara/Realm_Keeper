using UnityEditor;

/// <summary>
/// Custom editor for the enemy class, this exists to make it easier for
/// designers and non-programmers to fill in the unity values correctly
/// Please do not go for references here as it really doesn't do anything more than changes the UI for the panel in Unity
/// By Eric C. Malmerstrom <malmerino> Contact: eric.malmerstrom@gmail.com
/// </summary>
[CustomEditor(typeof(EM_FSM_Enemy)), CanEditMultipleObjects]
public class EM_FSM_EnemyEditor : Editor
{
    /// References from the innerclass EM_FSM_Enemy.cs
    /// Only for editor
    public SerializedProperty Player;
    public SerializedProperty EnemyEntityStatisticEnemyType;
    public SerializedProperty EnemyEntityStatisticBoundAbility1;
    public SerializedProperty EnemyEntityStatisticBoundAbility2;
    public SerializedProperty EnemyEntityStatisticBoundAbility3;
    public SerializedProperty EnemyEntityStatisticAttackDamage;
    public SerializedProperty EnemyEntityStatisticHealthRegen;
    public SerializedProperty EnemyEntityStatisticAttackSpeed;
    public SerializedProperty EnemyEntityStatisticMovementSpeed;
    public SerializedProperty EnemyEntityStatisticEnemyFlag;
    public SerializedProperty EnemyEntityStatisticVisionRange;
    public SerializedProperty EnemyEntityStatisticEnemyVisionAngle;
    public SerializedProperty EnemyEntityStatisticEnemyHarmless;
    public SerializedProperty EnemyEntityStatisticClosestDistanceToPlayer;
    public SerializedProperty MaxDistanceFromPath;
    public SerializedProperty AnimatorEnabled;



    /// <summary>
    /// Loads all the values for the editor
    /// </summary>
    void OnEnable()
    {
        // Creates a link between the visual elements on the panel ui and the inner-class propertys
        Player = serializedObject.FindProperty("Player");
        MaxDistanceFromPath = serializedObject.FindProperty("MaximumDistance");
        AnimatorEnabled = serializedObject.FindProperty("UseAnimations");
        EnemyEntityStatisticEnemyType = serializedObject.FindProperty("EnemyEntityStatistic.EnemyType");
        EnemyEntityStatisticBoundAbility1 = serializedObject.FindProperty("EnemyEntityStatistic.BoundAbility1");
        EnemyEntityStatisticBoundAbility2 = serializedObject.FindProperty("EnemyEntityStatistic.BoundAbility2");
        EnemyEntityStatisticBoundAbility3 = serializedObject.FindProperty("EnemyEntityStatistic.BoundAbility3");
        EnemyEntityStatisticEnemyFlag = serializedObject.FindProperty("EnemyEntityStatistic.EnemyFlag");
        EnemyEntityStatisticAttackDamage = serializedObject.FindProperty("EnemyEntityStatistic.AttackDamage");
        EnemyEntityStatisticHealthRegen = serializedObject.FindProperty("EnemyEntityStatistic.HealthRegen");
        EnemyEntityStatisticAttackSpeed = serializedObject.FindProperty("EnemyEntityStatistic.CoolDown");
        EnemyEntityStatisticMovementSpeed = serializedObject.FindProperty("EnemyEntityStatistic.MovementSpeed");
        EnemyEntityStatisticVisionRange = serializedObject.FindProperty("EnemyEntityStatistic.VisionRange");
        EnemyEntityStatisticEnemyVisionAngle = serializedObject.FindProperty("EnemyEntityStatistic.VisionAngle");
        EnemyEntityStatisticEnemyHarmless = serializedObject.FindProperty("EnemyEntityStatistic.IsHarmless");
        EnemyEntityStatisticClosestDistanceToPlayer = serializedObject.FindProperty("EnemyEntityStatistic.ClosestDist2P");

    }

    /// <summary>
    /// Applies the UI style to the panel
    /// </summary>
    public override void OnInspectorGUI()
    {

        // Updates the property representation
        serializedObject.Update();

        // Build Ui
        EditorGUILayout.LabelField("Finite State Machine enemy (FSM) by Eric M", EditorStyles.centeredGreyMiniLabel);

        // [SUBPART] General enemy Options
        EditorGUILayout.LabelField("General enemy Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(Player);
        EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyFlag);
        EditorGUILayout.PropertyField(EnemyEntityStatisticVisionRange);
        EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyVisionAngle);
        EditorGUILayout.PropertyField(EnemyEntityStatisticClosestDistanceToPlayer);
        EditorGUILayout.PropertyField(AnimatorEnabled);
        EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyHarmless);
        EditorGUILayout.LabelField("Closest Dist 2P = Closest Distance To Player", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.LabelField("", EditorStyles.centeredGreyMiniLabel);

        // Menu that changes the menu
        EM_FSM_EnemyEntityStatistic.AiEnemyFlag c1 =
            (EM_FSM_EnemyEntityStatistic.AiEnemyFlag)EnemyEntityStatisticEnemyFlag.enumValueIndex;

        // [SUBPART] Enemy Options (Statistics)
        EditorGUILayout.LabelField("Enemy Options", EditorStyles.boldLabel);



        switch (c1)
        {
            case EM_FSM_EnemyEntityStatistic.AiEnemyFlag.None:
                EditorGUILayout.LabelField("Enemy Flag: None", EditorStyles.centeredGreyMiniLabel);
                EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyType);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility1);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility2);
                break;
            case EM_FSM_EnemyEntityStatistic.AiEnemyFlag.Boss:
                EditorGUILayout.LabelField("Enemy Flag: Boss", EditorStyles.centeredGreyMiniLabel);
                EditorGUILayout.PropertyField(EnemyEntityStatisticEnemyType);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility1);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility2);
                EditorGUILayout.PropertyField(EnemyEntityStatisticBoundAbility3);
                break;
        }

        EditorGUILayout.PropertyField(EnemyEntityStatisticHealthRegen);

        EditorGUILayout.LabelField("↓ ↓ Multiplier stats, 1.0f = Default ↓ ↓", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.PropertyField(EnemyEntityStatisticAttackDamage);
        EditorGUILayout.PropertyField(EnemyEntityStatisticAttackSpeed);
        EditorGUILayout.PropertyField(EnemyEntityStatisticMovementSpeed);
        EditorGUILayout.LabelField("", EditorStyles.centeredGreyMiniLabel);

        // [SUBPART] Behaviour Settings
        EditorGUILayout.LabelField("Guard Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(MaxDistanceFromPath);
        EditorGUILayout.LabelField("Longest Distance Before calling retreat (0 => unlimited)", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.LabelField("", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.LabelField("Components that has to be attached for this to work", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.LabelField("NavMeshAgent and Animator", EditorStyles.centeredGreyMiniLabel);


        // Clamps the Panel Floats to not go less than 0
        MaxDistanceFromPath.ClampThisFloat();
        EnemyEntityStatisticHealthRegen.ClampThisFloat();
        EnemyEntityStatisticAttackDamage.ClampThisFloat();
        EnemyEntityStatisticAttackSpeed.ClampThisFloat();
        EnemyEntityStatisticMovementSpeed.ClampThisFloat();
        EnemyEntityStatisticVisionRange.ClampThisFloat();
        EnemyEntityStatisticEnemyVisionAngle.ClampThisFloat();



        serializedObject.ApplyModifiedProperties();
    }


}

/// <summary>
/// Some quick functions to simplify code
/// </summary>
public static class QuickTools
{
    /// <summary>
    /// Clamps this value from 0 to positive infinity (1/0f)
    /// </summary>
    /// <param name="x"></param>
    public static void ClampThisFloat(this SerializedProperty x)
    {
        if (x.floatValue < 0f) x.floatValue = 0;
    }
}
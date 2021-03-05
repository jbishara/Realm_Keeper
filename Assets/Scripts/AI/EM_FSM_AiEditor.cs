using UnityEditor;
#if UNITY_EDITOR
/// <summary>
/// Custom editor for the enemy class, this exists to make it easier for
/// designers and non-programmers to fill in the unity values correctly
/// Please do not go for references here as it really doesn't do anything more than changes the UI for the panel in Unity
/// By Eric C. Malmerstrom <malmerino> Contact: eric.malmerstrom@gmail.com
/// </summary>
[CustomEditor(typeof(FSM_Enemy)), CanEditMultipleObjects]
public class FSM_EnemyEditor : Editor
{

    /// References from the innerclass FSM_Enemy.cs
    /// Only for editor
    public SerializedProperty EnemyType;
    public SerializedProperty BoundAbility1;
    public SerializedProperty BoundAbility2;
    public SerializedProperty BoundAbility3;
    public SerializedProperty HealthRegen;
    public SerializedProperty AttackSpeed;
    public SerializedProperty MovementSpeed;
    public SerializedProperty EnemyFlag;
    public SerializedProperty VisionRange;
    public SerializedProperty EnemyVisionAngle;
    public SerializedProperty EnemyHarmless;
    public SerializedProperty ClosestDistanceToPlayer;
    public SerializedProperty MaxDistanceFromPath;
    public SerializedProperty AnimatorEnabled;
    public SerializedProperty AbilityInfo1;
    public SerializedProperty AbilityInfo2;
    public SerializedProperty AbilityInfo3;


    /// <summary>
    /// Loads all the values for the editor
    /// </summary>
    void OnEnable()
    {
        // Creates a link between the visual elements on the panel ui and the inner-class property
        MaxDistanceFromPath = serializedObject.FindProperty("MaximumDistance");
        AnimatorEnabled = serializedObject.FindProperty("UseAnimations");
        EnemyType = serializedObject.FindProperty("AiEnemyType");
        BoundAbility1 = serializedObject.FindProperty("BoundAbility1");
        BoundAbility2 = serializedObject.FindProperty("BoundAbility2");
        BoundAbility3 = serializedObject.FindProperty("BoundAbility3");
        EnemyFlag = serializedObject.FindProperty("EnemyFlag");
        HealthRegen = serializedObject.FindProperty("HealthRegen");
        AttackSpeed = serializedObject.FindProperty("CoolDown");
        MovementSpeed = serializedObject.FindProperty("MovementSpeed");
        VisionRange = serializedObject.FindProperty("VisionRange");
        EnemyVisionAngle = serializedObject.FindProperty("VisionAngle");
        EnemyHarmless = serializedObject.FindProperty("IsHarmless");
        ClosestDistanceToPlayer = serializedObject.FindProperty("ClosestDist2P");
        AbilityInfo1 = serializedObject.FindProperty("AbilityInfo1");
        AbilityInfo2 = serializedObject.FindProperty("AbilityInfo2");
        AbilityInfo3 = serializedObject.FindProperty("AbilityInfo3");
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
        EditorGUILayout.PropertyField(EnemyFlag);
        EditorGUILayout.PropertyField(VisionRange);
        EditorGUILayout.PropertyField(EnemyVisionAngle);
        EditorGUILayout.PropertyField(ClosestDistanceToPlayer);
        EditorGUILayout.PropertyField(AnimatorEnabled);
        EditorGUILayout.PropertyField(EnemyHarmless);
        EditorGUILayout.LabelField("Closest Dist 2P = Closest Distance To Player", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.LabelField("", EditorStyles.centeredGreyMiniLabel);

        // Menu that changes the menu
        AiEnemyFlag c1 = (AiEnemyFlag)EnemyFlag.enumValueIndex;

        // [SUBPART] Enemy Options (Statistics)
        EditorGUILayout.LabelField("Enemy Options", EditorStyles.boldLabel);



        switch (c1)
        {
            case AiEnemyFlag.None:
                EditorGUILayout.LabelField("Enemy Flag: None", EditorStyles.centeredGreyMiniLabel);
                EditorGUILayout.PropertyField(EnemyType);

                EditorGUILayout.PropertyField(BoundAbility1);
                EditorGUILayout.PropertyField(AbilityInfo1);

                EditorGUILayout.PropertyField(BoundAbility2);
                EditorGUILayout.PropertyField(AbilityInfo2);
                break;
            case AiEnemyFlag.Boss:
                EditorGUILayout.LabelField("Enemy Flag: Boss", EditorStyles.centeredGreyMiniLabel);
                EditorGUILayout.PropertyField(EnemyType);

                EditorGUILayout.PropertyField(BoundAbility1);
                EditorGUILayout.PropertyField(AbilityInfo1);

                EditorGUILayout.PropertyField(BoundAbility2);
                EditorGUILayout.PropertyField(AbilityInfo2);

                EditorGUILayout.PropertyField(BoundAbility3);
                EditorGUILayout.PropertyField(AbilityInfo3);

                break;
        }

        EditorGUILayout.PropertyField(HealthRegen);

        EditorGUILayout.LabelField("↓ ↓ Multiplier stats, 1.0f = Default ↓ ↓", EditorStyles.centeredGreyMiniLabel);

        EditorGUILayout.PropertyField(AttackSpeed);
        EditorGUILayout.PropertyField(MovementSpeed);
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
        HealthRegen.ClampThisFloat();
        AttackSpeed.ClampThisFloat();
        MovementSpeed.ClampThisFloat();
        VisionRange.ClampThisFloat();
        EnemyVisionAngle.ClampThisFloat();



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
#endif
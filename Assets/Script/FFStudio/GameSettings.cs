/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class GameSettings : ScriptableObject
    {
#region Fields (Settings)
    // Info: You can use Title() attribute ONCE for every game-specific group of settings.

    [ Title( "Player" ) ]
        [ LabelText( "Player Movement Speed Forward" ) ] public float player_movement_speed_forward;
        [ LabelText( "Player Movement Speed Lateral" ) ] public float player_movement_speed_lateral;
        [ LabelText( "Player Movement Clamp Lateral" ) ] public float player_movement_clamp_lateral;
        [ LabelText( "Player Upper Body Offset" ) ] public float player_offset_upper_body = 2f;

    [ Title( "Spring" ) ]
        [ LabelText( "Spring offset from ground" ) ] public float spring_offset_ground;
        [ LabelText( "Spring offset Vertical" ) ] public float spring_offset_vertical;
        [ LabelText( "Spring offset Horizontal" ) ] public Vector2 spring_offset_horizontal;
        [ LabelText( "Spring offset Scale" ) ] public Vector2 spring_offset_scale;
        [ LabelText( "Spring Speed Lateral" ) ] public float spring_speed_lateral; //todo make it vector2 ?:

    [ Title( "Spring Punch Lateral" ) ]
        public float spring_punch_lateral_duration = 0.7f;
        public int spring_punch_lateral_vibrato = 10;
        public float spring_punch_lateral_elasticity = 1f;
		public Ease spring_punch_lateral_ease;
    
    [ Title( "Camera" ) ]
        [ LabelText( "Follow Speed" ) ] public float camera_follow_speed;
        [ LabelText( "Follow Offset Start" ) ] public Vector3 camera_follow_offset_start;
        [ LabelText( "Follow Offset End" ) ] public Vector3 camera_follow_offset_end;
    
    [ Title( "Project Setup", "These settings should not be edited by Level Designer(s).", TitleAlignments.Centered ) ]
        public int maxLevelCount;
        
        // Info: 3 groups below (coming from template project) are foldout by design: They should remain hidden.
		[ FoldoutGroup( "Remote Config" ) ] public bool useRemoteConfig_GameSettings;
        [ FoldoutGroup( "Remote Config" ) ] public bool useRemoteConfig_Components;

        [ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the movement for ui element"          ) ] public float ui_Entity_Move_TweenDuration;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the fading for ui element"            ) ] public float ui_Entity_Fade_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the scaling for ui element"           ) ] public float ui_Entity_Scale_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the movement for floating ui element" ) ] public float ui_Entity_FloatingMove_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Joy Stick"                                        ) ] public float ui_Entity_JoyStick_Gap;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Pop Up Text relative float height"                ) ] public float ui_PopUp_height;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Pop Up Text float duration"                       ) ] public float ui_PopUp_duration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Random Spawn Area in Screen" ), SuffixLabel( "percentage" ) ] public float ui_particle_spawn_width; 
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Spawn Duration" ), SuffixLabel( "seconds" ) ] public float ui_particle_spawn_duration; 
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Spawn Ease" ) ] public Ease ui_particle_spawn_ease;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Wait Time Before Target" ) ] public float ui_particle_target_waitTime;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Target Travel Time" ) ] public float ui_particle_target_duration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Target Travel Ease" ) ] public Ease ui_particle_target_ease;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Percentage of the screen to register a swipe"     ) ] public int swipeThreshold;

        [ FoldoutGroup( "Debug" ) ] public float debug_ui_text_float_height;
        [ FoldoutGroup( "Debug" ) ] public float debug_ui_text_float_duration;
#endregion

#region Fields (Singleton Related)
        static GameSettings instance;

        delegate GameSettings ReturnGameSettings();
        static ReturnGameSettings returnInstance = LoadInstance;

		public static GameSettings Instance => returnInstance();
#endregion

#region Implementation
        static GameSettings LoadInstance()
		{
			if( instance == null )
				instance = Resources.Load< GameSettings >( "game_settings" );

			returnInstance = ReturnInstance;

			return instance;
		}

		static GameSettings ReturnInstance()
        {
            return instance;
        }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
    }
}

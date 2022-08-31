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
        [ LabelText( "Player Pseudo Max Length" ) ] public int player_length_max_pseudo = 30;

    [ Title( "Player - End Level" ) ]
        [ LabelText( "Player Jump Delay" ) ] public float player_jump_delay = 1f;
        [ LabelText( "Player Jump Power" ) ] public Vector2 player_jump_power;
        [ LabelText( "Player Jump Duration" ) ] public Vector2 player_jump_duration;
        [ LabelText( "Player Jump Min Index" ) ] public int player_jump_index_min = 5;
        [ LabelText( "Player Jump Min Index" ) ] public int player_jump_index_max = 40;
        [ LabelText( "Player Width Loss By Index" ) ] public int player_jump_loss_width = 200;
        [ LabelText( "Player Length Loss By Index" ) ] public int player_jump_loss_length = 1;
        [ LabelText( "Platform Offset Vertical" ) ] public float player_jump_offset_vertical = 0.2f;
        [ LabelText( "Platform Offset Step Horizontal" ) ] public float player_jump_offset_step_horizontal = 2f;

    [ Title( "Final Stage - Step" ) ]
        [ LabelText( "Step Start Rotation" ) ] public float finalStage_step_rotation_start = 45f;
        [ LabelText( "Step End Rotation" ) ] public float finalStage_step_rotation_end = 90f;
        [ LabelText( "Step Rotation Duration" ) ] public float finalStage_rotation_duration;
        [ LabelText( "Step Rotation Ease" ) ] public Ease finalStage_rotation_ease;

    [ Title( "Spring" ) ]
        [ LabelText( "Spring Max Width" ) ] public float spring_width_max;
        [ LabelText( "Spring Speed Lateral" ) ] public float spring_speed_lateral;
        [ LabelText( "Spring Spawn Scale" ) ] public float spring_spawn_punch;
        [ LabelText( "Spring Spawn Scale Duration" ) ] public float spring_spawn_punch_duration;
        [ LabelText( "Spring Drop Force" ) ] public float spring_drop_force;
        [ LabelText( "Spring Drop Torque" ) ] public float spring_drop_torque;
        [ LabelText( "Spring Drop Duration" ) ] public float spring_drop_duration;
        [ LabelText( "Spring PopUp Text" ) ] public float spring_ui_popUp_offset;

    [ Title( "Spring Low Count" ) ]
        [ LabelText( "Spring Horizontal Low Count" ) ] public int spring_horizontal_lowCount = 8;
        [ LabelText( "Spring offset Horizontal Low Count" ) ] public float spring_offset_horizontal_lowCount = 0.1f;
    [ Title( "Spring Offset" ) ]
        [ LabelText( "Spring offset from ground" ) ] public float spring_offset_ground;
        [ LabelText( "Spring offset Vertical" ) ] public float spring_offset_vertical;
        [ LabelText( "Spring offset Horizontal" ) ] public Vector2 spring_offset_horizontal;
        [ LabelText( "Spring offset Horizontal Ease" ) ] public Ease spring_offset_horizontal_ease;
        [ LabelText( "Spring offset Scale" ) ] public Vector2 spring_offset_scale;

    [ Title( "Spring Tighten" ) ]
        [ LabelText( "Spring Speed Tighten" ) ] public float spring_speed_tighten = 2f;
        [ LabelText( "Spring Ratio Tighten" ) ] public float spring_offset_tighten = -0.25f;

    [ Title( "Spring Punch Lateral" ) ]
        public float spring_punch_lateral_duration = 0.7f;
        public int spring_punch_lateral_vibrato = 10;
        public float spring_punch_lateral_elasticity = 1f;
		public Ease spring_punch_lateral_ease;

    [ Title( "Spring Punch Vertical Big" ) ]
        public float spring_punch_vertical_big_duration = 1f;
        public int spring_punch_vertical_big_vibrato = 10;
        public float spring_punch_vertical_big_elasticity = 1.25f;
		public Ease spring_punch_vertical_big_ease;
    [ Title( "Spring Punch Vertical Small" ) ]
        public float spring_punch_vertical_small_duration = 0.65f;
        public int spring_punch_vertical_small_vibrato = 8;
        public float spring_punch_vertical_small_elasticity = 1f;
		public Ease spring_punch_vertical_small_ease;
    [ Title( "Spring Punch Vertical Tight" ) ]
		public float spring_punch_vertical_tight_duration = 0.65f;
		public int spring_punch_vertical_tight_vibrato = 8;
		public float spring_punch_vertical_tight_elasticity = 1f;
		public Ease spring_punch_vertical_tight_ease;
		public float spring_punch_vertical_tight_smallest_ratio = 0.1f;
    
    [ Title( "Camera" ) ]
        [ LabelText( "Follow Speed" ) ] public float camera_follow_speed;
        [ LabelText( "Follow Speed End Level" ) ] public float camera_follow_speed_endLevel;
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

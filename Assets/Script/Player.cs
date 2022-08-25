/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;

public class Player : MonoBehaviour
{
#region Fields
  [ Title( "Shared Variables" ) ]
    [ SerializeField ] ColorData shared_player_color;
    [ SerializeField ] PlayerWidth notif_player_width;
    [ SerializeField ] PlayerLength shared_player_length;
    [ SerializeField ] SpringValue shared_spring_value;
    [ SerializeField ] SharedVector2 shared_input_drag;
    [ SerializeField ] SharedVector3 shared_levelEnd_position;
    [ SerializeField ] SharedFloat shared_player_position_delayed;
	[ SerializeField ] PoolSpring pool_spring;

  [ Title( "Components" ) ]
	[ SerializeField ] Transform body_upper_transform;
	[ SerializeField ] Renderer tightSpring_upper_renderer;
	[ SerializeField ] Renderer tightSpring_bottom_renderer;
	[ SerializeField ] Animator body_upper_animator;
	[ SerializeField ] Animator body_bottom_animator;
	[ SerializeField ] ColorSetter body_upper_colorSetter;
	[ SerializeField ] ColorSetter body_bottom_colorSetter;
	[ SerializeField ] ColorSetter tightSpring_upper_colorSetter;
	[ SerializeField ] ColorSetter tightSpring_bottom_colorSetter;

// Private
	List< Spring > spring_list = new List< Spring >( 32 );
	[ ShowInInspector, ReadOnly ] bool is_finger_down; 

    RecycledSequence recycledSequence = new RecycledSequence();
// Delegates
    UnityMessage onUpdateMethod;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		onUpdateMethod = ExtensionMethods.EmptyMethod;

		shared_player_position_delayed.sharedValue = transform.position.x;
		shared_player_length.SetValue_NotifyAlways( 0 );
		shared_player_color.ChangeData( CurrentLevelData.Instance.levelData.player_color_data );
		OnPlayerColorChange();

		tightSpring_upper_renderer.enabled  = false;
		tightSpring_bottom_renderer.enabled = false;
	}

    private void Update()
    {
		onUpdateMethod();
	}
#endregion

#region API
	public void OnFingerDown()
	{
		is_finger_down = true;
	}

	public void OnFingerUp()
	{
		is_finger_down = false;

		// if( shared_spring_value.CanTighten && spring_list.Count > 0 )
			// shared_spring_value.DoTightPunch();
	}

    public void OnLevelStart()
    {
		onUpdateMethod = OnUpdate_Movement;

		body_upper_animator.SetBool( "run", true );
		body_bottom_animator.SetBool( "run", true );
	}

    public void OnFinishLineReached()
    {
		onUpdateMethod = OnUpdate_FinishLine;

		// Level End Sequence
		var sequence = recycledSequence.Recycle( OnEndLevelReached );

		sequence.Append( transform.DOMove( shared_levelEnd_position.sharedValue, 1 ) );
		sequence.Join( transform.DORotate( Vector3.zero, 1 ) );
	}

	public void OnPlayerColorChange()
	{
		foreach( var spring in spring_list )
			spring.OnColorChange( shared_player_color.Color );

		body_upper_colorSetter.SetColor( shared_player_color.Color );
		body_bottom_colorSetter.SetColor( shared_player_color.Color );
		tightSpring_upper_colorSetter.SetColor( shared_player_color.Color );
		tightSpring_bottom_colorSetter.SetColor( shared_player_color.Color );
	}

	public void OnPlayerLength_Gained( IntGameEvent gameEvent )
	{
		var count = gameEvent.eventValue;

		for( var i = 0; i < count; i++ )
		{
			var spring = pool_spring.GetEntity();
			spring.gameObject.SetActive( true );

			Vector3 spawnPosition = spring_list.Count > 0 ? spring_list[ spring_list.Count - 1 ].AttachPoint() : transform.position;

			spring.Spawn( spring_list.Count, spawnPosition, shared_player_color.Color );
			spring_list.Add( spring );
		}

		shared_player_length.SharedValue = spring_list.Count;
		shared_spring_value.DoPunchSmall();

		tightSpring_upper_renderer.enabled  = true;
		tightSpring_bottom_renderer.enabled = true;
	}

	public void OnPlayerLength_Lost( IntGameEvent gameEvent )
	{
		var index = gameEvent.eventValue;

		spring_list[ index ].DropOff();

		for( var i = index; i < spring_list.Count - 1; i++ )
		{
			spring_list[ i ] = spring_list[ i + 1 ];
			spring_list[ i ].FallOff( i );
		}

		spring_list.RemoveAt( spring_list.Count - 1 );

		shared_player_length.SharedValue = spring_list.Count;
		shared_spring_value.DoPunchBig();

		if( spring_list.Count == 0 )
		{
			tightSpring_upper_renderer.enabled  = false;
			tightSpring_bottom_renderer.enabled = false;
		}
	}
#endregion

#region Implementation
    void OnUpdate_Movement()
    {
		MoveForward();
		SetUpperBodyPosition();
		SetPlayerDelayedPosition();

		// if( is_finger_down && shared_spring_value.CanTighten && spring_list.Count > 0 )
		// shared_spring_value.DoTighten();

		//Info: Since LeanTouch executive order is before default time, We can default an input value every frame after use
		shared_input_drag.sharedValue = Vector2.zero;
	}

	void MoveForward()
	{
		var position = transform.position;

		position.x = Mathf.Clamp(
			position.x + shared_input_drag.sharedValue.x * GameSettings.Instance.player_movement_speed_lateral,
			-GameSettings.Instance.player_movement_clamp_lateral,
			GameSettings.Instance.player_movement_clamp_lateral
		);

		position.z += GameSettings.Instance.player_movement_speed_forward * Time.deltaTime;

		transform.position = position;
	}

	void SetUpperBodyPosition()
	{
		body_upper_transform.position = spring_list.Count > 0 ? spring_list[ spring_list.Count - 1 ].AttachPoint() : transform.position + Vector3.up * GameSettings.Instance.player_offset_upper_body;
	}

	void SetPlayerDelayedPosition()
	{
		var position = transform.position;

		var offsetHorizontal = GameSettings.Instance.spring_offset_horizontal.ReturnProgress( notif_player_width.Ratio );
		var offsetHorizontalLowCount = GameSettings.Instance.spring_offset_horizontal_lowCount * ( float )( shared_player_length.sharedValue ) / GameSettings.Instance.spring_horizontal_lowCount;

		var clampValue = shared_player_length.sharedValue < GameSettings.Instance.spring_horizontal_lowCount ? offsetHorizontalLowCount : offsetHorizontal;

		offsetHorizontal = Mathf.Clamp( shared_player_position_delayed.sharedValue - position.x, -clampValue, clampValue );
		offsetHorizontal = Mathf.Lerp( offsetHorizontal, 0, GameSettings.Instance.spring_speed_lateral * Time.deltaTime );

		shared_player_position_delayed.sharedValue = offsetHorizontal + position.x;
	}

	void OnUpdate_FinishLine()
	{
		SetUpperBodyPosition();
		SetPlayerDelayedPosition();
	}

	void OnEndLevelReached()
    {
		body_upper_animator.SetBool( "run", false );
		body_bottom_animator.SetBool( "run", false );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere( Vector3.right * shared_player_position_delayed.sharedValue, 0.1f );
	}
#endif
#endregion
}
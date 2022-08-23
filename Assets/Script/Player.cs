/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour
{
#region Fields
  [ Title( "Shared Variables" ) ]
    [ SerializeField ] ColorData shared_player_color;
    [ SerializeField ] PlayerLength shared_player_length;
    [ SerializeField ] SpringValue shared_spring_value;
    [ SerializeField ] SharedVector2 shared_input_drag;
    [ SerializeField ] SharedVector3 shared_levelEnd_position;
	[ SerializeField ] PoolSpring pool_spring;

  [ Title( "Components" ) ]
	[ SerializeField ] Transform body_upper_transform;
	[ SerializeField ] Renderer tightSpring_upper_renderer;
	[ SerializeField ] Renderer tightSpring_bottom_renderer;
	[ SerializeField ] Animator body_upper_animator;
	[ SerializeField ] Animator body_bottom_animator;
	[ SerializeField ] ColorSetter body_upper_colorSetter;
	[ SerializeField ] ColorSetter body_bottom_colorSetter;

// Private
	List< Spring > spring_list = new List< Spring >( 32 );

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

		// tightSpring_upper_renderer.enabled  = false;
		// tightSpring_bottom_renderer.enabled = false;
	}

    private void Update()
    {
		onUpdateMethod();
	}
#endregion

#region API
    public void OnLevelStart()
    {
		onUpdateMethod = Movement;

		// body_upper_animator.SetTrigger( "run" );
		// body_bottom_animator.SetTrigger( "run" );

		// tightSpring_upper_renderer.enabled  = true;
		// tightSpring_bottom_renderer.enabled = true;
	}

    public void OnFinishLineReached()
    {
		onUpdateMethod = ExtensionMethods.EmptyMethod;

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
	}

	public void OnPlayerLength_Gained()
	{
		var spring = pool_spring.GetEntity();
		spring.gameObject.SetActive( true );

		Vector3 spawnPosition = spring_list.Count > 0 ? spring_list[ spring_list.Count - 1 ].transform.position : transform.position;

		spring.Spawn( spring_list.Count, spawnPosition );
		spring_list.Add( spring );

		shared_player_length.SharedValue = spring_list.Count;
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
		shared_spring_value.DoPunch();

		shared_player_length.SharedValue = spring_list.Count;
	}
#endregion

#region Implementation
    void Movement()
    {
		var position = transform.position;

		position.x = Mathf.Clamp(
			position.x + shared_input_drag.sharedValue.x * GameSettings.Instance.player_movement_speed_lateral,
			-GameSettings.Instance.player_movement_clamp_lateral,
			GameSettings.Instance.player_movement_clamp_lateral
		);

		position.z += GameSettings.Instance.player_movement_speed_forward * Time.deltaTime;

		transform.position = position;
		body_upper_transform.position = spring_list.Count > 0 ? spring_list[ spring_list.Count - 1 ].AttachPoint() : position + Vector3.up * GameSettings.Instance.player_offset_upper_body;

		//Info: Since LeanTouch executive order is before default time, We can default an input value every frame after use
		shared_input_drag.sharedValue = Vector2.zero;
	}

    void OnEndLevelReached()
    {
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Spring : MonoBehaviour
{
#region Fields
  [ Title( "Shared Variables" ) ]
	[ SerializeField ] SharedFloat shared_spring_value; // ( -1, +1 )
	[ SerializeField ] PlayerWidth notif_player_width;
	[ SerializeField ] PlayerLength shared_player_length;
	[ SerializeField ] SharedFloat shared_player_position_delayed;
	[ SerializeField ] SharedReferenceNotifier notif_player_transform;
	[ SerializeField ] PoolSpring pool_spring;
	[ SerializeField ] IntGameEvent event_player_length_lost;

  [ Title( "Components" ) ]
    [ SerializeField ] Collider _collider;
    [ SerializeField ] SkinnedMeshRenderer _skinRenderer;
    [ SerializeField ] ColorSetter colorSetter;

// Private
	[ ShowInInspector, ReadOnly ] int spring_index;
	Transform player_transform;

// Delegates
	UnityMessage onUpdateMethod;
#endregion

#region Properties
#endregion

#region Unity API
	private void Awake()
	{
		onUpdateMethod = ExtensionMethods.EmptyMethod;
	}

	private void OnDisable()
	{
		onUpdateMethod = ExtensionMethods.EmptyMethod;
	}

	private void Update()
	{
		onUpdateMethod();
	}
#endregion

#region API
	public void OnLevelFinish()
	{
		pool_spring.ReturnEntity( this );
	}

	public void Spawn( int index, Vector3 spawnPosition, Color color )
	{
		player_transform   = notif_player_transform.sharedValue as Transform;
		spring_index = index;

		transform.position = spawnPosition;
		_collider.enabled = true;

		OnUpdate();
		OnColorChange( color );
		OnPlayerWidhtChange( 0 );

		transform.localScale = Vector3.one.SetX( GameSettings.Instance.spring_spawn_punch ).SetY( GameSettings.Instance.spring_spawn_punch );
		transform.DOScaleX( 1, GameSettings.Instance.spring_spawn_punch_duration );
		transform.DOScaleZ( 1, GameSettings.Instance.spring_spawn_punch_duration );

		onUpdateMethod = OnUpdate;
	}

	public void OnTrigger()
	{
		event_player_length_lost.Raise( spring_index );
	}

	public void DropOff()
	{
		pool_spring.ReturnEntity( this );
		//todo spawn particle
	}

	public void FallOff( int index )
	{
		spring_index = index;
		OnUpdate();
	}

    public void OnColorChange( Color color )
    {
		colorSetter.SetColor( color );
	}

    public void OnPlayerWidhtChange( float value )
    {
		_skinRenderer.SetBlendShapeWeight( 0, notif_player_width.BlendRatio );
	}

	public Vector3 AttachPoint()
	{
		var scaleChange = shared_spring_value.sharedValue * GameSettings.Instance.spring_offset_scale.ReturnProgress( notif_player_width.Ratio );
		return transform.position + Vector3.up * ( GameSettings.Instance.spring_offset_vertical + GameSettings.Instance.spring_offset_vertical * scaleChange );
	}
#endregion

#region Implementation
	void OnUpdate()
	{
		var scaleChange = shared_spring_value.sharedValue * GameSettings.Instance.spring_offset_scale.ReturnProgress( notif_player_width.Ratio );
		transform.localScale = transform.localScale.SetY( 1 + scaleChange );

		var   playerPosition  = player_transform.position;
		float indexRatio      = ( float )spring_index / ( Mathf.Max( 1, shared_player_length.sharedValue - 1 ) );
		var   indexRatioEased = DOVirtual.EasedValue( 0, 1, indexRatio, GameSettings.Instance.spring_offset_horizontal_ease );

		var offsetHorizontal = ( shared_player_position_delayed.sharedValue - playerPosition.x ) * indexRatioEased;

		var offsetVertical = GameSettings.Instance.spring_offset_vertical * spring_index + GameSettings.Instance.spring_offset_vertical * scaleChange * spring_index + GameSettings.Instance.spring_offset_ground;

		transform.position = playerPosition + offsetVertical * Vector3.up + offsetHorizontal * Vector3.right;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
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
	[ SerializeField ] Pool_UIPopUpText pool_ui_popUpText;

  [ Title( "Fired Events" ) ]
	[ SerializeField ] IntGameEvent event_player_length_lost;
	[ SerializeField ] ParticleSpawnEvent event_particle_spawn;

  [ Title( "Components" ) ]
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] Collider _collider;
    [ SerializeField ] SkinnedMeshRenderer _skinRenderer;
    [ SerializeField ] ColorSetter colorSetter;

// Private
	[ ShowInInspector, ReadOnly ] int spring_index;
	Transform player_transform;

	RecycledSequence recycledSequence = new RecycledSequence();
	RecycledTween    recycledTween    = new RecycledTween();

	// Delegates
	UnityMessage onUpdateMethod;
#endregion

#region Properties
#endregion

#region Unity API
	private void Awake()
	{
		onUpdateMethod = ExtensionMethods.EmptyMethod;

		_rigidbody.isKinematic = true;
		_rigidbody.useGravity  = false;
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
		recycledTween.Kill();
		OnDropDone();
	}

	public void Spawn( int index, Vector3 spawnPosition, Color color )
	{
		player_transform   = notif_player_transform.sharedValue as Transform;
		spring_index = index;

		transform.position = spawnPosition;
		transform.rotation = Quaternion.identity;
		_collider.enabled  = true;

		_rigidbody.isKinematic = true;
		_rigidbody.useGravity  = false;

		var scale = Vector3.one.SetX( GameSettings.Instance.spring_spawn_punch ).SetY( GameSettings.Instance.spring_spawn_punch );
		transform.localScale = scale;

		OnUpdate( scale );
		OnColorChange( color );
		OnPlayerWidhtChange( 0 );

		pool_ui_popUpText.GetEntity().Spawn( transform.position + Vector3.right * GameSettings.Instance.spring_ui_popUp_offset,
		"+1", 2, Color.white, player_transform );

		var sequence = recycledSequence.Recycle( OnSpawnScaleDone );

		sequence.Append( transform.DOScaleX( 1, GameSettings.Instance.spring_spawn_punch_duration ) );
		sequence.Join( transform.DOScaleZ( 1, GameSettings.Instance.spring_spawn_punch_duration ) );

		onUpdateMethod = OnUpdate;

		// event_particle_spawn.Raise( "spring_gained", spawnPosition, player_transform );
	}

	public void OnTrigger()
	{
		event_player_length_lost.Raise( spring_index );
	}

	public void DropOff()
	{
		onUpdateMethod = ExtensionMethods.EmptyMethod;

		_rigidbody.isKinematic = false;
		_rigidbody.useGravity  = true;

		_rigidbody.AddForce( ( Random.onUnitSphere + Vector3.back + Vector3.up ).normalized * GameSettings.Instance.spring_drop_force, ForceMode.Impulse );
		_rigidbody.AddTorque( Random.onUnitSphere * GameSettings.Instance.spring_drop_force, ForceMode.Impulse );

		recycledTween.Recycle( DOVirtual.DelayedCall( GameSettings.Instance.spring_drop_duration, OnDropDone ) );
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
	void OnSpawnScaleDone()
	{
		transform.localScale = transform.localScale.SetX( 1 ).SetZ( 1 );
	}

	void OnDropDone()
	{
		_rigidbody.velocity    = Vector3.zero;
		_rigidbody.isKinematic = true;
		_rigidbody.useGravity  = false;

		pool_spring.ReturnEntity( this );
	}

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

	void OnUpdate( Vector3 scale )
	{
		var scaleChange = shared_spring_value.sharedValue * GameSettings.Instance.spring_offset_scale.ReturnProgress( notif_player_width.Ratio );
		transform.localScale = scale.SetY( 1 + scaleChange );

		var playerPosition = player_transform.position;
		float indexRatio = ( float )spring_index / ( Mathf.Max( 1, shared_player_length.sharedValue - 1 ) );
		var indexRatioEased = DOVirtual.EasedValue( 0, 1, indexRatio, GameSettings.Instance.spring_offset_horizontal_ease );

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
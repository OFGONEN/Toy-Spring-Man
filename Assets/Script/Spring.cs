/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Spring : MonoBehaviour
{
#region Fields
  [ Title( "Shared Variables" ) ]
	[ SerializeField ] SharedFloat shared_spring_value; // ( -1, +1 )
	[ SerializeField ] PlayerWidth notif_player_width;
	[ SerializeField ] SharedReferenceNotifier notif_player_transform;
	[ SerializeField ] PoolSpring pool_spring;

  [ Title( "Components" ) ]
    [ SerializeField ] SkinnedMeshRenderer _skinRenderer;
    [ SerializeField ] ColorSetter colorSetter;

// Private
	Transform player_transform;
	int spring_index;

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
	public void Spawn( int index, Vector3 spawnPosition )
	{
		spring_index = index;

		var scaleChange = shared_spring_value.sharedValue * GameSettings.Instance.spring_offset_scale.ReturnProgress( notif_player_width.Ratio );
		transform.localScale = Vector3.one.SetY( 1 + scaleChange );

		player_transform   = notif_player_transform.sharedValue as Transform;
		transform.position = spawnPosition + Vector3.up * GameSettings.Instance.spring_offset_vertical * ( 1 + scaleChange * index );

		onUpdateMethod = OnUpdate;
	}

    public void OnColorChange( Color color )
    {
		colorSetter.SetColor( color );
	}

    public void OnPlayerWidhtChange( float value )
    {
		_skinRenderer.SetBlendShapeWeight( 0, value );
	}
#endregion

#region Implementation
	void OnUpdate()
	{
		var scaleChange = shared_spring_value.sharedValue * GameSettings.Instance.spring_offset_scale.ReturnProgress( notif_player_width.Ratio );
		transform.localScale = Vector3.one.SetY( 1 + scaleChange );

		var playerPosition = player_transform.position;

		var offsetHorizontal = GameSettings.Instance.spring_offset_horizontal.ReturnProgress( notif_player_width.Ratio );
		    offsetHorizontal = Mathf.Clamp( transform.position.x - playerPosition.x, -offsetHorizontal, offsetHorizontal );
		    offsetHorizontal = Mathf.Lerp( offsetHorizontal, 0, GameSettings.Instance.spring_speed_lateral );

		var offsetVertical = ( GameSettings.Instance.spring_offset_vertical * index + GameSettings.Instance.spring_offset_vertical * scaleChange * index );

		transform.position = playerPosition + offsetVertical * Vector3.up + offsetHorizontal * Vector3.right;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
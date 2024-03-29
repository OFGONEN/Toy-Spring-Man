/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "property_player_width", menuName = "FF/Game/Property/Player Width" ) ]
public class PlayerWidth : SharedFloatNotifier
{
	RecycledTween recycledTween = new RecycledTween();

	Vector3 punch;
	float baseValue;

	public float Ratio      => sharedValue / GameSettings.Instance.spring_width_max;
	public float BlendRatio => sharedValue * 100f / GameSettings.Instance.spring_width_max;

	[ Button() ]
	public void Add( float value )
	{
		SharedValue = Mathf.Min( GameSettings.Instance.spring_width_max, sharedValue + value );
		Punch();
	}

	[ Button() ]
	public void Substact( float value )
	{
		SharedValue = Mathf.Max( 0, sharedValue - value );
		Punch();
	}

	[ Button() ]
	void Punch()
	{
		punch     = Vector3.zero;
		baseValue = sharedValue;

		recycledTween.Recycle( DOTween.Punch( GetPunch, SetPunch, Vector3.up,
			GameSettings.Instance.spring_punch_lateral_duration,
			GameSettings.Instance.spring_punch_lateral_vibrato,
			GameSettings.Instance.spring_punch_lateral_elasticity )
			.OnUpdate( OnPunchUpdate )
		);
	}

	void OnPunchUpdate()
	{
		SharedValue = baseValue + baseValue * punch.y;
	}

	Vector3 GetPunch()
	{
		return punch;
	}

	void SetPunch( Vector3 value )
	{
		punch = value;
	}
}
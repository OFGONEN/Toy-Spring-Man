/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "shared_spring_value", menuName = "FF/Game/Property/Spring Value" ) ]
public class SpringValue : SharedFloat
{
#region Fields
// Private
    Vector3 punch;

	RecycledTween recycledTween = new RecycledTween();

	public bool CanTighten => !recycledTween.IsPlaying();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void DoPunchBig()
    {
		sharedValue = 0;
		punch = Vector3.zero;

		recycledTween.Recycle( DOTween.Punch( GetPunchValue, SetPunchValue, Vector3.up,
			GameSettings.Instance.spring_punch_vertical_big_duration,
			GameSettings.Instance.spring_punch_vertical_big_vibrato,
			GameSettings.Instance.spring_punch_vertical_big_elasticity )
			.OnUpdate( OnPunchUpdate ) 
		);
	}

    [ Button() ]
    public void DoPunchSmall()
    {
		sharedValue = 0;
		punch = Vector3.zero;

		recycledTween.Recycle( DOTween.Punch( GetPunchValue, SetPunchValue, Vector3.up,
			GameSettings.Instance.spring_punch_vertical_small_duration,
			GameSettings.Instance.spring_punch_vertical_small_vibrato,
			GameSettings.Instance.spring_punch_vertical_small_elasticity )
			.OnUpdate( OnPunchUpdate ) 
		);
	}

	public void DoTighten()
	{
		sharedValue = Mathf.Lerp( sharedValue, GameSettings.Instance.spring_offset_tighten, Time.deltaTime * GameSettings.Instance.spring_speed_tighten );
	}

	public void DoTightPunch()
	{
		sharedValue = 0;
		punch = Vector3.zero;

		var duration   = Mathf.Lerp( GameSettings.Instance.spring_punch_vertical_tight_duration * GameSettings.Instance.spring_punch_vertical_tight_smallest_ratio, GameSettings.Instance.spring_punch_vertical_tight_duration, Mathf.Abs( sharedValue ) );

		var vibrato    = Mathf.FloorToInt( Mathf.Lerp( GameSettings.Instance.spring_punch_vertical_tight_vibrato * GameSettings.Instance.spring_punch_vertical_tight_smallest_ratio, GameSettings.Instance.spring_punch_vertical_tight_vibrato, Mathf.Abs( sharedValue ) ) );

		var elasticity = Mathf.Lerp( GameSettings.Instance.spring_punch_vertical_tight_elasticity * GameSettings.Instance.spring_punch_vertical_tight_smallest_ratio, GameSettings.Instance.spring_punch_vertical_tight_elasticity, Mathf.Abs( sharedValue ) );

		recycledTween.Recycle( DOTween.Punch( GetPunchValue, SetPunchValue, Vector3.up,
			duration,
			vibrato,
			elasticity )
			.OnUpdate( OnPunchUpdate )
		);
	}
#endregion

#region Implementation
	void OnPunchUpdate()
	{
		sharedValue = punch.y;
	}

	Vector3 GetPunchValue()
	{
		return punch;
	}

	void SetPunchValue( Vector3 value )
	{
		punch = value;
	}

	float GetSpringValue()
	{
		return sharedValue;
	}

	void SetSpringValue( float value )
	{
		sharedValue = value;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
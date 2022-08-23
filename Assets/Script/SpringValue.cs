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
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
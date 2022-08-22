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
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    Vector3 punch;

    [ Button() ]
    public void DoPunch()
    {
		sharedValue = 0;
		punch = Vector3.one * sharedValue;
		DOTween.Punch( GetPunchValue, SetPunchValue, Vector3.up, 2 ).OnUpdate( OnPunchUpdate );
	}

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
#endif
#endregion
}

/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TestMovementVertical : MonoBehaviour
{
#region Fields
    [ SerializeField ] SharedFloat springValue;
    [ SerializeField ] int positionIndex;

    Vector3 position;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		springValue.sharedValue = 0;
		position = transform.position;
	}
#endregion

#region API
    private void Update()
    {
		transform.localScale = new Vector3( 1, 1 + springValue.sharedValue / 4, 1 );
        transform.position = position + Vector3.up * springValue.sharedValue / 2 * positionIndex;
	}
#endregion

#region Implementation
    [ Button() ]
    public void DoSpringTweenNegative()
    {
		DOTween.To( GetSpringValue, SetSpringValue, -1, 0.5f ).OnComplete( DoSpringTweenPositive );
	}

    [ Button() ]
	public void DoSpringTweenPositive()
	{
		DOTween.To( GetSpringValue, SetSpringValue, 1, 0.5f ).OnComplete( DoSpringTweenNegative );
	}
    
    float GetSpringValue()
    {
		return springValue.sharedValue;
	}

    void SetSpringValue( float value )
    {
		springValue.sharedValue = value;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
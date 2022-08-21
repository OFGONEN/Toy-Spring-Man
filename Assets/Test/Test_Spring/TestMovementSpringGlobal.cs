/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TestMovementSpringGlobal : MonoBehaviour
{
#region Fields
    [ SerializeField ] Transform target;
    [ SerializeField ] SharedFloat springValue;
    [ SerializeField ] int positionIndex;
    [ SerializeField ] int springCount;
    [ SerializeField ] float offset_vertical;
    [ SerializeField ] float offset_horizontal;
    [ SerializeField ] float scaleValue;
	[ SerializeField ] float followSpeed;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		springValue.sharedValue = 0;
	}
    private void Update()
    {
		var targetPosition = target.position;
		var position = transform.position;

		var scaleChange = springValue.sharedValue * scaleValue;
		var offset = (float)positionIndex / springCount * offset_horizontal;

		var distace = transform.position.x - targetPosition.x;
		distace = Mathf.Clamp( distace, -offset, offset );


		var positionHorizontal = targetPosition.x + distace;
		positionHorizontal = Mathf.Lerp( positionHorizontal, targetPosition.x, followSpeed * Time.deltaTime );

		var positionVertical = positionIndex * offset_vertical + offset_vertical * scaleChange * positionIndex;

		transform.position   = Vector3.up * positionVertical + Vector3.right * positionHorizontal;
		transform.localScale = Vector3.one.SetY( 1 + scaleChange );
	}
#endregion

#region API
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
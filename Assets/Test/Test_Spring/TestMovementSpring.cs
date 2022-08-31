/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TestMovementSpring : MonoBehaviour
{
#region Fields
    [ SerializeField ] Transform[] springs;
    [ SerializeField ] SharedFloat springValue;
    [ SerializeField ] float offset_vertical;
    [ SerializeField ] float offset_horizontal;
    [ SerializeField ] float scaleValue;
	[ SerializeField ] float followSpeed;

    Vector3 lastPosition;
#endregion

#region Properties
    private void Start()
    {
		lastPosition = transform.position;
        
		springValue.sharedValue = 0;
	}
#endregion

#region Unity API
    // private void Foo()
    // {
	// 	var position = transform.position;

	// 	for( var i = 0; i < springs.Length; i++ )
    //     {
	// 		var spring = springs[ i ];

	// 		var scaleChange = springValue.sharedValue * scaleValue;
	// 		var positionY = position.y + i * offset_vertical + offset_vertical * scaleChange * i;

    //         var offsetHorizontal = (float) i / springs.Length * offset_horizontal;
	// 		var positionX = lastPosition[ i ].x;

	// 		var sign = position.x - positionX;
	// 		positionX = position.x + sign * Mathf.Min( offsetHorizontal, Mathf.Abs( position.x - positionX ) );
	// 		positionX = Mathf.Lerp( positionX, position.x, followSpeed * Time.deltaTime );

	// 		spring.localScale    = new Vector3( 1, 1 + scaleChange , 1 );
	// 		spring.position = Vector3.up * positionY + Vector3.right * positionX;
	// 	}

	// 	for( var i = 0; i < springs.Length; i++ )
	// 		lastPositions[ i ] = springs[ i ].position;
    // }

    // private void Foo2()
    // {
	// 	var parentPosition = transform.position;

	// 	for( var i = 0; i < springs.Length; i++ )
    //     {
	// 		var currentPosition = springs[ i ].position;
	// 		var lastPosition = this.lastPosition[ i ];

	// 		var scaleChange = springValue.sharedValue * scaleValue;
	// 		var offset = ( float )i / springs.Length * offset_horizontal;

	// 		var distance = parentPosition.x - lastPosition.x;
	// 		var sign = Mathf.Sign( distance );
	// 		offset = Mathf.Min( offset_horizontal, Mathf.Abs( distance ) ) * sign;

	// 		var positionHorizontal = parentPosition.x + offset;
	// 		// positionHorizontal = Mathf.Lerp( positionHorizontal, parentPosition.x, followSpeed * Time.deltaTime );

	// 		var position_Vertical = parentPosition.y + i * offset_vertical + offset_vertical * scaleChange * i;

	// 		springs[ i ].position = Vector3.up * position_Vertical + Vector3.right * positionHorizontal;
	// 		springs[ i ].localScale = Vector3.one.SetY( 1 + scaleChange );
	// 	}

		// for( var i = 0; i < springs.Length; i++ )
			// lastPositions[ i ] = springs[ i ].position;
	// }

    private void Update()
    {
		var parentPosition = transform.position;

		for( var i = 0; i < springs.Length; i++ )
        {
			var spring = springs[ i ];
			var scaleChange = springValue.sharedValue * scaleValue;
			var offset = ( float )i / springs.Length * offset_horizontal;

			var distace = lastPosition.x - parentPosition.x;

			var positionHorizontal = Mathf.Clamp( distace, -offset, offset );
			positionHorizontal = Mathf.Lerp( positionHorizontal, 0, followSpeed * Time.deltaTime );

			var positionVertical = i * offset_vertical + offset_vertical * scaleChange * i;

			spring.localPosition = Vector3.up * positionVertical + Vector3.right * positionHorizontal;
			spring.localScale = Vector3.one.SetY( 1 + scaleChange );
        }

		lastPosition = transform.position;
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
    [ Button() ]
    public void CacheChildren()
    {
		springs = new Transform[ transform.childCount ];

        for( var i = 0; i < transform.childCount; i++ )
        {
			springs[ i ] = transform.GetChild( i );
		}
    }
#endif
#endregion
}
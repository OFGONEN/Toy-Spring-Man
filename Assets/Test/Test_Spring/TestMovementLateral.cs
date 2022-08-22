/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class TestMovementLateral : MonoBehaviour
{
#region Fields
	[ SerializeField ] Transform target;
	[ SerializeField ] float followSpeed;
	[ SerializeField ] float maxOffSet;
#endregion

#region Properties
#endregion

#region Unity API
	private void Update()
	{
		var position       = transform.position;
		var targetPosition = target.position;

		var distance = position.x - targetPosition.x;

		var offset = Mathf.Min( maxOffSet, Mathf.Abs( distance ) );

		position.x = targetPosition.x + offset * Mathf.Sign( distance );

		position.x = Mathf.Lerp( position.x, targetPosition.x, followSpeed * Time.deltaTime );
		transform.position = position;
	}
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}

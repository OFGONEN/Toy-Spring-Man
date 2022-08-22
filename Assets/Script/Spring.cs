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
	int index;

// Delegates
	UnityMessage onUpdateMethod;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
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
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}

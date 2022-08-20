/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "property_player_length", menuName = "FF/Game/Property/Player Length" ) ]
public class PlayerLength : SharedIntNotifier
{
    public void Add( int value )
    {
		SharedValue += value;
	}

    public void Substact( int value )
    {
		SharedValue = Mathf.Max( 0, sharedValue - value );
	}
}

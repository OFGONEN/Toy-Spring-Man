/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "property_player_width", menuName = "FF/Game/Property/Player Width" ) ]
public class PlayerWidth : SharedFloatNotifier
{
	public void Add( float value )
	{
		SharedValue += value;
	}

	public void Substact( float value )
	{
		SharedValue -= value;
	}
}
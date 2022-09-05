/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "event_spring_gained", menuName = "FF/Game/Spring GameEvent" ) ]
public class SpringGameEvent : IntGameEvent
{
	public ColorData eventValue_ColorData;

    public void Raise( int value, ColorData data )
    {
		eventValue           = value;
		eventValue_ColorData = data;

		Raise();
	}
}

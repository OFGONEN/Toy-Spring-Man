/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using FFStudio;
using TMPro;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "tool_level_creator", menuName = "FFEditor/Tool/Level Creator" ) ]
public class LevelCreator : ScriptableObject
{
  [ Title( "Setup Ground" ) ]
    [ SerializeField ] int ground_count;

  [ Title( "Setup Collectable" ) ]
    [ SerializeField ] Transform collectable_origin;
    [ SerializeField ] int collectable_count;
    [ SerializeField ] CollectableType collectable_type;

  [ Title( "Setup - Final Stage" ) ]
    [ SerializeField ] Transform finalStage_transform;
	[ SerializeField ] int finalStage_step_count; 
	[ SerializeField ] Color[] finalStage_color; 

  [ Title( "Data Setup" ) ]
    [ SerializeField ] FinalStageData data_finalStage;
    [ SerializeField ] FinishLineData data_finishLine;
    [ SerializeField ] GroundData data_ground;
    [ SerializeField ] StepPlatformData data_stepPlatform; 
    [ SerializeField ] CollectableData data_collectable; 
    // [ SerializeField ] FinalStageData data_finalStage;

	[ Button() ]
	public void PlaceCollectableLine()
	{
		EditorSceneManager.MarkAllScenesDirty();
		var collectableParent = GameObject.Find( "collectables" ).transform;

		for( var i = 0; i < collectable_count; i++ )
		{
			var collectable = data_collectable.ReturnCollectable( collectable_type ).transform;

			collectable.SetParent( collectableParent );
			collectable.position = collectable_origin.position + Vector3.forward * i * data_collectable.collectable_offset;
		}

	    AssetDatabase.SaveAssets();
	}

	[ Button() ]
	public void PlaceCollectableDiagonal( float sign, float offset )
	{
		EditorSceneManager.MarkAllScenesDirty();
		var collectableParent = GameObject.Find( "collectables" ).transform;

		for( var i = 0; i < collectable_count; i++ )
		{
			var collectable = data_collectable.ReturnCollectable( collectable_type ).transform;

			collectable.SetParent( collectableParent );
			collectable.position = collectable_origin.position + 
				Vector3.forward * i * data_collectable.collectable_offset +
				Vector3.right * sign * i * offset;
		}

		AssetDatabase.SaveAssets();
	}

	[ Button() ]
	public void PlaceCollectable()
	{
		EditorSceneManager.MarkAllScenesDirty();
		var collectableParent = GameObject.Find( "collectables" ).transform;

		var collectable = data_collectable.ReturnCollectable( collectable_type ).transform;
			collectable.SetParent( collectableParent );
			collectable.position = collectable_origin.position;

	    AssetDatabase.SaveAssets();
	}

    [ Button() ]
    public void CreateEnvironment()
    {
		var environmentParent = GameObject.Find( "environment" ).transform;
   
		// EditorUtility.SetDirty( environmentParent );
		EditorSceneManager.MarkAllScenesDirty();
		environmentParent.DestoryAllChildren();

		int i;
		for( i = 0; i < ground_count - 1; i++ )
        {
			var ground = PrefabUtility.InstantiatePrefab( data_ground.ground_object ) as GameObject;
			ground.name = "ground_" + ( i + 1 );
			ground.transform.SetParent( environmentParent );
			ground.transform.localPosition = Vector3.forward * i * data_ground.ground_length;
		}

		var finishLine = PrefabUtility.InstantiatePrefab( data_finishLine.finishLine_object ) as GameObject;
		finishLine.transform.SetParent( environmentParent );
        finishLine.transform.localPosition = Vector3.forward * ( i * data_ground.ground_length );

		var finalStage = PrefabUtility.InstantiatePrefab( data_finalStage.finalStage_object ) as GameObject;
		finalStage.transform.SetParent( environmentParent );
        finalStage.transform.localPosition = Vector3.forward * ( i * data_ground.ground_length + data_finishLine.finishLine_offset + data_finalStage.finalStage_offset );

	    AssetDatabase.SaveAssets();
	}

	[ Button() ]
	public void EditFinalStage()
	{
		EditorSceneManager.MarkAllScenesDirty();

		var finalStageParent = GameObject.Find( "finalStage" ).transform;
		// finalStageParent.DestoryAllChildren();

		int score             = 10;
		int colorPortionCount = finalStage_step_count / ( finalStage_color.Length - 1 );

		for( var i = 0; i < finalStage_step_count; i++ )
		{
			var finalStage = PrefabUtility.InstantiatePrefab( data_stepPlatform.stepPlatform_object ) as GameObject;
			finalStage.transform.SetParent( finalStageParent );
			finalStage.transform.localPosition = Vector3.forward * ( i * data_stepPlatform.stepPlatform_offset );
			finalStage.transform.localEulerAngles = Vector3.right * data_stepPlatform.stepPlatform_rotate;
			finalStage.name = data_stepPlatform.stepPlatform_object.name + "_" + i;

			finalStage.GetComponentInChildren< TextMeshProUGUI >().text = "x" + ( score / 10f );
			score += 1;

			var colorIndex = i / colorPortionCount;
			var color = Color.Lerp( finalStage_color[ colorIndex ],
				finalStage_color[ Mathf.Min( colorIndex + 1, finalStage_color.Length ) ],
				( float ) ( i % colorPortionCount ) / colorPortionCount
			);

			finalStage.GetComponentInChildren< ColorSetter >().SetColorInEditor( color, true );
		}

	    AssetDatabase.SaveAssets();
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		for( var i = 0; i < finalStage_color.Length; i++ )
			finalStage_color[ i ] = finalStage_color[ i ].SetAlpha( 1 );
	}
#endif
}

[ Serializable ]
public struct GroundData
{
	public GameObject ground_object;
    public float ground_length;
}

[ Serializable ]
public struct FinishLineData
{
	public GameObject finishLine_object;
	public float finishLine_offset;
}

[ Serializable ]
public struct FinalStageData
{
	public GameObject finalStage_object;
    public float finalStage_offset;
}

[ Serializable ]
public struct StepPlatformData
{
	public GameObject stepPlatform_object;
    public float stepPlatform_offset;
    public float stepPlatform_rotate;
}

[Serializable]
public struct CollectableData
{
	[ SerializeField ] GameObject[] collectable_object_array;
	public float collectable_offset;

	public GameObject ReturnCollectable( CollectableType type )
	{
		return PrefabUtility.InstantiatePrefab( collectable_object_array[ ( int )type ] ) as GameObject;
	}
}

public enum CollectableType
{
	Blue,
	Green,
	Orange
}
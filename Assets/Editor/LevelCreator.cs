/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "tool_level_creator", menuName = "FFEditor/Tool/Level Creator" ) ]
public class LevelCreator : ScriptableObject
{
  [ Title( "Environment Setup" ) ]
    [ SerializeField ] public int ground_count;

  [ Title( "Data Setup" ) ]
    [ SerializeField ] GameObject object_finishLine;
    [ SerializeField ] GroundData data_ground;
    // [ SerializeField ] FinalStageData data_finalStage;

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

		var finishLine = PrefabUtility.InstantiatePrefab( object_finishLine ) as GameObject;
		finishLine.transform.SetParent( environmentParent );
        finishLine.transform.localPosition = Vector3.forward * ( i * data_ground.ground_length );

	    AssetDatabase.SaveAssets();
	}
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
public struct EndLevelData
{
	public GameObject finalStage_object;
    public float finalStage_offset;
}
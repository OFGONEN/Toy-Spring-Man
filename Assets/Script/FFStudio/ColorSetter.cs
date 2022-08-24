/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class ColorSetter : MonoBehaviour
	{
#region Fields
    [ Title( "Setup" ) ]
		[ SerializeField ] Color color;
		[ SerializeField ] Renderer _renderer;

		static int SHADER_ID_COLOR = Shader.PropertyToID( "_BaseColor" );

		MaterialPropertyBlock propertyBlock;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			propertyBlock = new MaterialPropertyBlock();
		}
#endregion

#region API
		public void SetColor( Color color )
		{
			this.color = color;

			SetColor();
		}

		[ Button ]
		public void SetColor() // Info: This may be more "Unity-Event-friendly".
		{
			_renderer.GetPropertyBlock( propertyBlock );
			propertyBlock.SetColor( SHADER_ID_COLOR, color );
			_renderer.SetPropertyBlock( propertyBlock );
		}
		
		public void SetAlpha( float alpha )
		{
			_renderer.GetPropertyBlock( propertyBlock );
			var currentColor = _renderer.sharedMaterial.GetColor( SHADER_ID_COLOR );
			propertyBlock.SetColor( SHADER_ID_COLOR, currentColor.SetAlpha( alpha ) );
			_renderer.SetPropertyBlock( propertyBlock );
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}
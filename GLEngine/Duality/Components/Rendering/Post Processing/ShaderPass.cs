﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THREE.Materials;
using THREE.Renderers;
using THREE.Renderers.gl;
using THREE.Renderers.Shaders;

namespace Duality.Postprocessing
{
    public class ShaderPass : Pass
    {
        private string textureId;
        public GLUniforms uniforms;
        private ShaderMaterial material;

        public ShaderPass(Material shader,string textureId=null)
        {
            this.textureId = textureId != null ? textureId : "tDiffuse";
            if(shader!=null && shader is ShaderMaterial)
            {                
                uniforms = (shader as ShaderMaterial).Uniforms;
                if (textureId!=null && !uniforms.ContainsKey(textureId))
                    uniforms[textureId] = new GLUniform { { "value", null } };
                material = shader as ShaderMaterial;
            }

            fullScreenQuad = new FullScreenQuad(this.material);
        }
		public override void Render(GLRenderTarget writeBuffer, GLRenderTarget readBuffer, bool? maskActive = null)
		{
			if (uniforms.ContainsKey(textureId))
			{
				(uniforms[textureId] as GLUniform)["value"] = readBuffer.Texture;
			}

			fullScreenQuad.material = material;
			// TODO: Avoid using autoClear properties, see https://github.com/mrdoob/three.js/pull/15571#issuecomment-465669600
			if (this.Clear) DualityApp.GraphicsBackend.Clear(DualityApp.GraphicsBackend.AutoClearColor, DualityApp.GraphicsBackend.AutoClearDepth, DualityApp.GraphicsBackend.AutoClearStencil);
			this.fullScreenQuad.Render(DualityApp.GraphicsBackend);
		}

        public override void SetSize(float width, float height)
        {
            
        }
    }
}

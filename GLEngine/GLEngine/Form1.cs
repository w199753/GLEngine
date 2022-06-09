
// Copyright (C) 2016-2018 Luca Piccioni
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Windows.Forms;
using System.Text;

using Khronos;
using OpenGL;
using ImGuiNET;
using System.Runtime.InteropServices;

namespace GLEngine
{
	/// <summary>
	/// Sample drawing a simple, rotating and colored triangle.
	/// </summary>
	/// <remarks>
	/// Supports:
	/// - OpenGL 3.2
	/// - OpenGL 1.1/1.0 (deprecated)
	/// - OpenGL ES2
	/// </remarks>
	public partial class Form1 : Form
	{
		#region Constructors

		/// <summary>
		/// Construct a SampleForm.
		/// </summary>
		public Form1()
		{
			InitializeComponent();
		}

		#endregion

		#region Event Handling

		/// <summary>
		/// Allocate GL resources or GL states.
		/// </summary>
		/// <param name="sender">
		/// The <see cref="object"/> that has rasied the event.
		/// </param>
		/// <param name="e">
		/// The <see cref="GlControlEventArgs"/> that specifies the event arguments.
		/// </param>
		private void RenderControl_ContextCreated(object sender, GlControlEventArgs e)
		{
			Gl.Extensions x = new Gl.Extensions();

			var res = Gl.Limits.Query(Gl.CurrentVersion, x);
			var size = res.LineWidthRange;
			Console.WriteLine("fzy hhh:" + size[0] + "   " + size[1] + "    " + (Gl.CurrentVersion >= Gl.Version_320));

			GlControl glControl = (GlControl)sender;

			// GL Debugging
			if (Gl.CurrentExtensions != null && Gl.CurrentExtensions.DebugOutput_ARB)
			{
				Gl.DebugMessageCallback(GLDebugProc, IntPtr.Zero);
				Gl.DebugMessageControl(DebugSource.DontCare, DebugType.DontCare, DebugSeverity.DontCare, 0, null, true);
			}

			// Allocate resources and/or setup GL states
			switch (Gl.CurrentVersion.Api)
			{
				case KhronosVersion.ApiGl:
					if (Gl.CurrentVersion >= Gl.Version_320)
						RenderControl_CreateGL320();
					else
						RenderControl_CreateGL100();
					break;
				case KhronosVersion.ApiGles2:
					RenderControl_CreateGLES2();
					break;
			}
			Init_IMGUI();
			// Uses multisampling, if available
			if (Gl.CurrentVersion != null && Gl.CurrentVersion.Api == KhronosVersion.ApiGl && glControl.MultisampleBits > 0)
				Gl.Enable(EnableCap.Multisample);
		}

		private void RenderControl_Render(object sender, GlControlEventArgs e)
		{
			// Common GL commands
			Control senderControl = (Control)sender;

			Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
			Gl.Clear(ClearBufferMask.ColorBufferBit);

			switch (Gl.CurrentVersion.Api)
			{
				case KhronosVersion.ApiGl:
					if (Gl.CurrentVersion >= Gl.Version_320)
						RenderControl_RenderGL320();
					else if (Gl.CurrentVersion >= Gl.Version_110)
						RenderControl_RenderGL110();
					else
						RenderControl_RenderGL100();
					break;
				case KhronosVersion.ApiGles2:
					RenderControl_RenderGLES2();
					break;
			}
		}

		private void RenderControl_ContextUpdate(object sender, GlControlEventArgs e)
		{
			// Change triangle rotation
			_Angle = (_Angle + 0.1f) % 45.0f;
		}

		#region sample code
		float f = 0.0f;
		int counter = 0;
		private ImFontPtr font;
		private int vboHandle;
		private int vbaHandle;
		private int elementsHandle;
		private int attribLocationTex;
		private int attribLocationProjMtx;
		private int attribLocationVtxPos;
		private int attribLocationVtxUV;
		private int attribLocationVtxColor;
		private int shaderProgram;
		private int shader_vs;
		private int shader_fs;
		private int fontTexture;
		//画面サイズ=GLControlのサイズ
		private System.Numerics.Vector2 displaySize;
		private bool show_demo_window = true;
		private bool show_another_window = false;
		//日本語グリフの文字コードリスト
		private IntPtr japaneseGlyph;

		System.Numerics.Vector3 clear_color = new System.Numerics.Vector3(0.45f, 0.55f, 0.60f);

		#region Initialize

		private void Init_IMGUI()
        {
			ImGui.CreateContext();
			ImGui.StyleColorsLight();
			var io = ImGui.GetIO();
			io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors;         // We can honor GetMouseCursor() values (optional)
			io.BackendFlags |= ImGuiBackendFlags.HasSetMousePos;          // We can honor io.WantSetMousePos requests (optional, rarely used)
			io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;// We can honor the ImDrawCmd::VtxOffset field, allowing for large meshes.
			io.KeyMap[(int)ImGuiKey.Tab] = (int)System.Windows.Forms.Keys.Tab;
			io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)System.Windows.Forms.Keys.Left;
			io.KeyMap[(int)ImGuiKey.RightArrow] = (int)System.Windows.Forms.Keys.Right;
			io.KeyMap[(int)ImGuiKey.UpArrow] = (int)System.Windows.Forms.Keys.Up;
			io.KeyMap[(int)ImGuiKey.DownArrow] = (int)System.Windows.Forms.Keys.Down;
			io.KeyMap[(int)ImGuiKey.PageUp] = (int)System.Windows.Forms.Keys.Prior;
			io.KeyMap[(int)ImGuiKey.PageDown] = (int)System.Windows.Forms.Keys.Next;
			io.KeyMap[(int)ImGuiKey.Home] = (int)System.Windows.Forms.Keys.Home;
			io.KeyMap[(int)ImGuiKey.End] = (int)System.Windows.Forms.Keys.End;
			io.KeyMap[(int)ImGuiKey.Insert] = (int)System.Windows.Forms.Keys.Insert;
			io.KeyMap[(int)ImGuiKey.Delete] = (int)System.Windows.Forms.Keys.Delete;
			io.KeyMap[(int)ImGuiKey.Backspace] = (int)System.Windows.Forms.Keys.Back;
			io.KeyMap[(int)ImGuiKey.Space] = (int)System.Windows.Forms.Keys.Space;
			io.KeyMap[(int)ImGuiKey.Enter] = (int)System.Windows.Forms.Keys.Return;
			io.KeyMap[(int)ImGuiKey.Escape] = (int)System.Windows.Forms.Keys.Escape;
			io.KeyMap[(int)ImGuiKey.A] = (int)System.Windows.Forms.Keys.A;
			io.KeyMap[(int)ImGuiKey.C] = (int)System.Windows.Forms.Keys.C;
			io.KeyMap[(int)ImGuiKey.V] = (int)System.Windows.Forms.Keys.V;
			io.KeyMap[(int)ImGuiKey.X] = (int)System.Windows.Forms.Keys.X;
			io.KeyMap[(int)ImGuiKey.Y] = (int)System.Windows.Forms.Keys.Y;
			io.KeyMap[(int)ImGuiKey.Z] = (int)System.Windows.Forms.Keys.Z;
			displaySize.X = 600;
			displaySize.Y = 600;
			io.DisplaySize = displaySize;
			createDeviceObjects();

			//io.ImeWindowHandle = glc.Handle;
			setStyle();

		}

		private readonly string[] vertex_shader_glsl_440_core = {
"#version 440 core\n",
"layout (location = 0) in vec2 Position;\n",
"layout (location = 1) in vec2 UV;\n",
"layout (location = 2) in vec4 Color;\n",
"layout (location = 10) uniform mat4 ProjMtx;\n",
"out vec2 Frag_UV;\n",
"out vec4 Frag_Color;\n",
"void main()\n",
"{\n",
" Frag_UV = UV;\n",
" Frag_Color = Color;\n",
" gl_Position = ProjMtx * vec4(Position.xy,0,1);\n",
"}\n",
		};

		private readonly string[] fragment_shader_glsl_440_core = {
"#version 440 core\n",
"in vec2 Frag_UV;\n",
"in vec4 Frag_Color;\n",
"layout (location = 20) uniform sampler2D Texture;\n",
"layout (location = 0) out vec4 Out_Color;\n",
"void main()\n",
"{\n",
"   Out_Color = Frag_Color * texture(Texture, Frag_UV.st);\n",
"}\n",
		};

		private void createDeviceObjects()
		{
			int last_texture, last_array_buffer;
			 Gl.GetInteger(GetPName.TextureBinding2d,out last_texture);

			 Gl.GetInteger(GetPName.ArrayBufferBinding,out last_array_buffer);

			//シェーダのコンパイルとリンク
			shader_vs = (int)Gl.CreateShader(ShaderType.VertexShader);
			Gl.ShaderSource((uint)shader_vs, vertex_shader_glsl_440_core);
			Gl.CompileShader((uint)shader_vs);
			StringBuilder sb = new StringBuilder();
			Gl.GetShaderInfoLog((uint)shader_vs,100,out int vs_len,sb);

			if (!string.IsNullOrWhiteSpace(sb.ToString()))
				Console.WriteLine($"GL.CompileShader [VertexShader] had info log: {sb.ToString()}");

			shader_fs = (int)Gl.CreateShader(ShaderType.FragmentShader);
			Gl.ShaderSource((uint)shader_fs, fragment_shader_glsl_440_core);
			Gl.CompileShader((uint)shader_fs);
			Gl.GetShaderInfoLog((uint)shader_fs,100,out int fs_len,sb);
			if (!string.IsNullOrWhiteSpace(sb.ToString()))
				Console.WriteLine($"GL.CompileShader [VertexShader] had info log: {sb.ToString()}");
			shaderProgram = (int)Gl.CreateProgram();
			Gl.AttachShader((uint)shaderProgram, (uint)shader_vs);
			Gl.AttachShader((uint)shaderProgram, (uint)shader_fs);
			Gl.LinkProgram((uint)shaderProgram);
			Gl.GetProgramInfoLog((uint)shaderProgram,100,out int p_len,sb);
			if (!string.IsNullOrWhiteSpace(sb.ToString()))
				Console.WriteLine($"GL.LinkProgram had info log: {sb.ToString()}");

			//ImGuiのサンプルではglGetUniformLocationでLocationを取得しているが
			//OpenGL4はシェーダで直値指定できるので直値で指定してしまう。
			attribLocationTex = 20; //glGetUniformLocation(g_ShaderHandle, "Texture");
			attribLocationProjMtx = 10; //glGetUniformLocation(g_ShaderHandle, "ProjMtx");
			attribLocationVtxPos = 0; // glGetAttribLocation(g_ShaderHandle, "Position");
			attribLocationVtxUV = 1;// glGetAttribLocation(g_ShaderHandle, "UV");
			attribLocationVtxColor = 2;//= glGetAttribLocation(g_ShaderHandle, "Color");
			vboHandle = (int)Gl.GenBuffer();//SetupRenderStateで使用される
			vbaHandle = (int)Gl.GenVertexArray();//SetupRenderStateで使用される
			elementsHandle = (int)Gl.GenBuffer();//SetupRenderStateで使用される
											// Restore modified GL state
			Gl.BindTexture(TextureTarget.Texture2d, (uint)last_texture);
			Gl.BindBuffer(BufferTarget.ArrayBuffer, (uint)last_array_buffer);
		}
		//bool createFontsTexture()
		//{
		//	// Build texture atlas
		//	unsafe
		//	{
		//		var io = ImGui.GetIO();
		//		//Font setup
		//		var config = new ImFontConfigPtr(ImGuiNative.ImFontConfig_ImFontConfig());
		//		// fill with data
		//		config.OversampleH = 2; //横方向のオーバーサンプリング、高画質になるらしい
		//		config.OversampleV = 1;
		//		config.RasterizerMultiply = 1.2f;//1より大きくすると太くなる。imGuiはフォント描画にアンチエイリアスがかかって薄くなるのでこれで対処
		//		config.FontNo = 2;//ttc(ttfが複数集まったやつ)ファイルの場合、この番号でフォントを指定できる。この場合MS UIGothicを指定
		//		config.PixelSnapH = true;//線が濃くなれば良いが効果不明
		//								 //サンプルのコード
		//								 //font = io.Fonts.AddFontFromFileTTF(@"c:\windows\fonts\msgothic.ttc", 12.0f, config, io.Fonts.GetGlyphRangesJapanese());
		//								 //imgui で日本語が「?」になる場合の対処 を適用(https://qiita.com/benikabocha/items/a25571c1b059eaf952de)
		//								 //以下のクラスを作る
		//								 //    public static readonly ushort[] glyphRangesJapanese = new ushort[] { 
		//								 //     0x0020, 0x007E, 0x00A2, 0x00A3, 0x00A7,....};
		//		IntPtr japaneseGlyph = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(ushort)) * FontGlyphs.glyphRangesJapanese.Length);
		//		//Copy()の引数にushort[]が無いので下記のキャストで無理やり渡す
		//		Marshal.Copy((short[])(object)FontGlyphs.glyphRangesJapanese, 0, japaneseGlyph, FontGlyphs.glyphRangesJapanese.Length);
		//		font = io.Fonts.AddFontFromFileTTF(@"c:\windows\fonts\msgothic.ttc", 12.0f, config, japaneseGlyph);
		//		//imgui内部でメモリを直接使用しているらしく、Freeすると落ちる
		//		//Marshal.FreeCoTaskMem(ptr);
		//		config.Destroy();

		//		byte* pixels;
		//		int width, height;
		//		io.Fonts.GetTexDataAsRGBA32(out pixels, out width, out height);   // Load as RGBA 32-bits (75% of the memory is wasted, but default font is so small) because it is more likely to be compatible with user's existing shaders. If your ImTextureId represent a higher-level concept than just a GL texture id, consider calling GetTexDataAsAlpha8() instead to save on GPU memory.

		//		// Upload texture to graphics system
		//		int last_texture;
		//		Gl.GetInteger(GetPName.TextureBinding2D, out last_texture);

		//		fontTexture = Gl.GenTexture();
		//		Gl.BindTexture(TextureTarget.Texture2D, fontTexture);
		//		Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
		//		Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
		//		//# ifdef GL_UNPACK_ROW_LENGTH
		//		//        GL.PixelStore(PixelStoreParameter.PackRowLength, 0);
		//		//#endif
		//		Gl.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, (IntPtr)pixels);

		//		// Store our identifier
		//		io.Fonts.TexID = (IntPtr)fontTexture;

		//		// Restore state
		//		Gl.BindTexture(TextureTarget.Texture2d, (uint)last_texture);
		//	}
		//	return true;
		//}

		private void setStyle()
		{
			var style = ImGui.GetStyle();
			style.WindowPadding = new System.Numerics.Vector2(5f, 5f);
			style.FramePadding = new System.Numerics.Vector2(3f, 2f);
			style.ItemSpacing = new System.Numerics.Vector2(4f, 4f);
			style.WindowRounding = 4f;
			style.TabRounding = 2f;
			style.Colors[(int)ImGuiCol.WindowBg] = new System.Numerics.Vector4(0.94f, 0.94f, 0.94f, 0.78f);
			style.Colors[(int)ImGuiCol.FrameBg] = new System.Numerics.Vector4(1.00f, 1.00f, 1.00f, 0.71f);
			style.Colors[(int)ImGuiCol.ChildBg] = ImGui.ColorConvertU32ToFloat4(0x0f000000);
		}

		#endregion Initialize

		public void ImDraw()
		{
			//ImGui.NewFrame();
			// 1. Show the big demo window (Most of the sample code is in ImGui::ShowDemoWindow()! You can browse its code to learn more about Dear ImGui!).
			if (show_demo_window)
				ImGui.ShowDemoWindow();
			// 2. Show a simple window that we create ourselves. We use a Begin/End pair to created a named window.
			{
				ImGui.Begin("Hello, world!");                          // Create a window called "Hello, world!" and append into it.
																	   //ImGui.PushFont(font);

				ImGui.Text("This is some useful text.");               // Display some text (you can use a format strings too)
				ImGui.Text("サンプルテキスト亜");               // Display some text (you can use a format strings too)
				ImGui.Checkbox("Demo Window", ref show_demo_window);      // Edit bools storing our window open/close state
				ImGui.Checkbox("Another Window", ref show_another_window);

				ImGui.SliderFloat("float", ref f, 0.0f, 1.0f);            // Edit 1 float using a slider from 0.0f to 1.0f
				ImGui.ColorEdit3("clear color", ref clear_color); // Edit 3 floats representing a color

				if (ImGui.Button("Button"))                            // Buttons return true when clicked (most widgets return true when edited/activated)
					counter++;
				ImGui.SameLine();
				ImGui.Text($"counter = {counter}");

				ImGui.Text($"Application average {1000.0f / ImGui.GetIO().Framerate:F3} ms/frame ({ImGui.GetIO().Framerate} FPS)");
				//ImGui.PopFont();
				ImGui.End();
			}

			// 3. Show another simple window.
			if (show_another_window)
			{
				ImGui.Begin("Another Window", ref show_another_window);   // Pass a pointer to our bool variable (the window will have a closing button that will clear the bool when clicked)
				ImGui.Text("Hello from another window!");
				if (ImGui.Button("Close Me"))
					show_another_window = false;
				ImGui.End();
			}

			// Rendering
			ImGui.Render();
			//OpenGLの画面クリアなどは上位モジュールで実施
			//GL.ClearColor(glc.BackColor);
			//GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			ImGui_ImplOpenGL3_RenderDrawData(ImGui.GetDrawData());

			//OpenGLのSwapは上位モジュールで実施
			//glc.SwapBuffers();
		}
		private int imDrawVertSize = System.Runtime.InteropServices.Marshal.SizeOf(default(ImDrawVert));
		private void ImGui_ImplOpenGL3_RenderDrawData(ImDrawDataPtr draw_data)
		{
			// Backup GL state

			//int last_active_texture = 
			Gl.GetInteger(GetPName.ActiveTexture,out int last_active_texture);
			Console.WriteLine("Fzy xxx:" + last_active_texture);
			//int last_program = Gl.GetInteger(GetPName.CurrentProgram);
			Gl.GetInteger(GetPName.CurrentProgram,out int last_program);
			//int last_texture = Gl.GetInteger(GetPName.TextureBinding2D);
			Gl.GetInteger(GetPName.TextureBinding2d, out int last_texture);
			//int last_sampler = GL.GetInteger(GetPName.SamplerBinding);
			Gl.GetInteger(GetPName.SamplerBinding,out int last_sampler);
			//int last_array_buffer = GL.GetInteger(GetPName.ColorArrayBufferBinding);
			Gl.GetInteger(GetPName.ColorArrayBufferBinding,out int last_array_buffer);
			//GetPName.ArrayBufferBinding
			int[] last_polygon_mode = new int[2]; Gl.Get(GetPName.PolygonMode, last_polygon_mode);
			////int[] last_viewport = new int[4]; GL.GetInteger(GetPName.Viewport, last_viewport);
			//int[] last_scissor_box = new int[4]; GL.GetInteger(GetPName.ScissorBox, last_scissor_box);
			int[] last_scissor_box = new int[4]; Gl.Get(GetPName.ScissorBox, last_scissor_box);
			//int last_blend_src_rgb = GL.GetInteger(GetPName.BlendSrcRgb);
			Gl.GetInteger(GetPName.BlendSrcRgb,out int last_blend_src_rgb);
			//int last_blend_dst_rgb = GL.GetInteger(GetPName.BlendDstRgb);
			Gl.GetInteger(GetPName.BlendDstRgb,out int last_blend_dst_rgb);
			//int last_blend_src_alpha = GL.GetInteger(GetPName.BlendSrcAlpha);
			Gl.GetInteger(GetPName.BlendSrcAlpha,out int last_blend_src_alpha);
			//int last_blend_dst_alpha = GL.GetInteger(GetPName.BlendDstAlpha);
			Gl.GetInteger(GetPName.BlendDstAlpha,out int last_blend_dst_alpha);
			//int last_blend_equation_rgb = GL.GetInteger(GetPName.BlendEquationRgb);
			Gl.GetInteger(GetPName.BlendEquationRgb,out int last_blend_equation_rgb);
			//int last_blend_equation_alpha = GL.GetInteger(GetPName.BlendEquationAlpha);
			Gl.GetInteger(GetPName.BlendEquationAlpha,out int last_blend_equation_alpha);
			//bool last_enable_blend = GL.IsEnabled(EnableCap.Blend);
			bool last_enable_blend = Gl.IsEnabled(EnableCap.Blend);
			//bool last_enable_cull_face = GL.IsEnabled(EnableCap.CullFace);
			bool last_enable_cull_face = Gl.IsEnabled(EnableCap.CullFace);
			//bool last_enable_depth_test = GL.IsEnabled(EnableCap.DepthTest);
			bool last_enable_depth_test = Gl.IsEnabled(EnableCap.DepthTest);
			//bool last_enable_scissor_test = GL.IsEnabled(EnableCap.ScissorTest);
			bool last_enable_scissor_test = Gl.IsEnabled(EnableCap.ScissorTest);
			//bool clip_origin_lower_left = true;
			bool clip_origin_lower_left = true;
			//GL.ActiveTexture(TextureUnit.Texture0);
			Gl.ActiveTexture(TextureUnit.Texture0);

            // Setup desired GL state
            // Recreate the VAO every time (this is to easily allow multiple GL contexts to be rendered to. VAO are not shared among GL contexts)
            // The renderer would actually work without any VAO bound, but then our VertexAttrib calls would overwrite the default one currently bound.
            uint vertex_array_object = 0;
            ImGui_ImplOpenGL3_SetupRenderState(draw_data, vertex_array_object);

            // Will project scissor/clipping rectangles into framebuffer space
            var clip_off = draw_data.DisplayPos;         // (0,0) unless using multi-viewports
            var clip_scale = draw_data.FramebufferScale; // (1,1) unless using retina display which are often (2,2)

            // Render command lists
            for (int n = 0; n < draw_data.CmdListsCount; n++)
            {
                var cmd_list = draw_data.CmdListsRange[n];

				// Upload vertex/index buffers  Indexバッファはほぼ100%ushortなのでushortとしてしまう。
				
				Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(cmd_list.VtxBuffer.Size * imDrawVertSize), cmd_list.VtxBuffer.Data, BufferUsage.StreamDraw);
				//GL.BufferData(BufferTarget.ArrayBuffer, cmd_list.VtxBuffer.Size * imDrawVertSize, cmd_list.VtxBuffer.Data, BufferUsageHint.StreamDraw);
				//GL.BufferData(BufferTarget.ElementArrayBuffer, cmd_list.IdxBuffer.Size * sizeof(ushort), cmd_list.IdxBuffer.Data, BufferUsageHint.StreamDraw);
				Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)(cmd_list.IdxBuffer.Size * sizeof(ushort)), cmd_list.IdxBuffer.Data, BufferUsage.StreamDraw);
				
				for (int cmd_i = 0; cmd_i < cmd_list.CmdBuffer.Size; cmd_i++)
                {
                    var pcmd = cmd_list.CmdBuffer[cmd_i];
                    if (pcmd.UserCallback != IntPtr.Zero)//ユーザコールバックはスキップ
                    {
                        // User callback, registered via ImDrawList::AddCallback()
                        // (ImDrawCallback_ResetRenderState is a special callback value used by the user to request the renderer to reset render state.)
                        //if (pcmd.UserCallback == ImGui. ImDrawCallback_ResetRenderState)
                        //  ImGui_ImplOpenGL3_SetupRenderState(draw_data, fb_width, fb_height, vertex_array_object);
                        //else
                        //pcmd->UserCallback(cmd_list, pcmd);
                        Console.WriteLine("UserCallback" + pcmd.UserCallback.ToString());
                    }
                    else
                    {
                        // Project scissor/clipping rectangles into framebuffer space
                        System.Numerics.Vector4 clip_rect = new System.Numerics.Vector4();
                        clip_rect.X = (pcmd.ClipRect.X - clip_off.X) * clip_scale.X;
                        clip_rect.Y = (pcmd.ClipRect.Y - clip_off.Y) * clip_scale.Y;
                        clip_rect.Z = (pcmd.ClipRect.Z - clip_off.X) * clip_scale.X;
                        clip_rect.W = (pcmd.ClipRect.W - clip_off.Y) * clip_scale.Y;

                        if (clip_rect.X < displaySize.X && clip_rect.Y < displaySize.Y && clip_rect.Z >= 0.0f && clip_rect.W >= 0.0f)
                        {
                            // Apply scissor/clipping rectangle
                            if (clip_origin_lower_left)
								Gl.Scissor((int)clip_rect.X, (int)(displaySize.Y - clip_rect.W), (int)(clip_rect.Z - clip_rect.X), (int)(clip_rect.W - clip_rect.Y));
							//GL.Scissor((int)clip_rect.X, (int)(displaySize.Y - clip_rect.W), (int)(clip_rect.Z - clip_rect.X), (int)(clip_rect.W - clip_rect.Y));
                            else
								Gl.Scissor((int)clip_rect.X, (int)clip_rect.Y, (int)clip_rect.Z, (int)clip_rect.W); // Support for GL 4.5 rarely used glClipControl(GL_UPPER_LEFT)
								//GL.Scissor((int)clip_rect.X, (int)clip_rect.Y, (int)clip_rect.Z, (int)clip_rect.W); // Support for GL 4.5 rarely used glClipControl(GL_UPPER_LEFT)

							// Bind texture, Draw
							//GL.BindTexture(TextureTarget.Texture2D, pcmd.TextureId.ToInt32());
							Gl.BindTexture(TextureTarget.Texture2d, (uint)pcmd.TextureId.ToInt32());
							//Indexバッファはほぼ100 % ushortなのでushortとしてしまう。
							//GL.DrawElementsBaseVertex(PrimitiveType.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort, new IntPtr(pcmd.IdxOffset * sizeof(ushort)), (int)pcmd.VtxOffset);
							Gl.DrawElementsBaseVertex(PrimitiveType.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort, new IntPtr(pcmd.IdxOffset * sizeof(ushort)), (int)pcmd.VtxOffset);
							//If glDrawElementsBaseVertex not supported
							//GL.DrawElements(BeginMode.Triangles, pcmd.ElemCount, sizeof(ImDrawIdx) == 2 ? GL_UNSIGNED_SHORT : GL_UNSIGNED_INT, (void*)(intptr_t)(pcmd->IdxOffset * sizeof(ImDrawIdx)));
						}
					}
                }
            }
            // Restore modified GL state
            Gl.UseProgram((uint)last_program);
            Gl.BindTexture(TextureTarget.Texture2d, (uint)last_texture);
            Gl.BindSampler(0, (uint)last_sampler);
            Gl.ActiveTexture((TextureUnit)last_active_texture);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, (uint)last_array_buffer);
            Gl.BlendEquationSeparate((BlendEquationMode)last_blend_equation_rgb, (BlendEquationMode)last_blend_equation_alpha);
			
            Gl.BlendFuncSeparate((BlendingFactor)last_blend_src_rgb, (BlendingFactor)last_blend_dst_rgb, (BlendingFactor)last_blend_src_alpha, (BlendingFactor)last_blend_dst_alpha);
			if (last_enable_blend) Gl.Enable(EnableCap.Blend); else Gl.Disable(EnableCap.Blend);
            if (last_enable_cull_face) Gl.Enable(EnableCap.CullFace); else Gl.Disable(EnableCap.CullFace);
            if (last_enable_depth_test) Gl.Enable(EnableCap.DepthTest); else Gl.Disable(EnableCap.DepthTest);
            if (last_enable_scissor_test) Gl.Enable(EnableCap.ScissorTest); else Gl.Disable(EnableCap.ScissorTest);
            Gl.PolygonMode(MaterialFace.FrontAndBack, (PolygonMode)last_polygon_mode[0]);
            //GL.Viewport(last_viewport[0], last_viewport[1], last_viewport[2], last_viewport[3]);
            Gl.Scissor(last_scissor_box[0], last_scissor_box[1], last_scissor_box[2], last_scissor_box[3]);
            Gl.DisableVertexAttribArray((uint)attribLocationVtxPos);
            Gl.DisableVertexAttribArray((uint)attribLocationVtxUV);
            Gl.DisableVertexAttribArray((uint)attribLocationVtxColor);

        }
		#endregion sample code

		private void ImGui_ImplOpenGL3_SetupRenderState(ImDrawDataPtr draw_data, uint vertex_array_object)
		{
            // Setup render state: alpha-blending enabled, no face culling, no depth testing, scissor enabled, polygon fill
            Gl.Enable(EnableCap.Blend);
            Gl.BlendEquation(BlendEquationMode.FuncAdd);
            Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            Gl.Disable(EnableCap.CullFace);
            Gl.Disable(EnableCap.DepthTest);
            Gl.Enable(EnableCap.ScissorTest);
            Gl.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            //↓サイズ変更の時だけ設定すればいいのでは？
            // Setup viewport, orthographic projection matrix
            // Our visible imgui space lies from draw_data->DisplayPos (top left) to draw_data->DisplayPos+data_data->DisplaySize (bottom right). DisplayPos is (0,0) for single viewport apps.
            //glViewport(0, 0, (GLsizei)fb_width, (GLsizei)fb_height);
            float L = draw_data.DisplayPos.X;
            float R = draw_data.DisplayPos.X + draw_data.DisplaySize.X;
            float T = draw_data.DisplayPos.Y;
            float B = draw_data.DisplayPos.Y + draw_data.DisplaySize.Y;
			//ここだけはOptenTKのstructにする

			Matrix4x4f ortho_projection = new Matrix4x4f(
              2.0f / (R - L), 0.0f, 0.0f, 0.0f,
              0.0f, 2.0f / (T - B), 0.0f, 0.0f,
              0.0f, 0.0f, -1.0f, 0.0f,
              (R + L) / (L - R), (T + B) / (B - T), 0.0f, 1.0f
              );

            Gl.UseProgram((uint)shaderProgram);
            Gl.Uniform1(attribLocationTex, 0);
            Gl.UniformMatrix4f(attribLocationProjMtx,1, false, ortho_projection);

            Gl.BindSampler(0, 0); // We use combined texture/sampler state. Applications using GL 3.3 may set that otherwise.

            // Bind vertex/index buffers and setup attributes for ImDrawVert
            Gl.BindVertexArray((uint)vbaHandle);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, (uint)vboHandle);
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, (uint)elementsHandle);
            Gl.EnableVertexAttribArray((uint)attribLocationVtxPos);
            Gl.EnableVertexAttribArray((uint)attribLocationVtxUV);
            Gl.EnableVertexAttribArray((uint)attribLocationVtxColor);

            Gl.VertexAttribPointer((uint)attribLocationVtxPos, 2, VertexAttribType.Float, false, imDrawVertSize, 0);//ImDrawVertのoffsetの値は直接入力
            Gl.VertexAttribPointer((uint)attribLocationVtxUV, 2, VertexAttribType.Float, false, imDrawVertSize, 8);//0番目(pos)はVector2なのでfloat*2 = 8
            Gl.VertexAttribPointer((uint)attribLocationVtxColor, 4, VertexAttribType.UnsignedByte, true, imDrawVertSize, 16);//1番目(puv)はVector2なので 8 + float*2 = 16
        }


		bool _showAnotherWindow = false;
		private void RenderControl_ContextPaint(object sender, PaintEventArgs e)
		{
			// Change triangle rotation
			//Console.WriteLine("fzy xxx13");
			var x = sender as GlControl;
			if(x!=null)
            {
				Gl.ClearColor(0, 0, 0, 0);
				Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
				ImDraw();

				//Gl.GetInteger(GetPName.ActiveTexture, out int last_active_texture);
				
				//int last_program = Gl.GetInteger(GetPName.CurrentProgram);
				//Gl.GetInteger(GetPName.CurrentProgram, out int last_program);
				//int last_texture = Gl.GetInteger(GetPName.TextureBinding2D);
				//Gl.GetInteger(GetPName.TextureBinding2d, out int last_texture);
				//int last_sampler = GL.GetInteger(GetPName.SamplerBinding);
				//Gl.GetInteger(GetPName.SamplerBinding, out int last_sampler);
				//Gl.GetInteger(GetPName.ColorArrayBufferBinding, out int last_array_buffer);
				//GetPName.ArrayBufferBinding
				//int[] last_polygon_mode = new int[2]; Gl.Get(GetPName.PolygonMode, last_polygon_mode);

				//Console.WriteLine("Fzy xxx:" + last_active_texture + "  "+ last_program+"   "+last_texture+"   "+last_sampler+"   "+ last_array_buffer+"   "+last_polygon_mode[0]+"   "+last_polygon_mode[1]);
				//ImGui.Begin("Another Window", ref _showAnotherWindow);
				//ImGui.Text("Hello from another window!");
				//if (ImGui.Button("Close Me"))
				//	_showAnotherWindow = false;
				//ImGui.End();
				//Console.WriteLine("fzy xxx13");
			}
		}

		private void RenderControl_ContextDestroying(object sender, GlControlEventArgs e)
		{
			_Program?.Dispose();
			_VertexArray?.Dispose();
		}

		private static void GLDebugProc(DebugSource source, DebugType type, uint id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
		{
			string strMessage;

			// Decode message string
			unsafe
			{
				strMessage = Encoding.ASCII.GetString((byte*)message.ToPointer(), length);
			}

			Console.WriteLine($"{source}, {type}, {severity}: {strMessage}");
		}

		#endregion

		#region Immediate Mode

		// Note: this methods are written for didactic purposes; they use old fixed pipeline methods, and
		// should be used.

		/// <summary>
		/// Setup GL state for OpenGL 1.1/1.0.
		/// </summary>
		private void RenderControl_CreateGL100()
		{
			// Setup projection matrix, only once since it remains fixed for the application lifetime

			// Projection matrix selector
			Gl.MatrixMode(MatrixMode.Projection);
			// Load (reset) to identity
			Gl.LoadIdentity();
			// Multiply with orthographic projection
			Gl.Ortho(0.0, 1.0, 0.0, 1.0, 0.0, 1.0);
		}

		/// <summary>
		/// Old school OpenGL 1.0 drawing.
		/// </summary>
		private void RenderControl_RenderGL100()
		{
			// Setup model-view matrix

			// Model-view matrix selector
			Gl.MatrixMode(MatrixMode.Modelview);
			// Load (reset) to identity
			Gl.LoadIdentity();
			// Multiply with rotation matrix (around Z axis)
			Gl.Rotate(_Angle, 0.0f, 0.0f, 1.0f);

			// Draw triangle using immediate mode (8 draw call)

			// Start drawing triangles
			Gl.Begin(PrimitiveType.Triangles);

			// Feed triangle data: color and position
			// Note: vertex attributes (color, texture coordinates, ...) are specified before position information
			// Note: vertex data is passed using method calls (performance killer!)
			Gl.Color3(1.0f, 0.0f, 0.0f); Gl.Vertex2(0.0f, 0.0f);
			Gl.Color3(0.0f, 1.0f, 0.0f); Gl.Vertex2(0.5f, 1.0f);
			Gl.Color3(0.0f, 0.0f, 1.0f); Gl.Vertex2(1.0f, 0.0f);

			// Triangles ends
			Gl.End();
		}

		/// <summary>
		/// Old school OpenGL 1.1 drawing.
		/// </summary>
		private void RenderControl_RenderGL110()
		{
			// Setup model-view matrix (as previously)

			// Model-view matrix selector
			Gl.MatrixMode(MatrixMode.Modelview);
			// Load (reset) to identity
			Gl.LoadIdentity();
			// Multiply with rotation matrix (around Z axis)
			Gl.Rotate(_Angle, 0.0f, 0.0f, 1.0f);

			// Draw triangle using immediate mode

			// Setup & enable client states to specify vertex arrays, and use Gl.DrawArrays instead of Gl.Begin/End paradigm
			using (MemoryLock vertexArrayLock = new MemoryLock(_ArrayPosition))
			using (MemoryLock vertexColorLock = new MemoryLock(_ArrayColor))
			{
				// Note: the use of MemoryLock objects is necessary to pin vertex arrays since they can be reallocated by GC
				// at any time between the Gl.VertexPointer execution and the Gl.DrawArrays execution

				// Set current client memory pointer for position: a vertex of 2 floats
				Gl.VertexPointer(2, VertexPointerType.Float, 0, vertexArrayLock.Address);
				// Position is used for drawing
				Gl.EnableClientState(EnableCap.VertexArray);

				// Set current client memory pointer for color: a vertex of 3 floats
				Gl.ColorPointer(3, ColorPointerType.Float, 0, vertexColorLock.Address);
				// Color is used for drawing
				Gl.EnableClientState(EnableCap.ColorArray);

				// Note: enabled client state and client memory pointers are a GL state, and theorically they could be
				// set only at creation time. However, memory should be pinned for application lifetime.

				// Start drawing triangles (3 vertices -> 1 triangle)
				// Note: vertex attributes are streamed from client memory to GPU
				Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
			}
		}

		#endregion

		#region Common Shading

		// Note: abstractions for drawing using programmable pipeline.

		/// <summary>
		/// Shader object abstraction.
		/// </summary>
		private class Object : IDisposable
		{
			public Object(ShaderType shaderType, string[] source)
			{
				if (source == null)
					throw new ArgumentNullException(nameof(source));

				// Create
				ShaderName = Gl.CreateShader(shaderType);
				// Submit source code
				Gl.ShaderSource(ShaderName, source);
				// Compile
				Gl.CompileShader(ShaderName);
				// Check compilation status
				int compiled;

				Gl.GetShader(ShaderName, ShaderParameterName.CompileStatus, out compiled);
				if (compiled != 0)
					return;

				// Throw exception on compilation errors
				const int logMaxLength = 1024;

				StringBuilder infolog = new StringBuilder(logMaxLength);
				int infologLength;

				Gl.GetShaderInfoLog(ShaderName, logMaxLength, out infologLength, infolog);

				throw new InvalidOperationException($"unable to compile shader: {infolog}");
			}

			public readonly uint ShaderName;

			public void Dispose()
			{
				Gl.DeleteShader(ShaderName);
			}
		}

		/// <summary>
		/// Program abstraction.
		/// </summary>
		private class Program : IDisposable
		{
			public Program(string[] vertexSource, string[] fragmentSource)
			{
				// Create vertex and frament shaders
				// Note: they can be disposed after linking to program; resources are freed when deleting the program
				using (Object vObject = new Object(ShaderType.VertexShader, vertexSource))
				using (Object fObject = new Object(ShaderType.FragmentShader, fragmentSource))
				{
					// Create program
					ProgramName = Gl.CreateProgram();
					// Attach shaders
					Gl.AttachShader(ProgramName, vObject.ShaderName);
					Gl.AttachShader(ProgramName, fObject.ShaderName);
					// Link program
					Gl.LinkProgram(ProgramName);
					Gl.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
					// Check linkage status
					int linked;

					Gl.GetProgram(ProgramName, ProgramProperty.LinkStatus, out linked);

					if (linked == 0)
					{
						const int logMaxLength = 1024;

						StringBuilder infolog = new StringBuilder(logMaxLength);
						int infologLength;

						Gl.GetProgramInfoLog(ProgramName, 1024, out infologLength, infolog);

						throw new InvalidOperationException($"unable to link program: {infolog}");
					}

					// Get uniform locations
					if ((LocationMVP = Gl.GetUniformLocation(ProgramName, "uMVP")) < 0)
						throw new InvalidOperationException("no uniform uMVP");

					// Get attributes locations
					if ((LocationPosition = Gl.GetAttribLocation(ProgramName, "aPosition")) < 0)
						throw new InvalidOperationException("no attribute aPosition");
					if ((LocationColor = Gl.GetAttribLocation(ProgramName, "aColor")) < 0)
						throw new InvalidOperationException("no attribute aColor");
				}
			}

			public readonly uint ProgramName;

			public readonly int LocationMVP;

			public readonly int LocationPosition;

			public readonly int LocationColor;

			public void Dispose()
			{
				Gl.DeleteProgram(ProgramName);
			}
		}

		/// <summary>
		/// Buffer abstraction.
		/// </summary>
		private class Buffer : IDisposable
		{
			public Buffer(float[] buffer)
			{
				if (buffer == null)
					throw new ArgumentNullException(nameof(buffer));

				// Generate a buffer name: buffer does not exists yet
				BufferName = Gl.GenBuffer();
				// First bind create the buffer, determining its type
				Gl.BindBuffer(BufferTarget.ArrayBuffer, BufferName);
				// Set buffer information, 'buffer' is pinned automatically
				Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(4 * buffer.Length), buffer, BufferUsage.StaticDraw);
			}

			public readonly uint BufferName;

			public void Dispose()
			{
				Gl.DeleteBuffers(BufferName);
			}
		}

		/// <summary>
		/// Vertex array abstraction.
		/// </summary>
		private class VertexArray : IDisposable
		{
			public VertexArray(Program program, float[] positions, float[] colors)
			{
				if (program == null)
					throw new ArgumentNullException(nameof(program));

				// Allocate buffers referenced by this vertex array
				_BufferPosition = new Buffer(positions);
				_BufferColor = new Buffer(colors);

				// Generate VAO name
				ArrayName = Gl.GenVertexArray();
				// First bind create the VAO
				Gl.BindVertexArray(ArrayName);

				// Set position attribute

				// Select the buffer object
				Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferPosition.BufferName);
				// Format the vertex information: 2 floats from the current buffer
				Gl.VertexAttribPointer((uint)program.LocationPosition, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
				// Enable attribute
				Gl.EnableVertexAttribArray((uint)program.LocationPosition);

				// As above, but for color attribute
				Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferColor.BufferName);
				Gl.VertexAttribPointer((uint)program.LocationColor, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
				Gl.EnableVertexAttribArray((uint)program.LocationColor);
			}

			public readonly uint ArrayName;

			private readonly Buffer _BufferPosition;

			private readonly Buffer _BufferColor;

			public void Dispose()
			{
				Gl.DeleteVertexArrays(ArrayName);

				_BufferPosition.Dispose();
				_BufferColor.Dispose();
			}
		}

		/// <summary>
		/// The program used for drawing the triangle.
		/// </summary>
		private Program _Program;

		/// <summary>
		/// The vertex arrays used for drawing the triangle.
		/// </summary>
		private VertexArray _VertexArray;

		#endregion

		#region Shaders

		private void RenderControl_CreateGL320()
		{
			_Program = new Program(_VertexSourceGL, _FragmentSourceGL);
			_VertexArray = new VertexArray(_Program, _ArrayPosition, _ArrayColor);
		}

		private void RenderControl_RenderGL320()
		{
			// Compute the model-view-projection on CPU
			Matrix4x4f projection = Matrix4x4f.Ortho2D(-1.0f, +1.0f, -1.0f, +1.0f);
			Matrix4x4f modelview = Matrix4x4f.Translated(-0.5f, -0.5f, 0.0f) * Matrix4x4f.RotatedZ(_Angle);

			// Select the program for drawing
			Gl.UseProgram(_Program.ProgramName);
			// Set uniform state
			Gl.UniformMatrix4f(_Program.LocationMVP, 1, false, projection * modelview);
			// Use the vertex array
			Gl.BindVertexArray(_VertexArray.ArrayName);
			// Draw triangle
			// Note: vertex attributes are streamed from GPU memory
			Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
		}

		private readonly string[] _VertexSourceGL = {
			"#version 150 compatibility\n",
			"uniform mat4 uMVP;\n",
			"in vec2 aPosition;\n",
			"in vec3 aColor;\n",
			"out vec3 vColor;\n",
			"void main() {\n",
			"	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
			"	vColor = aColor;\n",
			"}\n"
		};

		private readonly string[] _FragmentSourceGL = {
			"#version 150 compatibility\n",
			"in vec3 vColor;\n",
			"void main() {\n",
			"	gl_FragColor = vec4(vColor, 1.0);\n",
			"}\n"
		};

		#endregion

		#region Shaders (ES2)

		private void RenderControl_CreateGLES2()
		{
			// Create program
			_Program = new Program(_VertexSourceGLES2, _FragmentSourceGLES2);
		}

		private void RenderControl_RenderGLES2()
		{
			Matrix4x4f projection = Matrix4x4f.Ortho2D(0.0f, 1.0f, 0.0f, 1.0f);
			Matrix4x4f modelview = Matrix4x4f.RotatedZ(_Angle);

			Gl.UseProgram(_Program.ProgramName);

			using (MemoryLock arrayPosition = new MemoryLock(_ArrayPosition))
			using (MemoryLock arrayColor = new MemoryLock(_ArrayColor))
			{
				Gl.VertexAttribPointer((uint)_Program.LocationPosition, 2, VertexAttribType.Float, false, 0, arrayPosition.Address);
				Gl.EnableVertexAttribArray((uint)_Program.LocationPosition);

				Gl.VertexAttribPointer((uint)_Program.LocationColor, 3, VertexAttribType.Float, false, 0, arrayColor.Address);
				Gl.EnableVertexAttribArray((uint)_Program.LocationColor);

				Gl.UniformMatrix4f(_Program.LocationMVP, 1, false, projection * modelview);

				Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
			}
		}

		private readonly string[] _VertexSourceGLES2 = {
			"uniform mat4 uMVP;\n",
			"attribute vec2 aPosition;\n",
			"attribute vec3 aColor;\n",
			"varying vec3 vColor;\n",
			"void main() {\n",
			"	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
			"	vColor = aColor;\n",
			"}\n"
		};

		private readonly string[] _FragmentSourceGLES2 = {
			"precision mediump float;\n",
			"varying vec3 vColor;\n",
			"void main() {\n",
			"	gl_FragColor = vec4(vColor, 1.0);\n",
			"}\n"
		};

		#endregion

		#region Common Data

		private static float _Angle;

		/// <summary>
		/// Vertex position array.
		/// </summary>
		private static readonly float[] _ArrayPosition = new float[] {
			0.0f, 0.0f,
			1.0f, 0.0f,
			1.0f, 1.0f
		};

		/// <summary>
		/// Vertex color array.
		/// </summary>
		private static readonly float[] _ArrayColor = new float[] {
			1.0f, 0.0f, 0.0f,
			0.0f, 1.0f, 0.0f,
			0.0f, 0.0f, 1.0f
		};

		#endregion
	}
}


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
using WeifenLuo.WinFormsUI.Docking;

using AdamsLair.WinForms.ItemModels;
using AdamsLair.WinForms.ItemViews;
using Duality.Editor.Properties;
using Duality.Editor;
using GLEngine.Properties;
using GLEngine.Config;

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
	public partial class MainForm : Form
	{
		public ImGUIManager ImGUIManager;

		private bool shownWasCalled = false;
		private bool nonUserClosing = false;
		private MenuModel mainMenuModel = new MenuModel();
		private MenuStripMenuView mainMenuView = null;
		private MenuModel serializerMenuModel = new MenuModel();
		private MenuStripMenuView serializerMenuView = null;
		//private WelcomeDialog welcomeDialog = null;

		// Hardcoded main menu items
		private MenuModelItem menuRunSandboxPlay = null;
		private MenuModelItem menuRunSandboxPause = null;
		private MenuModelItem menuRunSandboxStop = null;
		private MenuModelItem menuRunSandboxStep = null;
		private MenuModelItem menuRunSandboxFaster = null;
		private MenuModelItem menuRunSandboxSlower = null;
		private MenuModelItem menuEditUndo = null;
		private MenuModelItem menuEditRedo = null;
		private MenuModelItem menuRunApp = null;
		private MenuModelItem menuDebugApp = null;
		private MenuModelItem menuProfileApp = null;

		protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
			this.WindowState = FormWindowState.Maximized;
		}

        /// <summary>
        /// Construct a SampleForm.
        /// </summary>
        public MainForm()
		{
			this.ImGUIManager = new ImGUIManager();
			this.InitializeComponent();

			this.InitMenus();
		}

		public void InitMenus()
		{
			
			this.mainMenuView = new MenuStripMenuView(this.mainMenuStrip.Items);
			this.mainMenuView.Model = this.mainMenuModel;

			MenuModelItem helpItem;
			this.mainMenuModel.AddItems(new[]
			{
				new MenuModelItem { Name = ConfigRoot.Editor_Window_Conf.GetText(ConfigKey.MenuName_File), SortValue = MenuModelItem.SortValue_Top, Items = new[]
				{
					new MenuModelItem
					{
						Name            = ConfigRoot.Editor_Window_Conf.GetText(ConfigKey.MenuItemName_PublishGame),
						SortValue       = MenuModelItem.SortValue_Top,
						Tag             = HelpInfo.FromText(ConfigRoot.Editor_Window_Conf.GetText(ConfigKey.MenuItemName_PublishGame), GeneralRes.MenuItemInfo_PublishGame),
						//ActionHandler   = this.actionPublishGame_Click
					},
					new MenuModelItem
					{
						Name            = "TopSeparator",
						SortValue       = MenuModelItem.SortValue_Top,
						TypeHint        = MenuItemTypeHint.Separator
					},
					new MenuModelItem
					{
						Name            = this.actionSaveAll.Text,
						Icon            = this.actionSaveAll.Image,
						ShortcutKeys    = Keys.Control | Keys.S,
						Tag             = HelpInfo.FromText(this.actionSaveAll.Text, ConfigRoot.Editor_Window_Conf.GetText(ConfigKey.MenuItemInfo_SaveAll)),
						//ActionHandler   = this.actionSaveAll_Click
					},
					new MenuModelItem
					{
						Name            = "CodeSeparator",
						TypeHint        = MenuItemTypeHint.Separator
					},
					new MenuModelItem
					{
						Name            = this.actionOpenCode.Text,
						Icon            = this.actionOpenCode.Image,
						Tag             = HelpInfo.FromText(this.actionOpenCode.Text, GeneralRes.MenuItemInfo_OpenProjectSource),
						//ActionHandler   = this.actionOpenCode_Click
					},
					new MenuModelItem
					{
						Name            = "BottomSeparator",
						SortValue       = MenuModelItem.SortValue_Bottom,
						TypeHint        = MenuItemTypeHint.Separator
					},
					new MenuModelItem
					{
						Name            = GeneralRes.MenuItemName_Quit,
						SortValue       = MenuModelItem.SortValue_Bottom,
						ShortcutKeys    = Keys.Alt | Keys.F4,
						//ActionHandler   = this.quitItem_Click
					}
				}},
				new MenuModelItem { Name = ConfigRoot.Editor_Window_Conf.GetText(ConfigKey.MenuName_Edit), SortValue = MenuModelItem.SortValue_Top, Items = new[]
				{
					this.menuEditUndo = new MenuModelItem
					{
						Name            = ConfigRoot.Editor_Window_Conf.GetText(ConfigKey.MenuItemName_Undo),
						SortValue       = MenuModelItem.SortValue_Top,
						Icon            = GeneralResCache.arrow_undo,
						//ShortcutKeys    = Keys.Z | Keys.Control,
						//ActionHandler   = this.menuEditUndo_Click
					},
					this.menuEditRedo = new MenuModelItem
					{
						Name            = ConfigRoot.Editor_Window_Conf.GetText(ConfigKey.MenuItemName_Redo),
						SortValue       = MenuModelItem.SortValue_Top,
						Icon            = GeneralResCache.arrow_redo,
						//ShortcutKeys    = Keys.Y | Keys.Control,
						//ActionHandler   = this.menuEditRedo_Click
					}
				}},
				new MenuModelItem { Name = ConfigRoot.Editor_Window_Conf.GetText(ConfigKey.MenuName_Run), SortValue = MenuModelItem.SortValue_OverBottom, Items = new[]
				{
					this.menuRunApp = new MenuModelItem
					{
						Name            = this.actionRunApp.Text,
						SortValue       = MenuModelItem.SortValue_Top,
						Icon            = this.actionRunApp.Image,
						ShortcutKeys    = Keys.Alt | Keys.F5,
						Tag             = HelpInfo.FromText(this.actionRunApp.Text, GeneralRes.MenuItemInfo_RunGame),
						//ActionHandler   = this.actionRunApp_Click
					},
					this.menuDebugApp = new MenuModelItem
					{
						Name            = this.actionDebugApp.Text,
						SortValue       = MenuModelItem.SortValue_Top,
						Icon            = this.actionDebugApp.Image,
						ShortcutKeys    = Keys.Alt | Keys.F6,
						Tag             = HelpInfo.FromText(this.actionDebugApp.Text, GeneralRes.MenuItemInfo_DebugGame),
						//ActionHandler   = this.actionDebugApp_Click
					},
					this.menuProfileApp = new MenuModelItem
					{
						Name            = GeneralRes.MenuItemName_ProfileGame,
						SortValue       = MenuModelItem.SortValue_Top,
						Icon            = Resources.application_stopwatch,
						Tag             = HelpInfo.FromText(GeneralRes.MenuItemName_ProfileGame, GeneralRes.MenuItemInfo_ProfileGame),
						//ActionHandler   = this.actionProfileApp_Click
					},
					new MenuModelItem
					{
						Name            = GeneralRes.MenuItemName_ConfigureLauncher,
						SortValue       = MenuModelItem.SortValue_Top,
						Tag             = HelpInfo.FromText(GeneralRes.MenuItemName_ConfigureLauncher, GeneralRes.MenuItemInfo_ConfigureLauncher),
						//ActionHandler   = this.actionConfigureLauncher_Click
					},
					new MenuModelItem
					{
						Name            = "TopSeparator",
						SortValue       = MenuModelItem.SortValue_Top,
						TypeHint        = MenuItemTypeHint.Separator
					},
					this.menuRunSandboxPlay = new MenuModelItem
					{
						Name            = this.actionRunSandbox.Text,
						Icon            = this.actionRunSandbox.Image,
						ShortcutKeys    = Keys.F5,
						Tag             = HelpInfo.FromText(this.actionRunSandbox.Text, GeneralRes.MenuItemInfo_SandboxPlay),
						//ActionHandler   = this.actionRunSandbox_Click
					},
					this.menuRunSandboxStep = new MenuModelItem
					{
						Name            = this.actionStepSandbox.Text,
						Icon            = this.actionStepSandbox.Image,
						ShortcutKeys    = Keys.F6,
						Tag             = HelpInfo.FromText(this.actionStepSandbox.Text, GeneralRes.MenuItemInfo_SandboxStep),
						//ActionHandler   = this.actionStepSandbox_Click
					},
					this.menuRunSandboxPause = new MenuModelItem
					{
						Name            = this.actionPauseSandbox.Text,
						Icon            = this.actionPauseSandbox.Image,
						ShortcutKeys    = Keys.F7,
						Tag             = HelpInfo.FromText(this.actionPauseSandbox.Text, GeneralRes.MenuItemInfo_SandboxPause),
						//ActionHandler   = this.actionPauseSandbox_Click
					},
					this.menuRunSandboxStop = new MenuModelItem
					{
						Name            = this.actionStopSandbox.Text,
						Icon            = this.actionStopSandbox.Image,
						ShortcutKeys    = Keys.F8,
						Tag             = HelpInfo.FromText(this.actionStopSandbox.Text, GeneralRes.MenuItemInfo_SandboxStop),
						//ActionHandler   = this.actionStopSandbox_Click
					},
					new MenuModelItem
					{
						Name            = "BottomSeparator",
						SortValue       = MenuModelItem.SortValue_Bottom,
						TypeHint        = MenuItemTypeHint.Separator
					},
					this.menuRunSandboxSlower = new MenuModelItem
					{
						Name            = GeneralRes.MenuItemName_SandboxSlower,
						ShortcutKeys    = Keys.F9,
						Tag             = HelpInfo.FromText(GeneralRes.MenuItemName_SandboxSlower, GeneralRes.MenuItemInfo_SandboxSlower),
						//ActionHandler   = this.menuRunSandboxSlower_Click
					},
					this.menuRunSandboxFaster = new MenuModelItem
					{
						Name            = GeneralRes.MenuItemName_SandboxFaster,
						ShortcutKeys    = Keys.F10,
						Tag             = HelpInfo.FromText(GeneralRes.MenuItemName_SandboxFaster, GeneralRes.MenuItemInfo_SandboxFaster),
						//ActionHandler   = this.menuRunSandboxFaster_Click
					}
				}},
				helpItem = new MenuModelItem { Name = GeneralRes.MenuName_Help, SortValue = MenuModelItem.SortValue_Bottom, Items = new[]
				{
					new MenuModelItem
					{
						Name            = GeneralRes.MenuItemName_About,
						SortValue       = MenuModelItem.SortValue_Top,
						//ActionHandler   = this.aboutItem_Click
					},
					new MenuModelItem
					{
						Name            = "TopSeparator",
						SortValue       = MenuModelItem.SortValue_Top + 1,
						TypeHint        = MenuItemTypeHint.Separator
					},
					new MenuModelItem
					{
						Name            = GeneralRes.MenuItemName_WelcomeDialog,
						//ActionHandler   = this.welcomeDialogItem_Click
					}
				}}
			});

			this.serializerMenuView = new MenuStripMenuView(this.selectFormattingMethod.DropDownItems);
			this.serializerMenuView.Model = this.serializerMenuModel;

			this.serializerMenuModel.AddItems(new[]
			{
				//分隔符Item
				new MenuModelItem
				{
					Name            = "BottomSeparator",
					SortValue       = MenuModelItem.SortValue_Bottom,
					TypeHint        = MenuItemTypeHint.Separator
				},
				new MenuModelItem
				{
					Name            = GeneralRes.MenuItemName_SerializerUpdateAll,
					SortValue       = MenuModelItem.SortValue_Bottom,
					Tag             = HelpInfo.FromText(GeneralRes.MenuItemName_SerializerUpdateAll, GeneralRes.MenuItemInfo_SerializerUpdateAll),
					//ActionHandler   = this.formatUpdateAll_Click
				}
			});

			// Set some view-specific properties
			ToolStripItem helpViewItem = this.mainMenuView.GetViewItem(helpItem);
			helpViewItem.Alignment = ToolStripItemAlignment.Right;

			// Attach help data to toolstrip actions
			this.actionOpenCode.Tag = HelpInfo.FromText(this.actionOpenCode.Text, GeneralRes.MenuItemInfo_OpenProjectSource);
			this.actionSaveAll.Tag = HelpInfo.FromText(this.actionSaveAll.Text, GeneralRes.MenuItemInfo_SaveAll);
			this.actionRunApp.Tag = HelpInfo.FromText(this.actionRunApp.Text, GeneralRes.MenuItemInfo_RunGame);
			this.actionDebugApp.Tag = HelpInfo.FromText(this.actionDebugApp.Text, GeneralRes.MenuItemInfo_DebugGame);
			this.actionRunSandbox.Tag = HelpInfo.FromText(this.actionRunSandbox.Text, GeneralRes.MenuItemInfo_SandboxPlay);
			this.actionStepSandbox.Tag = HelpInfo.FromText(this.actionStepSandbox.Text, GeneralRes.MenuItemInfo_SandboxStep);
			this.actionPauseSandbox.Tag = HelpInfo.FromText(this.actionPauseSandbox.Text, GeneralRes.MenuItemInfo_SandboxPause);
			this.actionStopSandbox.Tag = HelpInfo.FromText(this.actionStopSandbox.Text, GeneralRes.MenuItemInfo_SandboxStop);
			this.checkBackups.Tag = HelpInfo.FromText(this.checkBackups.Text, GeneralRes.MenuItemInfo_ToggleBackups);
		}

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
		/// 

		private void RenderControl_ContextCreated(object sender, GlControlEventArgs e)
		{
			GlControl glControl = (GlControl)sender;

			Gl.Extensions x = new Gl.Extensions();

			var res = Gl.Limits.Query(Gl.CurrentVersion, x);
			var size = res.LineWidthRange;
			Console.WriteLine("fzy hhh:" + size[0] + "   " + size[1] + "    " + (Gl.CurrentVersion >= Gl.Version_320));


			// GL Debugging  逆天Debug部分有bug，开启后无法调试，注释掉了
			//if (Gl.CurrentExtensions != null && Gl.CurrentExtensions.DebugOutput_ARB)
			//{
			//	Gl.DebugMessageCallback(GLDebugProc, IntPtr.Zero);
			//	Gl.DebugMessageControl(DebugSource.DontCare, DebugType.DontCare, DebugSeverity.DontCare, 0, null, true);
			//}

			// Allocate resources and/or setup GL states
			switch (Gl.CurrentVersion.Api)
			{
				case KhronosVersion.ApiGl:
					if (Gl.CurrentVersion >= Gl.Version_320)
						RenderControl_CreateGL320();
					else
						Console.WriteLine("not support immediate mode");
					break;
				case KhronosVersion.ApiGles2:
					RenderControl_CreateGLES2();
					break;
			}
            
            //DrawRequested += (arg1, arg2) => glControl.Invalidate();
			ImGUIManager.Init(glControl);
			ImGUIManager.DrawRequested += (arg1, arg2) => glControl.Invalidate();

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
					else
						Console.WriteLine("not support immediate mode");
					break;
				case KhronosVersion.ApiGles2:
					RenderControl_RenderGLES2();
					break;
			}
			if (ImGUIManager != null)
				ImGUIManager.ImDraw();
			//ImDraw();
		}

		private void RenderControl_ContextUpdate(object sender, GlControlEventArgs e)
		{
			// Change triangle rotation
			_Angle = (_Angle + 0.1f) % 45.0f;
		}


		private void RenderControl_ContextPaint(object sender, PaintEventArgs e)
		{
			// Change triangle rotation
			//Console.WriteLine("fzy xxx13");
			var x = sender as GlControl;
			if(x!=null)
            {
				Gl.ClearColor(0, 0, 0, 0);
				Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
				//ImDraw();


			}
		}

		private void RenderControl_ContextDestroying(object sender, GlControlEventArgs e)
		{
			_Program?.Dispose();
			_VertexArray?.Dispose();
			ImGui.DestroyContext();
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
				//在调用useprogram后，着色器被链接到程序对象后就可以删除shader了
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

					Console.WriteLine("fzy location:"+LocationPosition+"   "+LocationColor);
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
				_BufferPosition = new Buffer(positions);//创建vbo
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
				//第五个参数叫做步长(Stride)，它告诉我们在连续的顶点属性组之间的间隔。由于下个组位置数据在3个float之后，我们把步长设置为3 * sizeof(float)。
				//要注意的是由于我们知道这个数组是紧密排列的（在两个顶点属性之间没有空隙）我们也可以设置为0来让OpenGL决定具体步长是多少（只有当数值是紧密排列时才可用）。
				//一旦我们有更多的顶点属性，我们就必须更小心地定义每个顶点属性之间的间隔，我们在后面会看到更多的例子（译注: 这个参数的意思简单说就是从这个属性第二次出现的地方到整个数组0位置之间有多少字节）。
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

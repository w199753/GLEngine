
// MIT License
// 
// Copyright (c) 2009-2017 Luca Piccioni
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
// 
// This file is automatically generated

#pragma warning disable 649, 1572, 1573

// ReSharper disable RedundantUsingDirective
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

using Khronos;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable JoinDeclarationAndInitializer

namespace OpenGL
{
	public partial class Gl
	{
		/// <summary>
		/// [GL] Value of GL_MAX_VERTEX_BINDABLE_UNIFORMS_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public const int MAX_VERTEX_BINDABLE_UNIFORMS_EXT = 0x8DE2;

		/// <summary>
		/// [GL] Value of GL_MAX_FRAGMENT_BINDABLE_UNIFORMS_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public const int MAX_FRAGMENT_BINDABLE_UNIFORMS_EXT = 0x8DE3;

		/// <summary>
		/// [GL] Value of GL_MAX_GEOMETRY_BINDABLE_UNIFORMS_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public const int MAX_GEOMETRY_BINDABLE_UNIFORMS_EXT = 0x8DE4;

		/// <summary>
		/// [GL] Value of GL_MAX_BINDABLE_UNIFORM_SIZE_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public const int MAX_BINDABLE_UNIFORM_SIZE_EXT = 0x8DED;

		/// <summary>
		/// [GL] Value of GL_UNIFORM_BUFFER_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public const int UNIFORM_BUFFER_EXT = 0x8DEE;

		/// <summary>
		/// [GL] Value of GL_UNIFORM_BUFFER_BINDING_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public const int UNIFORM_BUFFER_BINDING_EXT = 0x8DEF;

		/// <summary>
		/// [GL] glUniformBufferEXT: Binding for glUniformBufferEXT.
		/// </summary>
		/// <param name="program">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="location">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="buffer">
		/// A <see cref="T:uint"/>.
		/// </param>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public static void UniformBufferEXT(uint program, int location, uint buffer)
		{
			Debug.Assert(Delegates.pglUniformBufferEXT != null, "pglUniformBufferEXT not implemented");
			Delegates.pglUniformBufferEXT(program, location, buffer);
			LogCommand("glUniformBufferEXT", null, program, location, buffer			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glGetUniformBufferSizeEXT: Binding for glGetUniformBufferSizeEXT.
		/// </summary>
		/// <param name="program">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="location">
		/// A <see cref="T:int"/>.
		/// </param>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public static int GetUniformBufferSizeEXT(uint program, int location)
		{
			int retValue;

			Debug.Assert(Delegates.pglGetUniformBufferSizeEXT != null, "pglGetUniformBufferSizeEXT not implemented");
			retValue = Delegates.pglGetUniformBufferSizeEXT(program, location);
			LogCommand("glGetUniformBufferSizeEXT", retValue, program, location			);
			DebugCheckErrors(retValue);

			return (retValue);
		}

		/// <summary>
		/// [GL] glGetUniformOffsetEXT: Binding for glGetUniformOffsetEXT.
		/// </summary>
		/// <param name="program">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="location">
		/// A <see cref="T:int"/>.
		/// </param>
		[RequiredByFeature("GL_EXT_bindable_uniform")]
		public static IntPtr GetUniformOffsetEXT(uint program, int location)
		{
			IntPtr retValue;

			Debug.Assert(Delegates.pglGetUniformOffsetEXT != null, "pglGetUniformOffsetEXT not implemented");
			retValue = Delegates.pglGetUniformOffsetEXT(program, location);
			LogCommand("glGetUniformOffsetEXT", retValue, program, location			);
			DebugCheckErrors(retValue);

			return (retValue);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_EXT_bindable_uniform")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glUniformBufferEXT(uint program, int location, uint buffer);

			[RequiredByFeature("GL_EXT_bindable_uniform")]
			[ThreadStatic]
			internal static glUniformBufferEXT pglUniformBufferEXT;

			[RequiredByFeature("GL_EXT_bindable_uniform")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate int glGetUniformBufferSizeEXT(uint program, int location);

			[RequiredByFeature("GL_EXT_bindable_uniform")]
			[ThreadStatic]
			internal static glGetUniformBufferSizeEXT pglGetUniformBufferSizeEXT;

			[RequiredByFeature("GL_EXT_bindable_uniform")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate IntPtr glGetUniformOffsetEXT(uint program, int location);

			[RequiredByFeature("GL_EXT_bindable_uniform")]
			[ThreadStatic]
			internal static glGetUniformOffsetEXT pglGetUniformOffsetEXT;

		}
	}

}

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
		/// [GL] Value of GL_FOG_FUNC_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_fog_function")]
		public const int FOG_FUNC_SGIS = 0x812A;

		/// <summary>
		/// [GL] Value of GL_FOG_FUNC_POINTS_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_fog_function")]
		public const int FOG_FUNC_POINTS_SGIS = 0x812B;

		/// <summary>
		/// [GL] Value of GL_MAX_FOG_FUNC_POINTS_SGIS symbol.
		/// </summary>
		[RequiredByFeature("GL_SGIS_fog_function")]
		public const int MAX_FOG_FUNC_POINTS_SGIS = 0x812C;

		/// <summary>
		/// [GL] glFogFuncSGIS: Binding for glFogFuncSGIS.
		/// </summary>
		/// <param name="points">
		/// A <see cref="T:float[]"/>.
		/// </param>
		[RequiredByFeature("GL_SGIS_fog_function")]
		public static void FogFuncSGIS(float[] points)
		{
			Debug.Assert(points.Length > 0 && (points.Length % 2) == 0, "empty or not multiple of 2");
			unsafe {
				fixed (float* p_points = points)
				{
					Debug.Assert(Delegates.pglFogFuncSGIS != null, "pglFogFuncSGIS not implemented");
					Delegates.pglFogFuncSGIS(points.Length / 2, p_points);
					LogCommand("glFogFuncSGIS", null, points.Length / 2, points					);
				}
			}
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glGetFogFuncSGIS: Binding for glGetFogFuncSGIS.
		/// </summary>
		/// <param name="points">
		/// A <see cref="T:float[]"/>.
		/// </param>
		[RequiredByFeature("GL_SGIS_fog_function")]
		public static void GetFogFuncSGIS([Out] float[] points)
		{
			unsafe {
				fixed (float* p_points = points)
				{
					Debug.Assert(Delegates.pglGetFogFuncSGIS != null, "pglGetFogFuncSGIS not implemented");
					Delegates.pglGetFogFuncSGIS(p_points);
					LogCommand("glGetFogFuncSGIS", null, points					);
				}
			}
			DebugCheckErrors(null);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_SGIS_fog_function")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glFogFuncSGIS(int n, float* points);

			[RequiredByFeature("GL_SGIS_fog_function")]
			[ThreadStatic]
			internal static glFogFuncSGIS pglFogFuncSGIS;

			[RequiredByFeature("GL_SGIS_fog_function")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glGetFogFuncSGIS(float* points);

			[RequiredByFeature("GL_SGIS_fog_function")]
			[ThreadStatic]
			internal static glGetFogFuncSGIS pglGetFogFuncSGIS;

		}
	}

}
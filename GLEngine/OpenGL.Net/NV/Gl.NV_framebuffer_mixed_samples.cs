
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
		/// [GL] Value of GL_COVERAGE_MODULATION_TABLE_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public const int COVERAGE_MODULATION_TABLE_NV = 0x9331;

		/// <summary>
		/// [GL] Value of GL_DEPTH_SAMPLES_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public const int DEPTH_SAMPLES_NV = 0x932D;

		/// <summary>
		/// [GL] Value of GL_STENCIL_SAMPLES_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public const int STENCIL_SAMPLES_NV = 0x932E;

		/// <summary>
		/// [GL] Value of GL_MIXED_DEPTH_SAMPLES_SUPPORTED_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public const int MIXED_DEPTH_SAMPLES_SUPPORTED_NV = 0x932F;

		/// <summary>
		/// [GL] Value of GL_MIXED_STENCIL_SAMPLES_SUPPORTED_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public const int MIXED_STENCIL_SAMPLES_SUPPORTED_NV = 0x9330;

		/// <summary>
		/// [GL] Value of GL_COVERAGE_MODULATION_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public const int COVERAGE_MODULATION_NV = 0x9332;

		/// <summary>
		/// [GL] Value of GL_COVERAGE_MODULATION_TABLE_SIZE_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public const int COVERAGE_MODULATION_TABLE_SIZE_NV = 0x9333;

		/// <summary>
		/// [GL] glCoverageModulationTableNV: Binding for glCoverageModulationTableNV.
		/// </summary>
		/// <param name="v">
		/// A <see cref="T:float[]"/>.
		/// </param>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public static void CoverageModulationTableNV(float[] v)
		{
			unsafe {
				fixed (float* p_v = v)
				{
					Debug.Assert(Delegates.pglCoverageModulationTableNV != null, "pglCoverageModulationTableNV not implemented");
					Delegates.pglCoverageModulationTableNV(v.Length, p_v);
					LogCommand("glCoverageModulationTableNV", null, v.Length, v					);
				}
			}
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glGetCoverageModulationTableNV: Binding for glGetCoverageModulationTableNV.
		/// </summary>
		/// <param name="bufsize">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="v">
		/// A <see cref="T:float[]"/>.
		/// </param>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public static void GetCoverageModulationTableNV(int bufsize, [Out] float[] v)
		{
			unsafe {
				fixed (float* p_v = v)
				{
					Debug.Assert(Delegates.pglGetCoverageModulationTableNV != null, "pglGetCoverageModulationTableNV not implemented");
					Delegates.pglGetCoverageModulationTableNV(bufsize, p_v);
					LogCommand("glGetCoverageModulationTableNV", null, bufsize, v					);
				}
			}
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glCoverageModulationNV: Binding for glCoverageModulationNV.
		/// </summary>
		/// <param name="components">
		/// A <see cref="T:int"/>.
		/// </param>
		[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
		public static void CoverageNV(int components)
		{
			Debug.Assert(Delegates.pglCoverageModulationNV != null, "pglCoverageModulationNV not implemented");
			Delegates.pglCoverageModulationNV(components);
			LogCommand("glCoverageModulationNV", null, components			);
			DebugCheckErrors(null);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glCoverageModulationTableNV(int n, float* v);

			[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
			[ThreadStatic]
			internal static glCoverageModulationTableNV pglCoverageModulationTableNV;

			[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glGetCoverageModulationTableNV(int bufsize, float* v);

			[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
			[ThreadStatic]
			internal static glGetCoverageModulationTableNV pglGetCoverageModulationTableNV;

			[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glCoverageModulationNV(int components);

			[RequiredByFeature("GL_NV_framebuffer_mixed_samples", Api = "gl|glcore|gles2")]
			[ThreadStatic]
			internal static glCoverageModulationNV pglCoverageModulationNV;

		}
	}

}

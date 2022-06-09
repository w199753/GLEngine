
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
	public partial class Glx
	{
		/// <summary>
		/// [GLX] glXBindSwapBarrierSGIX: Binding for glXBindSwapBarrierSGIX.
		/// </summary>
		/// <param name="dpy">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		/// <param name="drawable">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		/// <param name="barrier">
		/// A <see cref="T:int"/>.
		/// </param>
		[RequiredByFeature("GLX_SGIX_swap_barrier")]
		public static void BindSwapBarrierSGIX(IntPtr dpy, IntPtr drawable, int barrier)
		{
			Debug.Assert(Delegates.pglXBindSwapBarrierSGIX != null, "pglXBindSwapBarrierSGIX not implemented");
			Delegates.pglXBindSwapBarrierSGIX(dpy, drawable, barrier);
			LogCommand("glXBindSwapBarrierSGIX", null, dpy, drawable, barrier			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GLX] glXQueryMaxSwapBarriersSGIX: Binding for glXQueryMaxSwapBarriersSGIX.
		/// </summary>
		/// <param name="dpy">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		/// <param name="screen">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="max">
		/// A <see cref="T:int[]"/>.
		/// </param>
		[RequiredByFeature("GLX_SGIX_swap_barrier")]
		public static bool QueryMaxSwapBarriersSGIX(IntPtr dpy, int screen, int[] max)
		{
			bool retValue;

			unsafe {
				fixed (int* p_max = max)
				{
					Debug.Assert(Delegates.pglXQueryMaxSwapBarriersSGIX != null, "pglXQueryMaxSwapBarriersSGIX not implemented");
					retValue = Delegates.pglXQueryMaxSwapBarriersSGIX(dpy, screen, p_max);
					LogCommand("glXQueryMaxSwapBarriersSGIX", retValue, dpy, screen, max					);
				}
			}
			DebugCheckErrors(retValue);

			return (retValue);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GLX_SGIX_swap_barrier")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glXBindSwapBarrierSGIX(IntPtr dpy, IntPtr drawable, int barrier);

			[RequiredByFeature("GLX_SGIX_swap_barrier")]
			internal static glXBindSwapBarrierSGIX pglXBindSwapBarrierSGIX;

			[RequiredByFeature("GLX_SGIX_swap_barrier")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate bool glXQueryMaxSwapBarriersSGIX(IntPtr dpy, int screen, int* max);

			[RequiredByFeature("GLX_SGIX_swap_barrier")]
			internal static glXQueryMaxSwapBarriersSGIX pglXQueryMaxSwapBarriersSGIX;

		}
	}

}

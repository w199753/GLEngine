
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
		/// [GL] glBufferStorageExternalEXT: Binding for glBufferStorageExternalEXT.
		/// </summary>
		/// <param name="target">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="offset">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		/// <param name="size">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="clientBuffer">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		/// <param name="flags">
		/// A <see cref="T:MapBufferUsageMask"/>.
		/// </param>
		[RequiredByFeature("GL_EXT_external_buffer", Api = "gl|gles2")]
		public static void BufferStorageEXT(int target, IntPtr offset, uint size, IntPtr clientBuffer, MapBufferUsageMask flags)
		{
			Debug.Assert(Delegates.pglBufferStorageExternalEXT != null, "pglBufferStorageExternalEXT not implemented");
			Delegates.pglBufferStorageExternalEXT(target, offset, size, clientBuffer, (uint)flags);
			LogCommand("glBufferStorageExternalEXT", null, target, offset, size, clientBuffer, flags			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glNamedBufferStorageExternalEXT: Binding for glNamedBufferStorageExternalEXT.
		/// </summary>
		/// <param name="buffer">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="offset">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		/// <param name="size">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="clientBuffer">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		/// <param name="flags">
		/// A <see cref="T:MapBufferUsageMask"/>.
		/// </param>
		[RequiredByFeature("GL_EXT_external_buffer", Api = "gl|gles2")]
		public static void NamedBufferStorageEXT(uint buffer, IntPtr offset, uint size, IntPtr clientBuffer, MapBufferUsageMask flags)
		{
			Debug.Assert(Delegates.pglNamedBufferStorageExternalEXT != null, "pglNamedBufferStorageExternalEXT not implemented");
			Delegates.pglNamedBufferStorageExternalEXT(buffer, offset, size, clientBuffer, (uint)flags);
			LogCommand("glNamedBufferStorageExternalEXT", null, buffer, offset, size, clientBuffer, flags			);
			DebugCheckErrors(null);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_EXT_external_buffer", Api = "gl|gles2")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glBufferStorageExternalEXT(int target, IntPtr offset, uint size, IntPtr clientBuffer, uint flags);

			[RequiredByFeature("GL_EXT_external_buffer", Api = "gl|gles2")]
			[ThreadStatic]
			internal static glBufferStorageExternalEXT pglBufferStorageExternalEXT;

			[RequiredByFeature("GL_EXT_external_buffer", Api = "gl|gles2")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glNamedBufferStorageExternalEXT(uint buffer, IntPtr offset, uint size, IntPtr clientBuffer, uint flags);

			[RequiredByFeature("GL_EXT_external_buffer", Api = "gl|gles2")]
			[ThreadStatic]
			internal static glNamedBufferStorageExternalEXT pglNamedBufferStorageExternalEXT;

		}
	}

}

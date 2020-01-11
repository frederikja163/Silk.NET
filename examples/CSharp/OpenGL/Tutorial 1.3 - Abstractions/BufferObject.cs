using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorial
{
    public class BufferObject<TDataType> : IDisposable
        where TDataType : unmanaged
    {
        private uint _handle;
        private GLEnum _bufferType;
        private GL _gl;

        public unsafe BufferObject(GL gl, TDataType[] data, GLEnum bufferType)
        {
            _gl = gl;
            _bufferType = bufferType;

            _handle = _gl.GenBuffer();
            Bind();
            fixed (void* d = data)
            {
                _gl.BufferData(bufferType, (UIntPtr)(data.Length * sizeof(TDataType)), d, GLEnum.StaticDraw);
            }
        }

        public void Bind()
        {
            _gl.BindBuffer(_bufferType, _handle);
        }

        public void Dispose()
        {
            _gl.DeleteBuffer(_handle);
        }
    }
}
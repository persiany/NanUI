﻿// THIS FILE IS PART OF WinFormium PROJECT
// THE WinFormium PROJECT IS AN OPENSOURCE LIBRARY LICENSED UNDER THE MIT License.
// COPYRIGHTS (C) Xuanchen Lin. ALL RIGHTS RESERVED.
//
// GITHUB: https://github.com/XuanchenLin/WinFormium
// EMail: xuanchenlin(at)msn.com QQ:19843266 WECHAT:linxuanchen1985


using WinFormium.CefGlue.Interop;

namespace WinFormium.CefGlue;
/// <summary>
/// Structure representing cursor information. |buffer| will be
/// |size.width|*|size.height|*4 bytes in size and represents a BGRA image with
/// an upper-left origin.
/// </summary>
public unsafe sealed class CefCursorInfo
{
    private cef_cursor_info_t* _ptr;

    internal CefCursorInfo(cef_cursor_info_t* ptr)
    {
        _ptr = ptr;
    }

    internal void Dispose()
    {
        _ptr = null;
    }

    private void ThrowIfDisposed()
    {
        if (_ptr == null) throw new ObjectDisposedException("CefCursorInfo");
    }

    public CefPoint HotSpot
    {
        get
        {
            ThrowIfDisposed();
            return new CefPoint(_ptr->hotspot.x, _ptr->hotspot.y);
        }
    }

    public float ImageScaleFactor
    {
        get
        {
            ThrowIfDisposed();
            return _ptr->image_scale_factor;
        }
    }

    public byte[] GetBuffer()
    {
        ThrowIfDisposed();
        var bufferLength = _ptr->size.width * _ptr->size.height * 4;
        var bytes = new byte[bufferLength];
        Marshal.Copy((IntPtr)_ptr->buffer, bytes, 0, bufferLength);
        return bytes;
    }

    public CefSize Size
    {
        get
        {
            ThrowIfDisposed();
            return new CefSize(_ptr->size.width, _ptr->size.height);
        }
    }
}
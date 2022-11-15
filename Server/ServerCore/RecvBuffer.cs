using System;
using System.Collections.Generic;
using System.Text;

namespace ServerCore
{
	public class RecvBuffer
	{
		// [r][][w][][][][][][][]
		ArraySegment<byte> _buffer;
		//읽고있는커서
		int _readPos;
		//쓰는커서
		int _writePos;

		public RecvBuffer(int bufferSize)
		{
			_buffer = new ArraySegment<byte>(new byte[bufferSize], 0, bufferSize);
		}

		public int DataSize { get { return _writePos - _readPos; } }
		public int FreeSize { get { return _buffer.Count - _writePos; } }

		//데이터범위
		public ArraySegment<byte> ReadSegment
		{
			get { return new ArraySegment<byte>(_buffer.Array, _buffer.Offset + _readPos, DataSize); }
		}

		//유효범위
		public ArraySegment<byte> WriteSegment
		{
			get { return new ArraySegment<byte>(_buffer.Array, _buffer.Offset + _writePos, FreeSize); }
		}

		public void Clean()
		{
			int dataSize = DataSize;
			if (dataSize == 0)
			{
				// 남은 데이터가 없으면 복사하지 않고 커서 위치만 리셋
				_readPos = _writePos = 0;
			}
			else
			{
				// 남은게 있으면 시작 위치로 복사
				Array.Copy(_buffer.Array, _buffer.Offset + _readPos, _buffer.Array, _buffer.Offset, dataSize);
				_readPos = 0;
				_writePos = dataSize;
			}
		}

		//컨텐츠 코드 성공적으로 처리하면 호출해서 readpos 위치 조정
		public bool OnRead(int numOfBytes)
		{
			if (numOfBytes > DataSize)
				return false;

			_readPos += numOfBytes;
			return true;
		}

		//클라에서 쏜 데이터 받아서 writepos 위치 조정
		public bool OnWrite(int numOfBytes)
		{
			if (numOfBytes > FreeSize)
				return false;

			_writePos += numOfBytes;
			return true;
		}
	}
}

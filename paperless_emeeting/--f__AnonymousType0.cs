using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

[CompilerGenerated]
internal sealed class _003C_003Ef__AnonymousType0<_003CID_003Ej__TPar>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly _003CID_003Ej__TPar _003CID_003Ei__Field;

	public _003CID_003Ej__TPar ID
	{
		get
		{
			return _003CID_003Ei__Field;
		}
	}

	[DebuggerHidden]
	public _003C_003Ef__AnonymousType0(_003CID_003Ej__TPar ID)
	{
		_003CID_003Ei__Field = ID;
	}

	[DebuggerHidden]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("{ ID = ");
		stringBuilder.Append(_003CID_003Ei__Field);
		stringBuilder.Append(" }");
		return stringBuilder.ToString();
	}

	[DebuggerHidden]
	public override bool Equals(object value)
	{
		_003C_003Ef__AnonymousType0<_003CID_003Ej__TPar> anon = value as _003C_003Ef__AnonymousType0<_003CID_003Ej__TPar>;
		if (anon != null)
		{
			return EqualityComparer<_003CID_003Ej__TPar>.Default.Equals(_003CID_003Ei__Field, anon._003CID_003Ei__Field);
		}
		return false;
	}

	[DebuggerHidden]
	public override int GetHashCode()
	{
		int num = 1534793807;
		return -1521134295 * num + EqualityComparer<_003CID_003Ej__TPar>.Default.GetHashCode(_003CID_003Ei__Field);
	}
}

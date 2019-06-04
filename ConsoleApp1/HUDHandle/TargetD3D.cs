using System;
using Decal.Adapter.Wrappers;

namespace Defiance.HUDHandle
{
	public class TargetD3D
	{
		public int Id { get; set; }
		public D3DObj D3DObject { get; set; }
		public int Icon { get; set; }
		public TargetD3D()
		{
			this.Id = 0;
			this.D3DObject = null;
			this.Icon = 0;
		}
	}
}

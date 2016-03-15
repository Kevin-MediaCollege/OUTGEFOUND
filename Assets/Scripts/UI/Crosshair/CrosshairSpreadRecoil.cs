using UnityEngine;
using System.Collections;

public class CrosshairSpreadRecoil : CrosshairSpreadComponent
{
	public override float Spread
	{
		get
		{
			if(AssignFirearm())
			{
				return firearm.RecoilOffset;
			}

			return 0;
		}
	}

	private Entity entity;
	private Firearm firearm;

	private bool AssignFirearm()
	{
		if(firearm != null)
		{
			return true;
		}

		if(entity == null)
		{
			entity = EntityUtils.GetEntityWithTag("Player");

			if(entity == null)
			{
				return false;
			}
		}

		firearm = entity.GetComponentInChildren<Firearm>();
		return firearm != null;
	}
}
using UnityEngine;

namespace Section1
{
	public class Dragon : EnemyBase
	{
		public override string GetBreedName() => "ドラゴン";
		public override string GetAttackMessage() => "{0}は{1}に炎を吐いた！";
		public override WeaponType GetWeaknesses() => WeaponType.Spear;
	}
}
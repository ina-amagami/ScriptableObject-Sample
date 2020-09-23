using UnityEngine;

namespace Section1
{
	public class Golem : EnemyBase
	{
		public override string GetBreedName() => "ゴーレム";
		public override string GetAttackMessage() => "{0}は{1}に腕を振り下ろす！";
		public override WeaponType GetWeaknesses() => WeaponType.Axe;
	}
}
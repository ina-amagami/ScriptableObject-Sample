using UnityEngine;

namespace Section1
{
	public class Slime : EnemyBase
	{
		public override string GetBreedName() => "スライム";
		public override string GetAttackMessage() => "{0}は{1}に飛びかかった！";
		public override WeaponType GetWeaknesses() => WeaponType.Sword;
	}
}
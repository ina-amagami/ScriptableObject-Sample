using UnityEngine;

namespace Section3
{
	[CreateAssetMenu(menuName = "ScriptableObject/Breed(Section3)")]
    public class Breed : ScriptableObject
    {
		/// <summary>
		/// 系統名
		/// </summary>
		public string Name => _Name;
		[SerializeField] private string _Name = "";

		/// <summary>
		/// 攻撃メッセージ
		/// </summary>
		public string AttackMessage => _AttackMessage;
		[SerializeField] private string _AttackMessage = "";

		/// <summary>
		/// 弱点武器
		/// </summary>
		public WeaponType Weaknesses => _Weaknesses;
		[SerializeField] private WeaponType _Weaknesses = WeaponType.None;
	}
}
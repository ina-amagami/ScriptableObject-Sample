using UnityEngine;

namespace Section4
{
	[CreateAssetMenu(menuName = "ScriptableObject/Breed(Section4)")]
    public class Breed : ScriptableObject
	{
		/// <summary>
		/// 親系統
		/// </summary>
		[SerializeField] private Breed _Parent = null;

		/// <summary>
		/// 系統名
		/// </summary>
		public string Name
		{
			get
			{
				// UnityEngine.Objectクラスを継承したものは != null 不要
				if (_Parent)
				{
					return _Parent.Name;
				}
				return _Name;
			}
		}
		[SerializeField] private string _Name = "";

		/// <summary>
		/// 攻撃メッセージ
		/// </summary>
		public string AttackMessage
		{
			get
			{
				if (_OverrideAttackMessage)
				{
					return _AttackMessage;
				}
				else if (_Parent)
				{
					return _Parent.AttackMessage;
				}
				return _AttackMessage;
			}
		}
		[SerializeField] private bool _OverrideAttackMessage = false;
		[SerializeField] private string _AttackMessage = "";

		/// <summary>
		/// 弱点武器
		/// </summary>
		public WeaponType Weaknesses
		{
			get
			{
				if (_OverrideWeaknesses)
				{
					return _Weaknesses;
				}
				else if (_Parent)
				{
					return _Parent._Weaknesses;
				}
				return _Weaknesses;
			}
		}
		[SerializeField] private bool _OverrideWeaknesses = false;
		[SerializeField] private WeaponType _Weaknesses = WeaponType.None;
	}
}
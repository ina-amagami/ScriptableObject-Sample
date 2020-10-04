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
					// ここで「Name」を取得しているので
					// "親系統が更に親系統を持っていても" 最終的な親から系統名を取得できる
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
				if (_OverrideAttackMessage || !_Parent)
				{
					return _AttackMessage;
				}
				return _Parent.AttackMessage;
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
				if (_OverrideWeaknesses || !_Parent)
				{
					return _Weaknesses;
				}
				return _Parent._Weaknesses;
			}
		}
		[SerializeField] private bool _OverrideWeaknesses = false;
		[SerializeField] private WeaponType _Weaknesses = WeaponType.None;

#if UNITY_EDITOR
		private void OnValidate()
		{
			ValidateParent();
		}

		/// <summary>
		/// 【重要】無限ループ防止
		/// </summary>
		public void ValidateParent()
		{
			if (!_Parent) { return; }

			// 自身への参照が見つかったらその親は使えない
			Breed current = _Parent;
			while (current)
			{
				if (current == this)
				{
					_Parent = null;
					Debug.LogError("[Breed] 無効な親系統です");
					break;
				}
				current = current._Parent;
			}
		}
#endif
	}
}
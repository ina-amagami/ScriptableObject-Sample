using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Section1
{
	public abstract class EnemyBase : MonoBehaviour
	{
		/// <summary>
		/// 表示名
		/// </summary>
		public string DisplayName => _DisplayName;
		[SerializeField] private string _DisplayName = "";

		/// <summary>
		/// 最大HP
		/// </summary>
		public int MaxHp => _MaxHp;
		[SerializeField] private int _MaxHp = 0;

		/// <summary>
		/// 現在のHP
		/// </summary>
		public int Hp { get; protected set; }

		/// <summary>
		/// 攻撃力
		/// </summary>
		public int Atk => _Atk;
		[SerializeField] private int _Atk = 0;

		/// <summary>
		/// 系統名を取得
		/// </summary>
		public abstract string GetBreedName();

		/// <summary>
		/// 攻撃メッセージを取得
		/// </summary>
		public abstract string GetAttackMessage();

		/// <summary>
		/// 弱点武器を取得
		/// </summary>
		public abstract WeaponType GetWeaknesses();

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Vector3 basePos = transform.position;

			// 名前
			GUIStyle nameStyle = new GUIStyle();
			nameStyle.normal.textColor = Color.black;
			nameStyle.alignment = TextAnchor.MiddleCenter;
			nameStyle.fixedWidth = 100f;
			Handles.Label(basePos, DisplayName, nameStyle);

			// ステータス
			GUIStyle statusStyle = new GUIStyle();
			statusStyle.normal.textColor = Color.black;
			statusStyle.alignment = TextAnchor.UpperLeft;
			Handles.Label(basePos + new Vector3(1.8f, 2.5f),
				$"【最大ＨＰ】{MaxHp}\n" +
				$"【攻撃力　】{Atk}\n\n" +
				$"【系統名　】{GetBreedName()}\n" +
				$"【弱点武器】{GetWeaknesses()}\n\n" +
				$"【攻撃メッセージ】\n " + GetAttackMessage(), statusStyle);
		}
#endif
	}
}

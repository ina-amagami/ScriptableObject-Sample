using UnityEngine;
using UnityEditor;

namespace Section4
{
	[CustomEditor(typeof(Breed))]
	public class BreedInspector : Editor
	{
		GUIContent parentLabel;
		SerializedProperty parentProp;

		GUIContent nameLabel;
		SerializedProperty nameProp;

		GUIContent overrideAttackMessageLabel;
		SerializedProperty overrideAttackMessageProp;

		GUIContent attackMessageLabel;
		SerializedProperty attackMessageProp;

		GUIContent overrideWeaknessesLabel;
		SerializedProperty overrideWeaknessesProp;

		GUIContent weaknessesLabel;
		SerializedProperty weaknessesProp;

		private void OnEnable()
		{
			parentLabel = new GUIContent("親系統");
			parentProp = serializedObject.FindProperty("_Parent");

			nameLabel = new GUIContent("系統名");
			nameProp = serializedObject.FindProperty("_Name");

			overrideAttackMessageLabel = new GUIContent("攻撃メッセージを上書き");
			overrideAttackMessageProp = serializedObject.FindProperty("_OverrideAttackMessage");

			attackMessageLabel = new GUIContent("攻撃メッセージ");
			attackMessageProp = serializedObject.FindProperty("_AttackMessage");

			overrideWeaknessesLabel = new GUIContent("弱点武器を上書き");
			overrideWeaknessesProp = serializedObject.FindProperty("_OverrideWeaknesses");

			weaknessesLabel = new GUIContent("弱点武器");
			weaknessesProp = serializedObject.FindProperty("_Weaknesses");
		}

		public override void OnInspectorGUI()
		{
			var breed = target as Breed;

			// 最新データを取得
			serializedObject.Update();

			// 親系統
			using (var check = new EditorGUI.ChangeCheckScope())
			{
				EditorGUILayout.PropertyField(parentProp, parentLabel);
				if (check.changed)
				{
					// 一旦反映させて、無効な親系統でないかチェック
					serializedObject.ApplyModifiedProperties();
					breed.ValidateParent();
					serializedObject.Update();
				}
			}

			// ↓変更検知はこの書き方でもOK
			//EditorGUI.BeginChangeCheck();
			//EditorGUILayout.PropertyField(parentProp, parentLabel);
			//if (EditorGUI.EndChangeCheck())
			//{
			//	serializedObject.ApplyModifiedProperties();
			//	breed.ValidateParent();
			//	serializedObject.Update();
			//}

			Breed parent = parentProp.objectReferenceValue as Breed;
			if (!parent)
			{
				// 親系統を持たない場合、各パラメータの編集のみ
				EditorGUILayout.PropertyField(nameProp, nameLabel);
				EditorGUILayout.PropertyField(attackMessageProp, attackMessageLabel);
				EditorGUILayout.PropertyField(weaknessesProp, weaknessesLabel);
			}
			else
			{
				// 親系統の系統名を表示する
				EditorGUILayout.LabelField($"{nameLabel.text} : {parent.Name}");

				EditorGUILayout.Space();

				// 攻撃メッセージを上書きするか
				bool prevOverrideAttackMessage = overrideAttackMessageProp.boolValue;
				EditorGUILayout.PropertyField(overrideAttackMessageProp, overrideAttackMessageLabel);
				if (overrideAttackMessageProp.boolValue)
				{
					if (!prevOverrideAttackMessage)
					{
						// オフ → オンになった時に親の攻撃メッセージを取ってくる
						attackMessageProp.stringValue = parent.AttackMessage;
					}

					// 上書きをするなら書き換え
					EditorGUILayout.PropertyField(attackMessageProp, attackMessageLabel);
				}
				else
				{
					// 上書きしないなら親系統のものを表示する
					EditorGUILayout.LabelField($"{attackMessageLabel.text} : {parent.AttackMessage}");

					// 親系統のものを使うということは、このBreedが持つ攻撃メッセージは不要
					// データを節約しておく
					// ※ 編集の都合上、保持してくれていた方が嬉しいこともあるのでケースバイケース
					attackMessageProp.stringValue = string.Empty;
				}

				EditorGUILayout.Space();

				// 弱点武器を上書きするか
				EditorGUILayout.PropertyField(overrideWeaknessesProp, overrideWeaknessesLabel);
				if (overrideWeaknessesProp.boolValue)
				{
					// 上書きをするなら書き換え
					EditorGUILayout.PropertyField(weaknessesProp, weaknessesLabel);
				}
				else
				{
					// 上書きしないなら親系統のものを表示する
					EditorGUILayout.LabelField($"{weaknessesLabel.text} : {parent.Weaknesses}");
				}
			}

			// 変更点を反映させる
			serializedObject.ApplyModifiedProperties();
		}
	}
}
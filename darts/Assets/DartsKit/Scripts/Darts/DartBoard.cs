using UnityEngine;
using System.Collections;

namespace DartKit
	{
	public class DartBoard : MonoBehaviour 
	{

		//the dart fields
		private Vector3[] m_fields;

		//iterate through all the children of the dart board and add a dart field.
		void Start () {
			m_fields = new Vector3[20];
			for(int i=0; i<transform.childCount; i++)
			{

				DartField df = transform.GetChild(i).gameObject.AddComponent<DartField>();
				if(df.fieldType == DartField.FieldType.TRIPLE)
				{
					BoxCollider bc = df.gameObject.AddComponent<BoxCollider>();
					int index = ((df.score)/3)-1;
					m_fields[index] = bc.center;
	//				Debug.Log ("index" + index);
					Destroy(bc);
				}
			}
		}

		//center of the dart field used for around the CLOCK
		public Vector3 getFieldPosition(int index)
		{
			int fieldIndex = index-1;
			return m_fields[fieldIndex];
		}

	}
}
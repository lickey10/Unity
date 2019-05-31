using UnityEngine;
using System.Collections;

namespace DartKit
	{
	public class DartField : MonoBehaviour {

		//the score for the dartfield
		public int score;


		public enum FieldType
		{
			SINGLE,
			DOUBLE,
			TRIPLE,
			BULLSEYE,
			WIRE,
			UNKNOWN
		};


		//the type of field
		public FieldType fieldType;

		public void Awake()
		{
			//first get the score.
			if(name.Length>2)
			{
				string scoreStr = name.Substring(2,name.Length-2);
				score = int.Parse(scoreStr);
			}

			//then get the type of field.
			string prefix = name.Substring(0,1);
			if(prefix.Equals("b")){
				fieldType = FieldType.BULLSEYE;
			}
			else if(prefix.Equals("s")){
				fieldType = FieldType.SINGLE;
			}
			else if(prefix.Equals("d")){
				fieldType = FieldType.DOUBLE;
			}
			else if(prefix.Equals("t")){
				fieldType = FieldType.TRIPLE;
			}
			else if(prefix.Equals("w"))
			{
				fieldType = FieldType.WIRE;
			}else{
				fieldType = FieldType.UNKNOWN;
			}
		}

	}
}
using UnityEngine;
using System.Collections;

namespace DartKit
{
	public class Dart : MonoBehaviour {

		//the player who owns the dart
		public int playerIndex = 0;

		//the dart index
		public int dartIndex = 0;

		//the radius of the dartboard
		public float radiusOfDartBoard = 0.165f;

		//the speed at which the dart travels
		public float forceSpeed = 100;

		//the rotatoar script
		private Rotator m_rotator;

		//did we hit something
		private bool m_hitSomething = false;

		//the inital position
		private Vector3 m_initalPos;

		//the inital rotation
		private Quaternion m_initalRot;

		//the dart throwing angle
		public Vector3 dartThrowAngle = new Vector3(270,45f,0);

		//a ref to the dart game script
		private DartGameScript m_gameScript;

		//a ref to the dart board
		private DartBoard m_dartBoard;


		void Awake()
		{
			m_rotator = gameObject.GetComponent<Rotator>();
			m_initalPos = transform.position;
			m_initalRot = transform.rotation;
			m_gameScript= (DartGameScript)GameObject.FindObjectOfType(typeof(DartGameScript));
			m_dartBoard = (DartBoard)GameObject.FindObjectOfType(typeof(DartBoard));
		}
		void OnEnable()
		{
			BaseGameManager.onFireDart 	+= onFireDart;
	//		BaseGameManager.onPlayerTurn += onPlayerTurn;
			BaseGameManager.onResetDarts += onResetDarts;
		}
		void OnDisable()
		{
		//	BaseGameManager.onPlayerTurn -= onPlayerTurn;
			BaseGameManager.onFireDart 	-= onFireDart;
			BaseGameManager.onResetDarts -= onResetDarts;

		}


		public void onResetDarts()
		{
			transform.position = m_initalPos;
			transform.rotation = m_initalRot;
			m_hitSomething=false;
			if(m_rotator)
			{
				m_rotator.enabled=true;
			}
		}



		void OnCollisionEnter(Collision col) {
		
			if(m_hitSomething==false)
			{
				DartField dartField = col.gameObject.GetComponent<DartField>();
				GetComponent<Rigidbody>().isKinematic=true;
				m_hitSomething = true;
				//m_rotator.enabled=true;
				if(dartField)
				{
					BaseGameManager.dartHitBoard(dartField.score,
					                               dartField.fieldType);
				}
			}
		}
		private static int INDEX=0;


		public void onFireDart(int di, float hrz, float vrt,int fieldIndex)
		{
			if(di == dartIndex)
			{
				if(m_rotator)
				{
					m_rotator.enabled=false;
				}

				//convert from 0..1 to 1 to -1
				float x = 1f - (hrz*2f);
				float y = 1f - (vrt*2f);

				Vector3 offset = Vector3.zero;
				if(m_gameScript && m_gameScript.getGameType()==DartGameScript.GameType.AROUND_THE_WORLD)
				{
					offset = getOffset(fieldIndex);
				}


				Vector3 vec3 = Vector3.zero;// + offset;
				vec3.y = y;;
				vec3.x = -x;// * radiusOfDartBoard;
				if(vec3.sqrMagnitude>1)
				{
					vec3.Normalize();
				}
				vec3.x *= radiusOfDartBoard;
				vec3.y *= radiusOfDartBoard;
				vec3.z = -0.2732011f;


				transform.position = vec3 + offset;
				transform.rotation = Quaternion.Euler(dartThrowAngle);
				GetComponent<Rigidbody>().isKinematic=false;

				GetComponent<Rigidbody>().AddForce(new Vector3(0,0,forceSpeed));

			}
		}

		Vector3 getOffset(int index)
		{
			Vector3 offset = Vector3.zero;
			if(m_dartBoard)
			{
				offset = m_dartBoard.getFieldPosition(index);
				offset.z=0;
			}
			return offset;
		}
	}
}

/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;
using DG.Tweening;
//using Vectrosity;

namespace AppAdvisory.ab
{
	public class DotManagerAB : MonoBehaviour 
	{
		public bool isOnCircle;
		public SpriteRenderer DotSprite;
		GameManagerAB GM;
		LineRenderer lr;
		Vector3[] posLR = new Vector3[2];
		public GameObject phare;

		void Awake()
		{
			lr = GetComponent<LineRenderer>();
			lr.enabled = false;
			GM = FindObjectOfType<GameManagerAB>();
			phare.GetComponent<SpriteRenderer>().color = GM.PhareColor;
			phare.SetActive(false);
			Reset ();
		}

		void Reset()
		{
			if (DotSprite == null)
				DotSprite = GetComponent<SpriteRenderer> ();

			DotSprite.color = GM.DotColor;


			GetComponent<Collider2D>().enabled = false;

			StopAllCoroutines ();

			if (GetComponent<Rigidbody2D>() == null)
				gameObject.AddComponent<Rigidbody2D> ();

			transform.localScale = Vector3.one;
		}

		public void Replace()
		{
			if (DotSprite == null)
			{
				DotSprite = GetComponent<SpriteRenderer> ();
			}
			DotSprite.color = GM.DotColor;

			GetComponent<Collider2D>().enabled = false;

			StopAllCoroutines ();

			if (GetComponent<Rigidbody2D>() == null)
				gameObject.AddComponent<Rigidbody2D> ();

			transform.localScale = Vector3.one;
		}

		public void SetParentShootedDot(Transform parent, bool doLine, bool isBottom)
		{
			transform.SetParent(parent);

			this.doLine = doLine;

			float rot = 180f;

			if(isBottom)
				rot = 0f;

			phare.transform.parent.eulerAngles = new Vector3(0, 0, rot);

			phare.gameObject.SetActive(true);

			var sc = phare.transform.localScale;
			var saveSC = sc.y;
			sc.y = 0;

			phare.transform.localScale = sc;

			phare.transform.DOScaleY(saveSC, 0.3f);
		}

		public void DOParentAndActivateLine(Vector3 target, Transform CircleBorder, bool isTop)
		{
			doLine = true;

			GetComponent<Collider2D>().isTrigger = false;
			GetComponent<Collider2D>().enabled = true;

			transform.position = target;
			transform.rotation = Quaternion.Euler (0, 0, 0);
			transform.parent = CircleBorder;

			transform.localScale = Vector3.one;

			if (!transform.parent.name.Contains("TOP"))
				return;
		}

		public bool m_doLine = false;

		public bool doLine
		{
			set
			{
				m_doLine = value;

				lr.enabled = value;
			}

			get
			{
				return m_doLine;
			}
		}



		void Update()
		{
			lr.material.color = DotSprite.color;

			if(doLine)
			{
				posLR[0] = Vector3.zero;
				posLR[1] = transform.position;

				lr.SetPositions(posLR);
			}
			else
			{
				posLR[0] = transform.position;
				posLR[1] = transform.position;

				lr.SetPositions(posLR);
			}
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			GameOverLogic (col.gameObject);
		}

		void OnCollisionEnter2D(Collision2D col)
		{
			GameOverLogic (col.gameObject);
		}

		void GameOverLogic(GameObject col)
		{
			if( !GameManagerAB.ISGameOver &&  col.name.Contains("Dot") )
				GameManagerAB.DOGameOver();
		}
	}
}
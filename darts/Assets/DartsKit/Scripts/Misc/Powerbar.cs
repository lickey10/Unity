using UnityEngine;
using System.Collections;
namespace DartKit
{
	public class Powerbar: MonoBehaviour
	{
		public GUITexture spinner;

		public enum State
		{
			IDLE,
			MOVE_UP,
			MOVE_DOWN,
			DONE
		};
		public State m_state;
		public float minPos;
		public float maxPos;

		public enum PowerbarType
		{
			HORZ,
			VERT
		};
		public PowerbarType powerbarType;

		private float m_time = 0;
		public float pingTime = 0;

		public float m_val;
		public void Start()
		{
			m_time=0.5f;
			m_state = State.MOVE_UP;
		}
		void updateGUI()
		{
			Rect pi = spinner.pixelInset;

			float val = m_time / pingTime;
			m_val=val;
//			slider.value = val;

			float n = maxPos + -minPos;
			if(powerbarType==PowerbarType.HORZ)
			{
				pi.x = minPos + val * n;
			}else{
				pi.y = minPos + val * n;
			}
			spinner.pixelInset = pi;
		}
		void Update()
		{
			switch(m_state)
			{
				case State.MOVE_UP:
					handleMoveUp();
				break;
				case State.MOVE_DOWN:
					handleMoveDown();
				break;
			}



			updateGUI();
		}
		public void press()
		{
			if(m_state!=State.DONE)
			{
				BaseGameManager.powerbarPress( this );
				m_state = State.DONE;
			}
		}
		public void reset()
		{
			m_time = .5f;
			m_state = State.MOVE_UP;
		}
		void handleMoveDown()
		{
			m_time-=Time.deltaTime;
			if(m_time<0)
			{
				m_time = 0;
				m_state = State.MOVE_UP;
			}
		}
		void handleMoveUp()
		{
			m_time+=Time.deltaTime;
			if(m_time>pingTime)
			{
				m_time = pingTime;
				m_state = State.MOVE_DOWN;
			}
		}
		public Powerbar.PowerbarType getPowerbarType()
		{
			return powerbarType;
		}
		public float getValueAsScalar()
		{
			return m_time / pingTime;
		}
	}

}

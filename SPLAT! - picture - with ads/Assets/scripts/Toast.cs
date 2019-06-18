using UnityEngine;

	/// <summary>
	/// A toast is a message displayed through the GUI that eventually dies.
	/// </summary>
	public class Toast : MonoBehaviour
	{
		#region Members
		protected string textToDisplay;
		protected Vector2 textPosition;
		protected Vector2 textDirection;
		protected float textSpeed;
		protected GUIStyle textGUIStyle;
		
		protected bool displayed = false;
		protected Vector2 textSize;
		#endregion
		
		#region Properties
		public string TextToDisplay
		{
			get { return this.textToDisplay; }
			set { this.textToDisplay = value; }
		}
		
		public Vector2 TextPosition
		{
			get { return this.textPosition; }
			set { this.textPosition = value; }
		}
		
		public Vector2 TextDirection
		{
			get { return this.textDirection; }
			set { this.textDirection = value; }
		}
		
		public float TextSpeed
		{
			get { return this.textSpeed; }
			set { this.textSpeed = value; }
		}
		
		public GUIStyle TextGUIStyle
		{
			get { return this.textGUIStyle; }
			set { this.textGUIStyle = value; }
		}
		
		public Rect TextRect
		{
			get
			{
				Rect tempRect = new Rect();
				
				tempRect.x = this.TextPosition.x;
				tempRect.y = this.TextPosition.y;
				
				Vector2 tempSize = this.TextGUIStyle.CalcSize(new GUIContent(this.TextToDisplay));
				
				tempRect.width = tempSize.x;
				tempRect.height = tempSize.y;
				
				return tempRect;
			}
		}
		#endregion
		
		#region Methods     
		public void OnGUI()
		{
			if (this.displayed)
			{
				this.DisplayToast();
			}
		}
		
		public void FixedUpdate()
		{
			if (this.displayed)
			{
				this.MoveToast();
			}
		}
		
		protected void DisplayToast()
		{
			GUI.Label(this.TextRect, new GUIContent(this.TextToDisplay), this.TextGUIStyle);
		}
		
		protected void MoveToast()
		{
			Vector2 textMoveVector = this.TextDirection * this.TextSpeed;
			
			this.TextPosition += textMoveVector;
		}
		
		
		#region Offsets
		protected void CalculateAnchorOffsets()
		{
			switch (this.TextGUIStyle.alignment)
			{
			case TextAnchor.UpperLeft:
				//DO NOTHING!
				break;
			case TextAnchor.MiddleLeft:
				this.OffsetUp_Half();
				break;
			case TextAnchor.LowerLeft:
				this.OffsetUp_Full();
				break;
			case TextAnchor.LowerRight:
				this.OffsetRight_Full();
				break;
			case TextAnchor.MiddleRight:
				this.OffsetUp_Half();
				this.OffsetRight_Full();
				break;
			case TextAnchor.UpperRight:
				this.OffsetRight_Full();
				break;
			case TextAnchor.LowerCenter:
				this.OffsetUp_Full();
				this.OffsetRight_Half();
				break;
			case TextAnchor.MiddleCenter:
				this.OffsetUp_Half();
				this.OffsetRight_Half();
				break;
			case TextAnchor.UpperCenter:
				this.OffsetRight_Half();
				break;
			}
		}
		
		private void OffsetRight_Full()
		{
			this.textPosition.x -= this.TextRect.width;
		}
		
		private void OffsetRight_Half()
		{
			this.textPosition.x -= (this.TextRect.width/2);
			
		}
		
		private void OffsetUp_Full()
		{
			this.textPosition.y -= this.TextRect.height;
		}
		
		private void OffsetUp_Half()
		{
			this.textPosition.y -= (this.TextRect.height / 2);
		}
		#endregion
		#endregion
		
		#region Events
		public void BeginDisplayingToast()
		{
			this.displayed = true;
			
			this.CalculateAnchorOffsets();
		}
		#endregion
	}


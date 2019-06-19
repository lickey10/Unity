	 var Paper : int = 0;
     var paperToWin : int = 30;		//number to win!
    
     function OnTriggerEnter( other : Collider )
      {
        	if (other.gameObject.tag == "Paper")
        	{
           	  	Paper += 1;
           	  	Debug.Log("A paper was picked up. Total papers = " + Paper);
           	  	Destroy(other.gameObject);
        	 }
      }
	 
     function OnGUI()
     {
         if (Paper < paperToWin)
         {
             GUI.Box(Rect((Screen.width/2)-100, 10, 200, 35), "" + Paper + " Papers");
         }
         else
         {
             GUI.Box(Rect((Screen.width/2)-100, 10, 200, 35), "All Papers Collected!");
         }
     }
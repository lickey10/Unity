using UnityEngine;
using System.Collections;
using SQLiteClass;
using System.Text;

public class QuestionsHandler : MonoBehaviour {
	int questionNumber;
	string questionsQuery;
	SQLiteBeer sqlLiteClass;

	// Use this for initialization
	void Start () {
		questionNumber = 0;

		sqlLiteClass = new SQLiteBeer();

		setupQuiz();

		//put startQuiz in onClick on a start button
		startQuiz();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public class Number1Fan_Main extends Activity
//		implements android.view.View.OnClickListener
//	{
//		static final int DIALOG_FAILED = 2;
//		static final int DIALOG_PROGRESS = 0;
//		static final int DIALOG_SUBMITTED = 1;
//		static final int RC_REQUEST = 10001;
//		static String SKU_20QUESTIONS_SET2 = "";
//		static String SKU_20QUESTIONS_SET3 = "";
//		static final String TAG = "MMJ";
//		Activity activity;
//		RadioButton correctAnswer;
		private ArrayList curQA;
		private ArrayList curQuestions;
		private ArrayList curResultMessages;
		int currentQuestionIndex;
//		View layout;
//		private DBAdapter mDbHelper;
//		IabHelper mHelper;
//		boolean mIsPremium;
//		boolean mSubscribedToInfiniteGas;
//		boolean needToShowInitialsPopup;
//		int numCorrectAnswers;
		int numQuestions;
//		boolean ownSet2;
//		boolean ownSet3;
//		double percentCorrect;
//		PopupWindow popup;
//		PopupWindow popupInitials;
//		boolean postScore;
		string qaQuery;
		
//		String resultsMessageQuery;
//		int retries;
//		int retryMax;
//		RadioGroup rg;
//		boolean scorePosted;
//		String scores;
//		private TextView txtQuestion;
//		private TextView txtQuestionCount;
//		private TextView txtQuestionNumber;
//		private TextView txtQuestionsRight;
//		private TextView txtResults;
//		
//		public Number1Fan_Main()
//		{
//			numCorrectAnswers = 0;
//			numQuestions = 50;
//			questionNumber = 0;
//			currentQuestionIndex = 0;
//			questionsQuery = "";
//			resultsMessageQuery = "select Response from Responses where cast(percentRangeLow as integer) <= $percentCorrect$ and cast(percentRangeHigh as integer) >= $percentCorrect$ ORDER BY RANDOM() limit 1";

//		}
//		
//		public void onCreate(Bundle bundle)
//		{
//			super.onCreate(bundle);
//			//        SKU_20QUESTIONS_SET2 = getString(R.string.sku_set_2);
//			//        SKU_20QUESTIONS_SET3 = getString(R.string.sku_set_3);
//			
//			try
//			{
//				activity = this;
//				setContentView(R.layout.welcome);
//				
//				displayAd();
//				
//				loadData();
//				setupQuiz();
//				
//				((Button)findViewById(R.id.btnStart)).setOnClickListener(new OnClickListener() {
//					public void onClick(View v) {
//						startQuiz();
//					}
//				});
//				
//				updateButtons();
//				return;
//			}
//			catch(Exception exception)
//			{
//				exception.printStackTrace();
//			}
//		}
//		
//		
//		private void addRadioButton(String text, boolean correct)
//		{
//			try
//			{
//				final RadioButton rb = new RadioButton(this);
//				rb.setText(text);
//				rb.setChecked(false);
//				rb.setTextColor(Color.BLUE);
//				rb.setTextSize(20);
//				rb.setTextAppearance(this, Typeface.BOLD);
//				rb.setOnClickListener(radio_listener);
//				rb.setShadowLayer(1, 0, 0, Color.WHITE);
//				
//				if(correct)
//					correctAnswer = rb;
//				
//				rg.addView(rb);
//			}
//			catch(Exception ex)
//			{
//				ex.printStackTrace();
//			}
//		}
//		
		private void displayNextQuestion() 
		{
			try
			{
				//boolean correct = false;
				questionNumber++;
				
				if(curQA == null)
					getQA();
				
//				if(rg == null)
//					rg = (RadioGroup)findViewById(R.id.theRadioGroup);
//				
//				rg.removeAllViews();
				
//				txtQuestion = (TextView) findViewById(R.id.Question);
//				txtQuestion.setText(curQA.getString(3).trim());
				
//				clsQuestion theQuestion = new clsQuestion();
//				theQuestion.AdditionalInfo = curQA.getString(7).trim();
//				theQuestion.imgQuestionImage = curQA.getBlob(8);
//				txtQuestion.setTag(theQuestion);
				
				//addRadioButton(curQA.getString(4), curQA.getString(5).contains("True"));
				
				//iterate the rest of the answers and display
//				while(curQA.moveToNext())		
//					addRadioButton(curQA.getString(4), curQA.getString(5).contains("True"));

			//iterate the rest of the answers and display
			foreach(Object[] currentQA in curQA)
			{
				
			}
				
				//set question number
//				txtQuestionNumber = (TextView)findViewById(R.id.questionNumber);
//				txtQuestionNumber.setText("Question #"+ questionNumber);
				
				//set number right so far
//				txtQuestionsRight = (TextView)findViewById(R.id.questionsRight);
//				txtQuestionsRight.setText(numCorrectAnswers +" correct answers");
			}
			catch(System.Exception ex)
			{
				//ex.printStackTrace();
			}
		}
		
		private void getQA()
		{
//			curQA = mDbHelper.FetchData(qaQuery.replace("$questionID$", curQuestions.getString(0)));
//			startManagingCursor(curQA);
//			curQA.getCount();
//			setTitle((new StringBuilder("#1 Fan - ")).append(curQA.getString(1).trim()).toString());
		}
		
		private void getQuestions()
		{
			curQuestions = sqlLiteClass.GetResults(questionsQuery.Replace("$NumberOfQuestions$", ""));
			numQuestions = curQuestions.Count;

			//			curQuestions = mDbHelper.FetchData(questionsQuery.replace("$NumberOfQuestions$", (new StringBuilder()).append(numQuestions).toString()));
//			startManagingCursor(curQuestions);
//			numQuestions = curQuestions.getCount();
		}
//		
		private void setupQuiz()
		{
			//String base64EncodedPublicKey = getString(R.string.public_key_1) + getString(R.string.public_key_2) + getString(R.string.public_key_3);
			
			try
			{
//				Log.d("MMJ", "Creating IAB helper.");
//				mHelper = new IabHelper(Number1Fan_Main.this, base64EncodedPublicKey);
//				mHelper.enableDebugLogging(false);
				
				questionsQuery = "SELECT q.ID";
				questionsQuery += " FROM Quizes AS qz INNER JOIN";
				questionsQuery += " (select * from questions order by id desc limit $NumberOfQuestions$) AS q ON qz.ID = q.quizID";
				questionsQuery += " ORDER BY RANDOM()";

				qaQuery = "SELECT q.ID, qz.Name, qz.Description, q.question, a.answer, a.correct, a.ID as _id, q.additionalInfo, q.rawImage";
				qaQuery += " FROM Quizes AS qz INNER JOIN";
				qaQuery += " Questions AS q ON qz.ID = q.quizID INNER JOIN";
				qaQuery += " Answers AS a ON q.ID = a.questionID";
				qaQuery += " WHERE (q.ID = '$questionID$') and length(a.answer) > 0";
				qaQuery += " ORDER BY RANDOM()";
//				mDbHelper = new DBAdapter(this);
//				mDbHelper.open();
			}
			catch(System.Exception sqlexception)
			{
				//sqlexception.printStackTrace();
			}
		}
	
		private void startQuiz()
		{
			try
			{
				//rg = null;
				questionNumber = 0;
				curQuestions = null;
				curQA = null;
				//numCorrectAnswers = 0;

				getQuestions();
				//txtQuestionCount = (TextView)findViewById(R.id.questionCount);

//				if(txtQuestionCount != null)
//					txtQuestionCount.setText((new StringBuilder()).append(numQuestions).toString());

				displayNextQuestion();

				return;
			}
			catch(System.Exception exception)
			{
				//exception.printStackTrace();
			}
		}
//		
//		private void updateButtons()
//		{
//			final String payload = "";
//			//        final Button btnBuySet2 = (Button)findViewById(R.id.btnBuySet2);
//			//        final Button btnBuySet3 = (Button)findViewById(R.id.btnBuySet3);
//			//        
//			//        if(!ownSet2)
//			//        {
//			//        	btnBuySet2.setOnClickListener(new OnClickListener() {
//			//    		    public void onClick(View v) {
//			//    		        mHelper.launchPurchaseFlow(Number1Fan_Main.this, SKU_20QUESTIONS_SET2, RC_REQUEST, 
//			//    		                mPurchaseFinishedListener, payload);
//			//    		        
//			////    		        ownSet2 = true;
//			//    		        //btnBuySet2.setEnabled(false);
//			////    		        numQuestions += 20;
//			//    		        appendLog("clicked buy set 2 button");
//			//    		    }
//			//    		});
//			//        } else
//			//        {
//			//        	btnBuySet2.setEnabled(false);
//			////            ownSet2 = true;
//			//        }
//			//        
//			//        if(!ownSet3)
//			//        {
//			//        	btnBuySet3.setOnClickListener(new OnClickListener() {
//			//    		    public void onClick(View v) {
//			//    		    	mHelper.launchPurchaseFlow(Number1Fan_Main.this, SKU_20QUESTIONS_SET3, RC_REQUEST, 
//			//    		                mPurchaseFinishedListener, payload);
//			//    		    	
//			////    		    	ownSet3 = true;
//			//    		    	//btnBuySet3.setEnabled(false);
//			////    		    	numQuestions += 20;
//			//    		    }
//			//    		});
//			//        } else
//			//        {
//			//        	btnBuySet3.setEnabled(false);
//			////            ownSet3 = true;
//			//            return;
//			//        }
//		}
//		
//		private OnClickListener radio_listener = new OnClickListener() {
//			public void onClick(View v) {
//				// Perform action on clicks
//				RadioButton rb = (RadioButton) v;
//				
//				if(rb == correctAnswer)
//				{
//					//String additionalInfo = (String)txtQuestion.getTag();
//					clsQuestion theQuestion = (clsQuestion)txtQuestion.getTag();
//					
//					View anchor = findViewById(R.id.popupAnchor);
//					
//					int[] location = new int[2];
//					
//					anchor.getLocationOnScreen(location);
//					Point	ptClickPosition = new Point();
//					ptClickPosition.x = location[0];
//					ptClickPosition.y = location[1];
//					
//					if(theQuestion != null && theQuestion.AdditionalInfo.trim().length() > 0)
//						showPopup(Number1Fan_Main.this, ptClickPosition, theQuestion.AdditionalInfo, theQuestion.imgQuestionImage);
//					
//					//Toast.makeText(Number1Fan_Main.this, "Correct Answer", Toast.LENGTH_SHORT).show();
//					
//					numCorrectAnswers++;
//				}
//				else
//				{
//					Toast toast = Toast.makeText(Number1Fan_Main.this, "Wrong Answer", Toast.LENGTH_SHORT);
//					LinearLayout toastLayout = (LinearLayout) toast.getView();
//					TextView toastTV = (TextView) toastLayout.getChildAt(0);
//					toastTV.setTextSize(35);
//					toast.show();
//				}
//				
//				try
//				{
//					if(curQuestions.moveToNext())
//					{
//						curQA = null;
//						displayNextQuestion();
//					}
//					else
//					{
//						//done with quiz
//						//Toast.makeText(QuizMaster_Main.this, "Results: ("+ numCorrectAnswers +"\\"+ numQuestions +") "+ (numCorrectAnswers/numQuestions), Toast.LENGTH_SHORT).show();
//						
//						try
//						{
//							setContentView(R.layout.results);
//							
//							displayAd();
//							
//							TextView txtResultsMessage = null;
//							
//							percentCorrect = (double)(((double)numCorrectAnswers)/((double)numQuestions)*100);
//							
//							String resultString = "Results: ("+ NumberFormat.getIntegerInstance().format(numCorrectAnswers) +"\\"+ NumberFormat.getIntegerInstance().format(numQuestions) +") "+ NumberFormat.getIntegerInstance().format(percentCorrect) +"%";
//							resultString += "\n\n\n\n";
//							
//							txtQuestion = (TextView) findViewById(R.id.Results);
//							txtQuestion.setText(resultString);
//							
//							try
//							{
//								curResultMessages = mDbHelper.FetchData(resultsMessageQuery.replace("$percentCorrect$", NumberFormat.getIntegerInstance().format(percentCorrect) +""));
//								
//								if(curResultMessages != null)
//								{
//									startManagingCursor(curResultMessages);
//									
//									txtResultsMessage = (TextView) findViewById(R.id.txtResultsMessage);
//									txtResultsMessage.setText(curResultMessages.getString(0));
//									
//									//hide resultsMessage until I get the db filled with results
//									txtResultsMessage.setVisibility(View.GONE);
//								}
//								
//								if(numQuestions == 50)
//								{
//									TextView txtBuyAllMessage = (TextView) findViewById(R.id.txtResultsBuyAllMessage);
//									
//									if(percentCorrect < 100)
//										txtBuyAllMessage.setText("Nice Try! The #1 Fan knows all the answers.");
//									else
//										txtBuyAllMessage.setText("WOW!! You truly are the #1 Fan!!");
//								}
//								
//								if(percentCorrect < 80)
//								{
//									//they got less than 80%
//									//show disappointed picture
//									//View myLayout = findViewById(R.id.theLayout);
//									//myLayout.setBackgroundResource(R.drawable.background);
//								}
//							}
//							catch(Exception ex)
//							{
//								ex.printStackTrace();
//							}
//							
//							final Button button = (Button) findViewById(R.id.btnRestart);
//							
//							button.setOnClickListener(new OnClickListener() {
//								public void onClick(View v) {
//									startQuiz();
//								}
//							});
//							
//							updateButtons();
//						}
//						catch(Exception ex)
//						{
//							ex.printStackTrace();
//						}
//						
//						//submit score to leaderboard
//						/*  try {
//						// this is where you should input your game's score
//						Score score = new Score(percentCorrect, null);
//
//						// set up an observer for our request
//						RequestControllerObserver observer = new RequestControllerObserver() {
//
//							//@Override
//							public void requestControllerDidFail(RequestController controller, Exception exception) {
//								// something went wrong... possibly no internet connection
//								dismissDialog(DIALOG_PROGRESS);
//								showDialog(DIALOG_FAILED);
//							}
//
//							// this method is called when the request succeeds
//							//@Override
//							public void requestControllerDidReceiveResponse(RequestController controller) {
//								// reset the text field to 0
//								//scoreField.setText("0");
//								// remove the progress dialog
//								dismissDialog(DIALOG_PROGRESS);
//								// show the success dialog
//								showDialog(DIALOG_SUBMITTED);
//								// alternatively, you may want to return to the main screen
//								// or start another round of the game at this point
//							}
//						};
//
//						// with the observer, we can create a ScoreController to submit the score
//						ScoreController scoreController = new ScoreController(observer);
//
//						// show a progress dialog while we are submitting
//						showDialog(DIALOG_PROGRESS);
//
//						// this is the call that submits the score
//						scoreController.submitScore(score);
//						// please note that the above method will return immediately and reports to
//						// the RequestControllerObserver when it's done/failed
//					} catch (Exception e) {
//						// TODO Auto-generated catch block
//						e.printStackTrace();
//					}*/
//						
//					}
//				}
//				catch(Exception ex)
//				{
//					ex.printStackTrace();
//				}
//			}
//		};
//	
//	}
//
}

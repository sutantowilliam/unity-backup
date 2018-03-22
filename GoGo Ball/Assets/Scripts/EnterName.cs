using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class EnterName : MonoBehaviour {
	private string conn;
	public GameOver gameOver;
	public InputField fieldName;
	private float playerScore;
	private float playerDistance;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (true);
		conn = "URI=file:" + Application.dataPath + "/StreamingAssets/GoGoBallDB.sqlite";
		playerScore = 328.5f;
		playerDistance = 446;
	}

	// Update is called once per frame
	void Update () {

	}

	public void ToogleEnterName(float score, float distance){
		playerScore = score;
		playerDistance = distance;
	}

	private void InsertScore(string name, float newScore, float newDistance) 
	{
		using(IDbConnection dbConnection = new SqliteConnection(conn)) 
		{
			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand()) 
			{
				string sqlQuery = String.Format("INSERT INTO HighScores(Name,Score,Distance) VALUES('{0}','{1}','{2}')", name, newScore, newDistance);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	public void SubmitScore() {
		if (fieldName.text != string.Empty) {
			InsertScore (fieldName.text, playerScore, playerDistance);
			Debug.Log ("name text: " + fieldName.text);
			fieldName.text = string.Empty;
			gameObject.SetActive (false);
			gameOver.ToogleGameOver (playerScore);
		}
	}
}

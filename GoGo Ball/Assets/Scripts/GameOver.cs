using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class GameOver : MonoBehaviour {
	private string conn;
	public Text scoreText, bestText, coinText, gotCoinsText;
	private float playerScore;
	private int playerCoins;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (true);
		conn = "URI=file:" + Application.dataPath + "/StreamingAssets/GoGoBallDB.sqlite";
		playerCoins = GetPlayerCoins () - ConvertScoresToCoins ();
		UpdateCoin (playerCoins);
		scoreText.text = playerScore.ToString();
		bestText.text = GetBestScore().ToString ();
		coinText.text = playerCoins.ToString ();
		gotCoinsText.text = "You got "+ ConvertScoresToCoins ().ToString () + " coins!";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private float GetBestScore() 
	{
		List<HighScore> highScores = new List<HighScore> ();
		using(IDbConnection dbConnection = new SqliteConnection(conn)) 
		{
			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand()) 
			{
				string sqlQuery = "SELECT * FROM HighScores";
				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader()) 
				{
					while (reader.Read()) 
					{
						highScores.Add(new HighScore(reader.GetInt32(0),reader.GetFloat(2),reader.GetString(1),reader.GetFloat(3)));
					}
					dbConnection.Close();
					reader.Close();
				}
			}
		}
		highScores.Sort ();
		return highScores [0].Score;
	}
		
	private int GetPlayerCoins() {
		int coin = 0;
		using (IDbConnection dbConn = new SqliteConnection(conn)) 
		{
			dbConn.Open ();

			using (IDbCommand dbCmd = dbConn.CreateCommand()) 
			{
				string sqlQuery = "SELECT Coin FROM Player WHERE ID = 1";
				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader()) 
				{
					while (reader.Read()) 
					{
						coin = reader.GetInt32 (0);
					}
					dbConn.Close();
					reader.Close();
				}
			}
		}
		return coin;
	}

	private int ConvertScoresToCoins() {
		return ((int)playerScore / 2);
	}

	private void UpdateCoin (int coin) {
		using (IDbConnection dbConn = new SqliteConnection(conn)) 
		{
			dbConn.Open ();

			using (IDbCommand dbCmd = dbConn.CreateCommand()) 
			{
				string sqlQuery = String.Format("UPDATE Player SET Coin = '{0}' WHERE ID = '1'", coin);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteNonQuery ();
				dbConn.Close ();
			}
		}
	}

	public void ToogleGameOver(float score){
		gameObject.SetActive (true);
		playerScore = score;
	}
}

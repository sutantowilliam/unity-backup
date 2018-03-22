using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class HighScoresManager : MonoBehaviour {

	private string conn;
	private List<HighScore> highScores = new List<HighScore> ();

	public Transform scoreParent;
	public GameObject scorePrefab;

	public int topRanks;

	// Use this for initialization
	void Start () {
		conn = "URI=file:" + Application.dataPath + "/StreamingAssets/GoGoBallDB.sqlite";
		ShowScores();
	}

	// Update is called once per frame
	void Update () {

	}

	private void GetScores() 
	{
		highScores.Clear ();

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
	}

	private void ShowScores()
	{
		GetScores ();
		for (int i = 0; i < topRanks; i++) {
			if (i <= highScores.Count - 1) {
				GameObject tmpObject = Instantiate (scorePrefab);
				HighScore tmpScore = highScores [i];
				tmpObject.GetComponent<HighScoreScript> ().SetScore (tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString());

				tmpObject.transform.SetParent (scoreParent);
				tmpObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
			}
		}
	}
}
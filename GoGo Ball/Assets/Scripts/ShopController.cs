using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

	private string conn;
	public Camera mainCamera;
	public Camera panelCamera;
	private Canvas ballCanvas;
	private GameObject ballPanel;
	private int ballId;
	private string ballName;
	private int ballStatus;
	private int ballPrice;
	private string ballDesc;
	public GameObject atomPlayer;
	public GameObject plainPlayer;
	public GameObject eyePlayer;
	public GameObject woodenPlayer;

	// Use this for initialization
	void Start () {
		conn = "URI=file:" + Application.dataPath + "/StreamingAssets/GoGoBallDB.sqlite";
		SetBallActive (GetIDBallInUse());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int ReadCoin() {
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

	void ReadBall(int ball_id) {
		using (IDbConnection dbConn = new SqliteConnection(conn)) 
		{
			dbConn.Open ();

			using (IDbCommand dbCmd = dbConn.CreateCommand()) 
			{
				string sqlQuery = String.Format("SELECT * FROM Balls WHERE ID = '{0}'", ball_id);
				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader()) 
				{
					while (reader.Read()) 
					{
						ballId = reader.GetInt32 (0);
						ballName = reader.GetString (1);
						ballStatus = reader.GetInt32 (2);
						ballPrice = reader.GetInt32 (3);
						ballDesc = reader.GetString (4);
					}
					dbConn.Close();
					reader.Close();
				}
			}
		}
	}

	int GetIDBallInUse() {
		/**
		using (IDbConnection dbConn = new SqliteConnection(conn)) 
		{
			dbConn.Open ();

			using (IDbCommand dbCmd = dbConn.CreateCommand()) 
			{
				string sqlQuery = String.Format("SELECT ID FROM Balls WHERE Status = '2'");
				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader()) 
				{
					while (reader.Read()) 
					{
						id = reader.GetInt32 (0);
					}
					dbConn.Close();
					reader.Close();
				}
			}
		}
		return id;
		**/
		string playerPrefs = PlayerPrefs.GetString ("ballType", "none");
		if (playerPrefs == "plain")
			return 0;
		else if (playerPrefs == "atom")
			return 1;
		else if (playerPrefs == "eye")
			return 2;
		else if (playerPrefs == "wooden")
			return 3;
		else
			return 99;
	}

	void UpdateBallStatus (int stat, int ball_id) {
		using (IDbConnection dbConn = new SqliteConnection(conn)) 
		{
			dbConn.Open ();

			using (IDbCommand dbCmd = dbConn.CreateCommand()) 
			{
				string sqlQuery = String.Format("UPDATE Balls SET Status = '{0}' WHERE ID = '{1}'", stat, ball_id);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteNonQuery ();
				dbConn.Close ();
			}
		}
	}

	void UpdateCoin (int coin) {
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

	void SetBallActive(int ball_id) {
		if (ball_id == 0) { 
			plainPlayer.SetActive (true);
			Debug.Log ("set active plain");
		} else if (ball_id == 1) {
			atomPlayer.SetActive (true);
			Debug.Log ("set active atom");
		} else if (ball_id == 2) {
			eyePlayer.SetActive (true);
			Debug.Log ("set active eye");
		} else if (ball_id == 3) {
			woodenPlayer.SetActive (true);
			Debug.Log ("set active wooden");
		}
	}

	void SetBallNotActive(int ball_id) {
		if (ball_id == 0) 
			plainPlayer.SetActive (false);
		else if (ball_id == 1) 
			atomPlayer.SetActive (false);
		else if (ball_id == 2) 
			eyePlayer.SetActive (false);
		else if (ball_id == 3) 
			woodenPlayer.SetActive (false);
	}

	public void shop() {
		int coin = ReadCoin();

		if (ballStatus == 0) {
			UpdateBallStatus (1, ballId);
			coin -= ballPrice;  //assume coin always >= price
			UpdateCoin (coin);
			LoadPanel (ballId);
		} else if (ballStatus == 1) {
			int ball_id_use = GetIDBallInUse ();
			UpdateBallStatus (1, ball_id_use);
			UpdateBallStatus (2, ballId);

			string lower = ballName.ToLower ();
			PlayerPrefs.SetString ("ballType", lower);
			SetBallNotActive (ballId);
			SetBallActive (ball_id_use);
			CloseShop ();
			SceneManager.LoadScene ("Shop");
		}
	}

	public void CloseShop() {
		string canvasName = ballName + "BallCanvas";
		ballCanvas = GameObject.Find (canvasName).GetComponent<Canvas>();
		Time.timeScale =1.0f;
		mainCamera.gameObject.SetActive (true);
		panelCamera.gameObject.SetActive (false);
		ballCanvas.gameObject.SetActive(false);
	}
		
	public void LoadPanel(int ball_id) {
		ReadBall (ball_id);

		string panelName;
		panelName = ballName + "Panel";
		ballPanel = GameObject.Find (panelName);
		ballPanel.GetComponentInChildren<BallPanelScript> ().SetBallPanel (ballName+" Ball",ballDesc,ballPrice,ballStatus);

		string canvasName = ballName + "BallCanvas";
		ballCanvas = GameObject.Find (canvasName).GetComponent<Canvas>();
	}

	public void BackToHome() {
		SceneManager.LoadScene ("MainMenu");
	}
}
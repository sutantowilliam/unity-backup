using System;

public class HighScore : IComparable<HighScore>
{
	public float Score {get;set;}
	public string Name {get;set;}
	public float Distance {get;set;}
	public int ID {get;set;}

	public HighScore (int id, float score, string name, float distance)
	{
		this.Score = score;
		this.Name = name;
		this.ID = id;
		this.Distance = distance;
	}

	public int CompareTo (HighScore other)
	{
		//first > second return -1
		//first < second return 1
		//first == second return 0

		if (other.Score < this.Score) {
			return -1;
		} else if (other.Score > this.Score) {
			return 1;
		} else if (other.Distance < this.Distance) {
			return -1;
		} else if (other.Distance > this.Distance) {
			return 1;
		} 
		return 0;
	}

}



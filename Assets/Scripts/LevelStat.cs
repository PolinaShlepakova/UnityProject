using System.Collections.Generic;

[System.Serializable]
public class LevelStat {
	public bool HasCrystals = false;
	public bool HasAllFruits = false;
	public bool LevelPassed = false;
	public List<int> CollectedFruits = new List<int>();
}
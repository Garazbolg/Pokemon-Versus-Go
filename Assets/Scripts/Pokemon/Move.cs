//Moves.cs
namespace Pokemon{
	public class Move{
		public readonly int accuracy;
		public readonly int power;
		public readonly bool type;//false => physic,true => special
		
		public readonly bool OHKO;
		public readonly bool priority;
		public readonly bool aoe;			//Area of effect
		public readonly bool shouldRepeat;  //Mania
		public readonly int numberOfRpeat;
		
		public readonly Statut appliedStatut;
		public readonly float propabilityStatut;
		
	}
	
	public class Move_InBattle : Move{
		public int launcherID;
		public int target; // 0 =>AOE 
		public bool applyMove(Pokemon_InBattle attacker,Pokemon_InBattle target){
			float probability = accuracy/100.0f * (attacker.Accuracy / target.Evasion);

            bool hit = UnityEngine.Random.value < probability;  //should be with server seed

            //Do all of the calculations

            return hit;
		}
	}
}
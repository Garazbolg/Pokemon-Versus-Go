using System.Collections.Generic;

namespace Pokemon {
    public enum BattleType {
        Normal,         //1v1 ; 6 Pokemon each
        Quick,          //1v1 ; 3 Pokemon each
        Duel,           //1v1 ; 1 Pokemon each
        Duo,            //2v2 ; 12 Pokemon each
        DuoQuick,       //2v2 ; 6 Pokemon each
        PairDuel,       //2v2 ; 2 Pokemon each
        Trio,           //3v3 ; 18 Pokemon each
        TrioQuick,      //3v3 ; 6 Pokemon each
        Rotating,       //3v3 ; Rotating 
        Swarm           //5v1 ; Only Wild
    }

    public class BattleEngine {
        public static BattleEngine GetInstance() {
            if (instance == null) {
                return null;
            }
            return instance;
        }

        public static BattleEngine Init(Pokemon_InBattle first, Pokemon_InBattle second) {
            instance = new BattleEngine(first, second);
            return instance;
        }

        private BattleEngine(Pokemon_InBattle first, Pokemon_InBattle second)
        {
            type = BattleType.Duel;
            team1 = new List<Pokemon_InBattle>();
            team2 = new List<Pokemon_InBattle>();

            team1.Add(first);
            team2.Add(second);

            currentState = new BattleState_SpawnAnimation();
        }

        public static void Process() {
            instance.currentState.Update();
            instance.currentState = instance.currentState.CheckForChange();
        }

        private static BattleEngine instance = null;
        private BattleType type;

        private List<Pokemon_InBattle> team1;
        private List<Pokemon_InBattle> team2;

        private BattleState currentState;

        #region BattleState
        public abstract class BattleState {
            public BattleState() {
                Start();
                Update();
            }
            public virtual void Start() { }
            public virtual void Update() { }
            public abstract BattleState CheckForChange();
        }

        public class BattleState_WaitForChoices : BattleState {
            List<string> commands;
            int numberOfExpectedCommands;

            BattleState_WaitForChoices(int numberOfCommands)
            {
                numberOfExpectedCommands = numberOfCommands;
            }

            public override BattleState CheckForChange() {
                if (commands.Count >= numberOfExpectedCommands)
                    return this; //Should change for Resolve 
                else
                    return this;
            }
        }

        public class BattleState_SpawnAnimation : BattleState
        {
            public override BattleState CheckForChange()
            {
                return this;
            }
        }

        #endregion
    }
}
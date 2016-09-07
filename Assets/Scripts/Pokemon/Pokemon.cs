/*
	Container Classes for Pokemons
*/

using System.Collections.Generic;

namespace Pokemon
{
    public struct Stats
    {
        public readonly int hp;
        public readonly int atk;
        public readonly int def;
        public readonly int atkspe;
        public readonly int defspe;
        public readonly int speed;

        public Stats(int _hp, int _atk, int _def, int _atkspe, int _defspe, int _speed) {
            hp = _hp;
            atk = _atk;
            def = _def;
            atkspe = _atkspe;
            defspe = _defspe;
            speed = _speed;
        }

        public enum Type { HP, Atk, Def, AtkSpe, DefSpe, Speed }
    }

    public enum Type {
        Normal,
        Fire,
        Fighting,
        Water,
        Flying,
        Grass,
        Poison,
        Electric,
        Ground,
        Psychic,
        Rock,
        Ice,
        Bug,
        Dragon,
        Ghost,
        Dark,
        Steel,
        Fairy,
        None
    }

    public enum Statut {
        None,
        Paralized,
        Sleeping,
        Poisened,
        Burning,
        Frozen
    }

    public enum Nature {
        Hardy,
        Lonely,
        Brave,
        Adamant,
        Naughty,
        Bold,
        Docile,
        Relaxed,
        Impish,
        Lax,
        Timid,
        Hasty,
        Serious,
        Jolly,
        Naive,
        Modest,
        Mild,
        Quiet,
        Bashful,
        Rash,
        Calm,
        Gentle,
        Sassy,
        Careful,
        Quirky
    }



    public class Species
    {
        public readonly string name;
        public readonly string id;

        public readonly Stats baseStats;

        public readonly Type type1;
        public readonly Type type2;

        public readonly bool hasGender;
    }

    public class PokedexEntry : Species
    {
        public readonly Stats EVYield;

        public readonly int catchRate;

        public readonly Experience.CourbeXP xpRate;
        public readonly int xpYield;

        public readonly float malePropability;

        public readonly Dictionary<int, int> moveset; //key = level 

        public readonly List<int> learnableMoves;

        public readonly List<int> abilities;

        public readonly string EggGroup;

        //public readonly int evolutionId;    A lot more complicated, must include stone evolution(s), level evolution, happyness, item, trade, places (leafeon),stats (tyrogue), day/night,etc ... AAaand Shedinja

        public readonly float height;
        public readonly float weight;

        public readonly string description;

        public readonly string soundsPath; // pikachaaah !
        public readonly string footprintPath;
    }

    public class Pokemon : Species
    {
        public int level;
        public string name;


        public readonly Nature nature;

        public readonly Stats IV;
        private Stats _EV;
        public Stats EV
        {
            get
            {
                return _EV;
            }
        }
        public void AddEV(Stats toAddEV) {
            if (_EV.hp + _EV.atk + _EV.def + _EV.atkspe + _EV.defspe + _EV.speed + toAddEV.hp + toAddEV.atk + toAddEV.def + toAddEV.atkspe + toAddEV.defspe + toAddEV.speed <= 510) { //510 = max overall EV
                _EV = new Stats(Maths.Min(252, _EV.hp + toAddEV.hp), Maths.Min(252, _EV.atk + toAddEV.atk), Maths.Min(252, _EV.def + toAddEV.def), Maths.Min(252, _EV.atkspe + toAddEV.atkspe), Maths.Min(252, _EV.defspe + toAddEV.defspe), Maths.Min(252, _EV.speed + toAddEV.speed));
            }
        }

        ///false = male / true = female
        public readonly bool gender;
        private bool _isAlive;
        public bool isAlive {
            get {
                return _isAlive;
            }
        }

        #region Stats
        private float GetNatureMultiplier(Nature n, Stats.Type s) {
            switch (s) {
                case Stats.Type.Atk:
                    switch (n) {
                        case Nature.Lonely:
                        case Nature.Adamant:
                        case Nature.Naughty:
                        case Nature.Brave:
                            return 1.1f;
                            break;
                        case Nature.Bold:
                        case Nature.Modest:
                        case Nature.Calm:
                        case Nature.Timid:
                            return 0.9f;
                            break;
                        default: return 1f;
                    }
                    break;
                case Stats.Type.Def:
                    switch (n) {
                        case Nature.Bold:
                        case Nature.Impish:
                        case Nature.Lax:
                        case Nature.Relaxed:
                            return 1.1f;
                            break;
                        case Nature.Lonely:
                        case Nature.Mild:
                        case Nature.Gentle:
                        case Nature.Hasty:
                            return 0.9f;
                            break;
                        default: return 1f;
                    }
                    break;
                case Stats.Type.AtkSpe:
                    switch (n) {
                        case Nature.Modest:
                        case Nature.Mild:
                        case Nature.Rash:
                        case Nature.Quiet:
                            return 1.1f;
                            break;
                        case Nature.Adamant:
                        case Nature.Impish:
                        case Nature.Careful:
                        case Nature.Jolly:
                            return 0.9f;
                            break;
                        default: return 1f;
                    }
                    break;
                case Stats.Type.DefSpe:
                    switch (n) {
                        case Nature.Calm:
                        case Nature.Gentle:
                        case Nature.Careful:
                        case Nature.Sassy:
                            return 1.1f;
                            break;
                        case Nature.Naughty:
                        case Nature.Lax:
                        case Nature.Rash:
                        case Nature.Naive:
                            return 0.9f;
                            break;
                        default: return 1f;
                    }
                    break;
                case Stats.Type.Speed:
                    switch (n) {
                        case Nature.Timid:
                        case Nature.Hasty:
                        case Nature.Jolly:
                        case Nature.Naive:
                            return 1.1f;
                            break;
                        case Nature.Brave:
                        case Nature.Relaxed:
                        case Nature.Quiet:
                        case Nature.Sassy:
                            return 0.9f;
                            break;
                        default: return 1f;
                    }
                    break;
                default: return 1f;
            }
            return 1f;
        }

        private Stats _statistics;
        public void UpdateStats() {
            _statistics = new Stats(
                                    (int)(10 + level + (int)((level * (2 * baseStats.hp + IV.hp + (int)(_EV.hp / 4))) / 100)),
                                        (int)(GetNatureMultiplier(nature, Stats.Type.Atk) * (5 + (int)(((2 * baseStats.atk + IV.atk + (int)(_EV.atk / 4)) * level) / 100))),
                                        (int)(GetNatureMultiplier(nature, Stats.Type.Def) * (5 + (int)(((2 * baseStats.def + IV.def + (int)(_EV.def / 4)) * level) / 100))),
                                        (int)(GetNatureMultiplier(nature, Stats.Type.AtkSpe) * (5 + (int)(((2 * baseStats.atkspe + IV.atkspe + (int)(_EV.atkspe / 4)) * level) / 100))),
                                        (int)(GetNatureMultiplier(nature, Stats.Type.DefSpe) * (5 + (int)(((2 * baseStats.defspe + IV.defspe + (int)(_EV.defspe / 4)) * level) / 100))),
                                        (int)(GetNatureMultiplier(nature, Stats.Type.Speed) * (5 + (int)(((2 * baseStats.speed + IV.speed + (int)(_EV.speed / 4)) * level) / 100)))
                                    );
        }
        public int HPMax {
            get {
                return _statistics.hp;
            }
        }
        public virtual int Atk {
            get {
                return _statistics.atk;
            }
        }
        public virtual int Def
        {
            get {
                return _statistics.def;
            }
        }
        public virtual int AtkSpe
        {
            get {
                return _statistics.atkspe;
            }
        }
        public virtual int DefSpe
        {
            get {
                return _statistics.defspe;
            }
        }
        public virtual int Speed
        {
            get {
                return _statistics.speed;
            }
        }


        private int _currentHPLost;
        public void ReduceHP(int value) {
            _currentHPLost += value;
            if (_currentHPLost > HPMax) {
                _isAlive = false;
                _currentHPLost = HPMax;
            }
        }
        public void AddHP(int value) {
            _currentHPLost -= value;
            if (_currentHPLost < 0) {
                _currentHPLost = 0;
            }
        }
        public int HP {
            get {
                return HPMax - _currentHPLost;
            }
        }

        #endregion

        private Statut _currentStatut;
        public Statut currentStatut {
            get {
                return _currentStatut;
            }
            set {
                if (_currentStatut != Statut.None)
                    _currentStatut = value;
            }
        }
    }

    public class Pokemon_InBattle : Pokemon
    {
        public readonly int battleID;

        #region Stats
        private Stats _currentStats;

        public void DecreaseAtk(int value) {
            if (_currentStats.atk - value > -6)
                _currentStats = new Stats(0, _currentStats.atk - 1, _currentStats.def, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed);
            else
                _currentStats = new Stats(0, -6, _currentStats.def, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed);
        }
        public void IncreaseAtk(int value) {
            if (_currentStats.atk + value < 6)
                _currentStats = new Stats(0, _currentStats.atk + 1, _currentStats.def, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed);
            else
                _currentStats = new Stats(0, 6, _currentStats.def, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed);
        }
        public override int Atk {
            get {
                return (int)(((_currentStats.atk > 0) ? (2 + _currentStats.atk) / 2.0 : 2.0 / (2 - _currentStats.atk)) * base.Atk);
            }
        }

        public void DecreaseDef(int value) {
            if (_currentStats.def - value > -6)
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def - 1, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed);
            else
                _currentStats = new Stats(0, _currentStats.atk, -6, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed);
        }
        public void IncreaseDef(int value) {
            if (_currentStats.def + value < 6)
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def + 1, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed);
            else
                _currentStats = new Stats(0, _currentStats.atk, 6, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed);
        }
        public override int Def {
            get {
                return (int)(((_currentStats.def > 0) ? (2 + _currentStats.def) / 2.0 : 2.0 / (2 - _currentStats.def)) * base.Def);
            }
        }

        public void DecreaseAtkSpe(int value) {
            if (_currentStats.atk - value > -6)
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe - 1, _currentStats.defspe, _currentStats.speed);
            else
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, -6, _currentStats.defspe, _currentStats.speed);
        }
        public void IncreaseAtkSpe(int value) {
            if (_currentStats.atkspe + value < 6)
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe + 1, _currentStats.defspe, _currentStats.speed);
            else
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, 6, _currentStats.defspe, _currentStats.speed);
        }
        public override int AtkSpe {
            get {
                return (int)(((_currentStats.atkspe > 0) ? (2 + _currentStats.atkspe) / 2.0 : 2.0 / (2 - _currentStats.atkspe)) * base.AtkSpe);
            }
        }

        public void DecreaseDefSpe(int value) {
            if (_currentStats.defspe - value > -6)
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe, _currentStats.defspe - 1, _currentStats.speed);
            else
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe, -6, _currentStats.speed);
        }
        public void IncreaseDefSpe(int value) {
            if (_currentStats.defspe + value < 6)
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe, _currentStats.defspe + 1, _currentStats.speed);
            else
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe, 6, _currentStats.speed);
        }
        public override int DefSpe {
            get {
                return (int)(((_currentStats.defspe > 0) ? (2 + _currentStats.defspe) / 2.0 : 2.0 / (2 - _currentStats.defspe)) * base.DefSpe);
            }
        }

        public void DecreaseSpeed(int value) {
            if (_currentStats.speed - value > -6)
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed - 1);
            else
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe, _currentStats.defspe, -6);
        }
        public void IncreaseSpeed(int value) {
            if (_currentStats.speed + value < 6)
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe, _currentStats.defspe, _currentStats.speed + value);
            else
                _currentStats = new Stats(0, _currentStats.atk, _currentStats.def, _currentStats.atkspe, _currentStats.defspe, 6);
        }
        public override int Speed {
            get {
                return (int)(((_currentStats.speed > 0) ? (2 + _currentStats.speed) / 2.0 : 2.0 / (2 - _currentStats.speed)) * base.Speed);
            }
        }

        private int _currentAccuracy;
        public void DecreaseAccuracy(int value) {
            if (_currentAccuracy - value > -6)
                _currentAccuracy -= value;
            else
                _currentAccuracy = -6;
        }
        public void IncreaseAccuracy(int value) {
            if (_currentAccuracy + value < 6)
                _currentAccuracy += value;
            else
                _currentAccuracy = 6;
        }
        public float Accuracy {
            get {
                return (_currentAccuracy > 0) ? (3 + _currentAccuracy) / 3.0f : 3.0f / (3 - _currentAccuracy);
            }
        }

        private int _currentEvasion;
        public void DecreaseEvasion(int value) {
            if (_currentEvasion - value > -6)
                _currentEvasion -= value;
            else
                _currentEvasion = -6;
        }
        public void IncreaseEvasion(int value) {
            if (_currentEvasion + value < 6)
                _currentEvasion += value;
            else
                _currentEvasion = 6;
        }
        public float Evasion {
            get {
                return (_currentEvasion < 0) ? (3 - _currentEvasion) / 3.0f : 3.0f / (3 + _currentEvasion);
            }
        }

        #endregion

        #region Special Statuts

        public bool isConfused;//Ultrason
        public bool isCursed;//Malediction
        public bool cantEscape;//GrosYeux
        public bool hasToAttack;//Provoc
        public bool isInLove;//Attraction
        public int loverIndex;
        public bool hasToRepeatMove;//Mania
        public bool cantRepeatMove; //entrave
        public bool isTired; //ultralazer,monaflemit
        public bool isconstricted; //constriction
        public bool vampigraine;
        public int cloneHP;

        #endregion
    }
}

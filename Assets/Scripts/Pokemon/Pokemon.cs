/*
	Container Classes for Pokemons
*/

public struct Stats
{
	public readonly int hp;
	public readonly int atk;
	public readonly int def;
	public readonly int atkspe;
	public readonly int defspe;
	public readonly int speed;

	public Stats(int _hp,int _atk,int _def, int _atkspe, int _defspe,int _speed){
		hp = _hp;
		atk = _atk;
		def = _def;
		atkspe = _atkspe;
		defspe = _defspe;
		speed = _speed;
	}
}

public class Pokemon_Species
{
	public readonly string name;
	public readonly string id;

	public readonly Stats baseStats;

    public readonly int captureRate;

	public readonly int evolutionId;
}
using System;

public class Arma
{
	public string Name { get; set; }
	public int damage { get; set; }
	public int durability { get; set; }

	public Arma(string nm, int dmg, int drblty)
	{
		Name = nm;
		damage = dmg;
		durability = drblty;
	}
}



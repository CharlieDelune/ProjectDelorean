
public class ConsumableItem : Item {
	public int hpRestore;
	public int mpRestore;
	public bool applyHaste;
	public int increaseAttack;
	public int increaseDefense;
	public int increaseAgility;
	public float damageMod;
	public bool useableOutOfCombat;

	public ConsumableItem(string name, string desc, int buy, int sell, ItemType itm, TargetType type, int hprst, int mprst, bool hst, int atk, int def, int agi, float dmg, bool ooc) : base(name, desc, buy, sell, itm,type){
		this.hpRestore = hprst;
		this.mpRestore = mprst;
		this.applyHaste = hst;
		this.increaseAttack = atk;
		this.increaseDefense = def;
		this.increaseAgility = agi;
		this.useableOutOfCombat = ooc;
	}
}
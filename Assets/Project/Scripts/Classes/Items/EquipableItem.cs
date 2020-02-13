public class EquipableItem : Item {

	public string equipLoc;
	public int increaseAttack;
	public int increaseMagAttack;
	public int increaseDefense;
	public int increaseMagDefense;
	public int increaseAgility;
	public string[] equippableBy;

	public EquipableItem(string name, string desc, int buy, int sell, ItemType itm,TargetType type, string eqloc, int atk, int magAtk, int def, int magDef, int agi, string[] eq) : base(name, desc, buy, sell, itm,type){
		this.equipLoc = eqloc;
		this.increaseAttack = atk;
		this.increaseMagAttack = magAtk;
		this.increaseDefense = def;
		this.increaseMagDefense = magDef;
		this.increaseAgility = agi;
		this.equippableBy = eq;
	}
}

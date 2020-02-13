public class Item {
	public string itemName;
	public string description;
	public int buyValue;
	public int sellValue;
	public ItemType itemType;
	public TargetType targetType;

	public Item(string name, string desc, int buy, int sell, ItemType itmtyp, TargetType type){
		this.itemName = name;
		this.description = desc;
		this.buyValue = buy;
		this.sellValue = sell;
		this.itemType = itmtyp;
		this.targetType = type;
	}
}
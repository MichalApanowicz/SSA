using SQLiteNetExtensions.Attributes;

namespace SSA.Droid.Models
{
    public class ItemInLists
    {
        [ForeignKey(typeof(ItemModel))]
        public int ItemId { get; set; }

        [ForeignKey(typeof(ListModel))]
        public int ListId { get; set; }
    }
}
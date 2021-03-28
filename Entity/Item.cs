using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dev.groceries.lltm.local.Entity
{
    [Table("Item")]
    public class Item
    {
        public int Id { get; set; }

        public Item(string name, int count)
        {
            this.Name = name;
            this.Count = count;
            this.Purchased = null;
        }

        public string Name { get; set; }

        public int Count { get; set; }

        public DateTime? Purchased { get; set; }

    }
}

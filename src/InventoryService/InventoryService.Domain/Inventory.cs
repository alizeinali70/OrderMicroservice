namespace InventoryService.Domain
{
    public class Inventory
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        protected Inventory() { }

        public Inventory(Guid productId, int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
        }

        public void AdjustQuantity(int amount)
        {
            if (Quantity + amount < 0)
                throw new InvalidOperationException("Insufficient stock.");
            Quantity += amount;
        }
    }
}
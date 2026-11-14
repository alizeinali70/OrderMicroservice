namespace OrderService.Domain
{
    public class Order
    {
        public Guid Id { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Order() { }

        public Order(decimal totalAmount)
        {
            if (totalAmount <= 0)
                throw new ArgumentException("Total amount must be greater than zero.", nameof(totalAmount));

            Id = Guid.NewGuid();
            TotalAmount = totalAmount;
            Status = "Pending";
            CreatedAt = DateTime.UtcNow;
        }
    }
}
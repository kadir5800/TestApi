namespace Business.DTO.Basket
{
    public class getOneBasketResponse
    {
        public string Id { get; set; }
        public string ShoesId { get; set; }
        public string CustomerId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Gender { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

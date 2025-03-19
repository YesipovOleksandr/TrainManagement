namespace TrainManagement.DAL.Entities
{
    public class TrainComponent : Entity<long>
    {
        public string Name { get; set; }
        public string UniqueNumber { get; set; }
        public bool CanAssignQuantity { get; set; }
        public int? Quantity { get; set; }
    }
}

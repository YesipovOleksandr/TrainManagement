namespace TrainManagement.Common.Models
{
    public class TrainComponentList
    {
        public List<TrainComponent> Items { get; set; }
        public int AllCount { get; }

        public TrainComponentList(List<TrainComponent> items, int count)
        {
            AllCount = count;
            Items = items;
        }
    }
}

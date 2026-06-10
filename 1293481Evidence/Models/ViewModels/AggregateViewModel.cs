namespace _1293481Evidence.Models.ViewModels
{
    namespace _1293481Evidence.Models.ViewModels
    {
        public class GroupByAggregateViewModel
        {

            public int TransactionTypeId { get; set; }
            public string TransactionTypeName { get; set; } = null!;
            public int Count { get; set; }
            public decimal MinValue { get; set; }
            public decimal MaxValue { get; set; }
            public decimal SumValue { get; set; }
            public decimal AvgValue { get; set; }

        }

        public class AggregateViewModel
        {
            public decimal MinValue { get; set; }
            public decimal MaxValue { get; set; }
            public decimal SumValue { get; set; }
            public decimal AvgValue { get; set; }
            public int Count { get; set; }
            public List<GroupByAggregateViewModel> GroupByAggregateViewModelResult { get; set; }
        }
    }

}

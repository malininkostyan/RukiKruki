namespace DAL.ViewModel
{
    public class StatisticViewModel
    {
        public (string, int) MostPopularTO { get; set; }

        public (string, int) MostPopularTOClient { get; set; }

        public decimal AverageCheck { get; set; }

        public decimal AverageCheckClient { get; set; }

        public int CountTOClient { get; set; }
    }
}

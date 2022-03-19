namespace MatthewMcAllister.Models
{
    public class FormulaFeedingRecommendation
    {
        public double MinimumMonths { get; set; }
        public double MaximumMonths { get; set; }

        public int MinimumFeedings { get; set; }
        public int MaximumFeedings { get; set; }

        public int MinimumOuncesPerFeeding { get; set; }
        public int MaximumOuncesPerFeeding { get; set; }
    }
}

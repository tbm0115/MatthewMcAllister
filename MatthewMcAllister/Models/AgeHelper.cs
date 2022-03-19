namespace MatthewMcAllister.Models
{
    public static class AgeHelper
    {
        public static DateTime Birthdate = new DateTime(2022, 03, 10, 7, 22, 0);

        public static int GetAge(AgeType type = AgeType.Year)
        {
            DateTime now = DateTime.Now;
            switch (type)
            {
                case AgeType.Year:
                    return now.Year - Birthdate.Year;
                    break;
                case AgeType.Month:
                    return (now.Month + now.Year * 12) - (Birthdate.Month + Birthdate.Year * 12);
                    break;
                case AgeType.Week:
                    throw new NotImplementedException();
                    break;
                case AgeType.Day:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }
    }
    public enum AgeType
    {
        Year,
        Month,
        Week,
        Day
    }
}

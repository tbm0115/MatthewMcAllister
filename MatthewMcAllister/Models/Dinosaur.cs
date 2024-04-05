namespace MatthewMcAllister.Models
{
    public class Dinosaur
    {
        public int Id { get; set; }

        /// <summary>
        /// Name of the dinosaur
        /// </summary>
        public string Name { get; set; }

        public string Pronunciation { get; set; }

        public string MeaningOfName { get; set; }

        public string Diet { get; set; }

        public string Length { get; set; }

        public string Period { get; set; }

        public string Mya { get; set; }

        public string Info { get; set; }

        /// <summary>
        /// Image link
        /// </summary>
        public string ImageUrl { get; set; }
    }
}

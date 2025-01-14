namespace BaltaDataAccess.Models
{
    class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public Category()
        {
            Id = Guid.NewGuid();
        }
    }
}
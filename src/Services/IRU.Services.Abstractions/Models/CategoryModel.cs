namespace IRU.Services.Models
{
    public class CategoryModel
    {
        public Categories Value { get; set; }

        public int Id => (int)this.Value;
    }
}

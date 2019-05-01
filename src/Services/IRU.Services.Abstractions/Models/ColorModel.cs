namespace IRU.Services.Models
{
    public class ColorModel
    {
        public Colors Value { get; set; }

        public int Id => (int)this.Value;
    }
}

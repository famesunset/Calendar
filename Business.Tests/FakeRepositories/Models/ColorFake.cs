namespace Business.Tests.FakeRepositories.Models
{
    public class ColorFake
    {
        public int Id { get; set; }
        public string Hex { get; set; }

        public static ColorFake Default = new ColorFake { Id = 10, Hex = "#fff" };
    }
}

namespace Business.Tests.FakeRepositories.Models
{
    public class FakeBrowser
    {
        public int Id { get; set; }
        public string BrowserId { get; set; }
        public FakeUser User { get; set; }
    }
}
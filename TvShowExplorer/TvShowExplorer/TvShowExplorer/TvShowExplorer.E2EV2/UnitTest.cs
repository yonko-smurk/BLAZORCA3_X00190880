namespace TvShowExplorer.E2EV2
{
    [TestClass]
    public class SearchTests : PageTest
    {
        private const string BaseUrl = "https://gray-plant-018db4303.3.azurestaticapps.net/";

        [TestMethod]
        public async Task Search_For_Show_Displays_Results()
        {
            await Page.GotoAsync(BaseUrl);
            await Page.FillAsync("#query", "one piece");
            await Page.ClickAsync("button:has-text(\"Search\")");

            var cards = Page.Locator("[data-test=search-results] .card");
            await cards.First.WaitForAsync();

            var count = await cards.CountAsync();
            Assert.IsTrue(count > 0, "Expected at least one search result card, but found none.");
        }

        [TestMethod]
        public async Task Search_Shows_Requires_Query()
        {
            await Page.GotoAsync(BaseUrl);
            await Page.ClickAsync("button:has-text(\"Search\")");

            var validation = Page.Locator(".validation-message");
            await Expect(validation).ToContainTextAsync("Please enter a show name.");
        }

        [TestMethod]
        public async Task Search_No_Results_Shows_Info_Message()
        {
            await Page.GotoAsync(BaseUrl);
            await Page.FillAsync("#query", "zzzz-not-a-real-show-12345");
            await Page.ClickAsync("button:has-text(\"Search\")");

            var message = Page.Locator(".alert.alert-warning");
            await Expect(message).ToContainTextAsync("No shows found for that name.");
        }

        [TestMethod]
        public async Task Browse_Load_Shows_Displays_Cards()
        {
            await Page.GotoAsync(BaseUrl);
            await Page.ClickAsync("section:has-text(\"Browse Shows\") button:has-text(\"Load Shows\")");

            var browseCards = Page.Locator("section:has-text(\"Browse Shows\") .card");
            await browseCards.First.WaitForAsync();

            var count = await browseCards.CountAsync();
            Assert.IsTrue(count > 0, "Expected browse cards after loading shows.");
        }
    }
}

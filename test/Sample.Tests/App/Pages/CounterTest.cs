namespace Sample.App.Pages;

public class CounterCSharpTest : TestContext
{
    [Fact]
	public void Counter_should_increment_when_clicked()
    {
        // Arrange: render the Counter.razor component
        IRenderedFragment cut = RenderComponent<Counter>();

        // Act: find and click the <button> element to increment
        // the counter in the <p> element
        IElement button = cut.Find("button");
        button.Click();

        // Assert: first find the <p> element, then verify its content
        IElement paragraph = cut.Find("p");
        paragraph.MarkupMatches(@"<p role=""status"">Current count: 1</p>");
    }
}

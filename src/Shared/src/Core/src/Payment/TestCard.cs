namespace SunRaysMarket.Shared.Core.Payment;

public class TestCard
{
#nullable disable
    public string Id { get; internal init; }
    public string Title { get; internal init; }
    public CardDefinition Definition { get; internal init; }

    public void Deconstruct(out string id, out string title, out CardDefinition definition)
    {
        id = Id;
        title = Title;
        definition = Definition;
    }
}
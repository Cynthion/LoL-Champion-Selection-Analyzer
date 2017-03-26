using System.Diagnostics.CodeAnalysis;

namespace WebApi.Misc
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Region
    {
        EUW,
        NA
    }

    public enum Season
    {
        Season1,
        Season2,
        Season3,
        Season4,
        Season5,
        Season6,
        Season7
    }

    public enum Lane
    {
        Top,
        Jgl,
        Mid,
        Bot,
        Sup
    }
}

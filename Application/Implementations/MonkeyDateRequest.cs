using CSharpFunctionalExtensions;

namespace Application.Implementations
{
    public class MonkeyDateRequest
    {
        //date range
        public Maybe<DateTime> DateFrom { get; set; }

        public Maybe<DateTime> DateTo { get; set; }

    }
}
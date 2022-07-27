// Modification History
// ---------------------------------------------------------------------------------
// 27.07.2022  mihula  [JM220711-06E8226] CR42 - Sports EPG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Provys.Custom.OTE
{
    public class SportDict {

        private Func<IEnumerable<SportDictData>> populate;
        private List<SportDictData>? data = null;

        public SportDict(Func<IEnumerable<SportDictData>> populateFunc)
        {
            populate = populateFunc ?? throw new System.ArgumentNullException(nameof(populateFunc));            
        }

        public void Clear()
        {
            if (this.data != null) this.data.Clear();
            this.data = null;
        }

        public void PopulateIfEmpty()
        {
            if ((this.data?.Count ?? 0) > 0) return;

            this.data = new List<SportDictData>();
            this.data.AddRange(populate());
        }

        public IEnumerable<string> Translate(string input, SportDictType type, bool fromEN)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));

            PopulateIfEmpty();

            var typedData = from d in this.data where d.Type == type select d.ToTuple(fromEN); // Teams / Venues...

            // exactmatch returns translation
            var exactMatch = from d in typedData where string.Compare(input, d.From, StringComparison.InvariantCultureIgnoreCase) == 0 select d.To;
            if (exactMatch.Any())
            {
                return exactMatch;
            }

            // lets lower the bar (startwith)
            var startWithMatch = from d in typedData where d.From.StartsWith(input, StringComparison.InvariantCultureIgnoreCase) select d.To;
            if (startWithMatch.Any())
            {
                return startWithMatch;
            }

            // lets lower the bar even more (substrings)
            var substringMatch = from d in typedData where d.From.Contains(input, StringComparison.InvariantCultureIgnoreCase) select d.To;
            if (substringMatch.Any())
            {
                return substringMatch;
            }

            // nothing
            return new List<string>();
        }

        public IEnumerable<string> Whisper(string input, SportDictType type, bool fromEN)
        {
            PopulateIfEmpty();

            var typedData = from d in this.data where d.Type == type select d.ToTuple(fromEN);
            var result = from d in typedData where d.From.Contains(input, StringComparison.InvariantCultureIgnoreCase) select d.From;
            return result;
        }
    }

    public enum SportDictType { Unknown, Team, Venue, Event, Group, Match }

    public enum SportDictSource { Unknown, Manual, Import }

    [DebuggerDisplay("{Type}: {TitleEN} <=> {TitleGR} | {Source} | {ID}")]
    public class SportDictData
    {
        public object ID { get; set; }
        public SportDictType Type { get; set; }
        public SportDictSource Source { get; set; }
        public string TitleEN { get; set; }
        public string TitleGR { get; set; }
        public TupleFromTo ToTuple(bool fromEN)
        {
            return new TupleFromTo(fromEN ? TitleEN : TitleGR, fromEN ? TitleGR : TitleEN);
        }
    }

    public class TupleFromTo
    {
        public TupleFromTo(string from, string to)
        {
            From = from;
            To = to;
        }

        public string From { get; }
        public string To { get; }
    }
}

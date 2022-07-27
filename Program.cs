using Provys.Custom.OTE;

namespace SportInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dict = new SportDict(CreateSampleData);

            Console.WriteLine("Whisper");
            Debug("Team: ABC", dict.Whisper("ABC", SportDictType.Team, true));
            Debug("Team: BAN", dict.Whisper("BAN", SportDictType.Team, true));
            Debug("Team: SP", dict.Whisper("SP", SportDictType.Team, true));
            Debug("Team: A", dict.Whisper("A", SportDictType.Team, true));
            Debug("Venue: A", dict.Whisper("A", SportDictType.Venue, true));
            Debug("Venue: O", dict.Whisper("O", SportDictType.Venue, true));
            Debug("Team: Σ", dict.Whisper("Σ", SportDictType.Team, false));

            Console.WriteLine();
            Console.WriteLine("Translate");
            Debug("Team: ABC (to GR)", dict.Translate("ABC", SportDictType.Team, true));
            Debug("Team: BANIK (to GR)", dict.Translate("BANIK", SportDictType.Team, true));
            Debug("Team: BANICEK (to GR)", dict.Translate("BANICEK", SportDictType.Team, true));
            Debug("Team: BAN (to GR)", dict.Translate("BAN", SportDictType.Team, true));
            Debug("Team: SPARTA (to GR)", dict.Translate("SPARTA", SportDictType.Team, true));
            Debug("Team: MOSKVA (to GR)", dict.Translate("MOSKVA", SportDictType.Team, true));
            Debug("Team: Σ (to EN)", dict.Translate("Σ", SportDictType.Team, false));
            Debug("Team: Σπάρτη (to EN)", dict.Translate("Σπάρτη", SportDictType.Team, false));

            Console.WriteLine("Bye");
        }

        private static void Debug(string message, IEnumerable<string> result)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Result ({result.Count()}): {string.Join(", ", result.ToArray())}");
        }

        private static IEnumerable<SportDictData> CreateSampleData()
        {
            var result = new List<SportDictData>();

            result.Add(new SportDictData() { ID = 0, Source = SportDictSource.Import, Type = SportDictType.Team, TitleEN = "BANIK", TitleGR = "Ανθρακωρύχος" });
            result.Add(new SportDictData() { ID = 1, Source = SportDictSource.Import, Type = SportDictType.Team, TitleEN = "SPARTA PRAHA", TitleGR = "Σπάρτη Πράγα" });
            result.Add(new SportDictData() { ID = 2, Source = SportDictSource.Import, Type = SportDictType.Team, TitleEN = "SPARTAK MOSKVA", TitleGR = "Σπάρτακος Μόσχα" });
            
            result.Add(new SportDictData() { ID = 100, Source = SportDictSource.Import, Type = SportDictType.Venue, TitleEN = "Athenes", TitleGR = "Αθήνα" });
            result.Add(new SportDictData() { ID = 101, Source = SportDictSource.Import, Type = SportDictType.Venue, TitleEN = "London", TitleGR = "Λονδίνο" });
            result.Add(new SportDictData() { ID = 102, Source = SportDictSource.Import, Type = SportDictType.Venue, TitleEN = "Ostrava", TitleGR = "Οστράβα" });
           

            return result;
        }
    }
}
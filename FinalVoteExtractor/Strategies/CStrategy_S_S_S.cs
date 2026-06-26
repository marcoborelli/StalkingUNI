using System.Text.RegularExpressions;

namespace FinalVoteExtractor.Strategies {
    public class CStrategy_S_S_S : IVotoStrategy {
        /*
         * Questa implementazione gestisce voto:
         * LODE: IsString(voto) ["Esame superato con <voto>"...] {S}tring
         * PROMOSSO: IsString(voto) ["Esame superato con <voto>"...] {S}tring
         * BOCCIATO: voto < 18 OR IsString(voto) ["Bocciato", "Ritirato"...] {S}tring
        */
        public int GetVoto(string csv_val) {
            int voto = 0;

            Regex rg = new Regex(@".+\s(?<votoInTrentesimi>\d+).+\s(?<suTrenta>\d)");
            Match match = rg.Match(csv_val);

            if (rg.IsMatch(csv_val)) {
                voto = int.Parse(match.Groups["votoInTrentesimi"].Value);
            }

            return voto;
        }
    }
}

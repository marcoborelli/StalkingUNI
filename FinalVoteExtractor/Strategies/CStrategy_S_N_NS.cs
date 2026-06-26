using System;
using System.Text.RegularExpressions;

namespace FinalVoteExtractor.Strategies {
    public class CStrategy_S_N_NS : IVotoStrategy {
        /*
         * Questa implementazione gestisce voto:
         * LODE: IsString(voto) ["30 L", "30L", "30 LODE"...] {S}tring
         * PROMOSSO: 18 <= voto <= 30 {N}umber
         * BOCCIATO: voto < 18 OR IsString(voto) ["Bocciato", "Ritirato"...] {N}umber{S}tring
        */
        public int GetVoto(string csv_val) {
            // Nota + (almeno un carattere -> "30" False), non * (anche nessun carattere -> "30" True)
            Regex rg = new Regex(@"30.+");

            // IsString(csv_val) -> voto resta intoccato (0)
            Int32.TryParse(csv_val, out int voto);

            if (rg.IsMatch(csv_val)) {
                voto = 31;
            } else if (voto < 18) {
                voto = 0;
            }

            return voto;
        }
    }
}

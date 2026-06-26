using System;

namespace FinalVoteExtractor.Strategies {
    public class CStrategy_N_N_NS: IVotoStrategy {
        /*
         * Questa implementazione gestisce voto:
         * LODE      -> voto > 30 {N}umber
         * PROMOSSO  -> 18 <= voto <= 30 {N}umber
         * BOCCIATO  -> voto < 18 OR IsString(voto) ["Bocciato", "Ritirato"...] {N}umber{S}tring
        */
        public int GetVoto(string csv_val) {
            // IsString(csv_val) -> voto resta intoccato (0)
            Int32.TryParse(csv_val, out int voto);

            if (voto > 31) {
                voto = 31;
            } else if (voto < 18) {
                voto = 0;
            }

            return voto;
        }
    }
}

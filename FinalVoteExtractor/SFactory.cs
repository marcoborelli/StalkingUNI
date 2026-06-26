using FinalVoteExtractor.Strategies;

namespace FinalVoteExtractor {
    public class SFactory {
        private static SFactory _instance;

        public static SFactory GetInstance() {
            if (_instance == null) {
                _instance = new SFactory();
            }
            return _instance;
        }

        private SFactory() {}

        public IVotoStrategy GetVotoStrategy(string subject) {
            IVotoStrategy res = null;

            switch (subject) {
                case "Architettura":
                case "Fondamenti":
                case "Programmazione1":
                case "Programmazione2":
                case "MetodiAlgebrici":
                case "SistemiReti":
                    res = new CStrategy_N_N_NS();
                    break;
                case "Algoritmi":
                case "AlgebraLineare":
                    res = new CStrategy_S_N_NS();
                    break;
                case "Analisi1":
                    res = new CStrategy_S_S_S();
                    break;
            }

            return res;
        }
    }
}
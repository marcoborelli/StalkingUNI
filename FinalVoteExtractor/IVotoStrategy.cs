namespace FinalVoteExtractor {
    public interface IVotoStrategy {
        // Output: 31 = Lode, 0 = Bocciato, [18, 30] = VotoNormale
        int GetVoto(string csv_val);
    }
}

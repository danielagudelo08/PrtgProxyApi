using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace PrtgProxyApi.Helpers
{
    public static class TextNormalizer
    {
        private static readonly Regex MultiSpaceRegex = new(@"\s+", RegexOptions.Compiled);
        private static readonly Regex SpecialCharsRegex = new(@"[^\w\s]", RegexOptions.Compiled);

        /// <summary>
        /// Normaliza un texto eliminando espacios extras, signos de puntuación, tildes y usando minúsculas.
        /// </summary>
        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Eliminar espacios extra
            string trimmed = input.Trim();
            string singleSpaced = MultiSpaceRegex.Replace(trimmed, " ");

            // Eliminar signos de puntuación
            string noPunctuation = SpecialCharsRegex.Replace(singleSpaced, "");

            // Eliminar tildes y acentos
            string noDiacritics = RemoveDiacritics(noPunctuation);

            // Pasar todo a minúsculas
            return noDiacritics.ToLowerInvariant();
        }

        private static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            return new string(normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray())
                .Normalize(NormalizationForm.FormC); // Recompón si es necesario
        }
    }

}

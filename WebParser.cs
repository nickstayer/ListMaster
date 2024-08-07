using System.Text.RegularExpressions;


namespace ListMaster
{
    public class WebParser
    {
        public static (string, string, string) DocTypeSeriesNumberEsfl(string docTypeSeriesNumber)
        {
            if (string.IsNullOrEmpty(docTypeSeriesNumber))
            {
                return (string.Empty, string.Empty, string.Empty);
            }
            var docType = Regex.Match(docTypeSeriesNumber, Consts.PATTERN_ESFL_DOC_TYPE).Value;
            var docSeries = Regex.Match(docTypeSeriesNumber, Consts.PATTERN_ESFL_DOC_SERIES).Value;
            var docNumber = Regex.Match(docTypeSeriesNumber, Consts.PATTERN_ESFL_DOC_NUMBER).Value;
            return (docType?.Trim(), docSeries?.Trim(), docNumber?.Trim());
        }

        public static (string, string) DepCodeAndNameEsfl(string depCodeAndName)
        {
            if (string.IsNullOrEmpty(depCodeAndName))
            {
                return (string.Empty, string.Empty);
            }
            var depCode = Regex.Match(depCodeAndName, Consts.PATTERN_ESFL_DEP_CODE).Value;
            var depName = Regex.Match(depCodeAndName, Consts.PATTERN_ESFL_DEP_NAME).Value;
            return (depCode?.Trim(), depName?.Trim());
        }
    }
}

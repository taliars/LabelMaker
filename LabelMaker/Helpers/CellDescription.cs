using iText.Layout.Properties;


namespace LabelMaker.Helpers
{
    internal class CellDescription
    {
        public string Content { get; set; }

        public TextAlignment TextAlignment { get; set; }

        public CellPosition CellPosition { get; set; }

        public bool IsBold { get; set; } = false;

        public int ColumnSpan { get; set; } = 1;
    }
}

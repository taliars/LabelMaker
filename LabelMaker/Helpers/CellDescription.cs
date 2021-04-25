namespace LabelMaker.Helpers
{
    internal class CellDescription
    {
        public string Content { get; set; }

        public CellPosition CellPosition { get; set; }

        public bool IsBold { get; set; } = false;
    }
}

namespace LabelMaker.Core
{
    internal class CellDescription
    {
        public CellDescription(string content, CellPosition position, bool isBold = false)
        {
            this.Content = content;
            this.CellPosition = position;
            this.IsBold = isBold;
        }

        public string Content { get; set; }

        public CellPosition CellPosition { get; set; }

        public bool IsBold { get; set; } = false;
    }
}

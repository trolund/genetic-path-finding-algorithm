using Blazor.Extensions.Canvas.Canvas2D;

namespace blazor_canvas_ga_path_finding.Models
{
    public class Wall
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public string Color { get; private set; }

        public Wall(double x, double y, double width, double height, string color)
        {
            (X, Y, Width, Height, Color) = (x, y, width, height, color);
        }

        public async void Display(Canvas2DContext ctx)
        {
            await ctx.BeginPathAsync();
            await ctx.SetFillStyleAsync(Color);
            await ctx.FillRectAsync(X, Y, Width, Height);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvasTest2.Models
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

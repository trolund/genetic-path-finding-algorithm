using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvasTest2.Models
{
    public class LifeBar {

        private int Max;
        private int Progress;
        private Vector2 Pos;

        public LifeBar(Vector2 pos, int progress, int max) {
            Max = max;
            Progress = progress;
            Pos = pos;
        }
        
        public void StepForward(int progress)
        {
            Progress = progress;
        }
        public async Task Display(Canvas2DContext ctx)
        {
            float r = (float)Progress/(float)Max;
            int p = (int)(r * 100) * 2;

            await ctx.BeginPathAsync();
            await ctx.SetFillStyleAsync("#212121");
            await ctx.FillRectAsync(Pos.X, Pos.Y, 210, 30);
            await ctx.FillAsync();

            await ctx.BeginPathAsync();
            await ctx.SetFillStyleAsync("#303030");
            await ctx.FillRectAsync(Pos.X + 5, Pos.Y + 5, 200, 20);
            await ctx.FillAsync();

            await ctx.BeginPathAsync();
            await ctx.SetFillStyleAsync("#e37730");
            await ctx.FillRectAsync(Pos.X + 5, Pos.Y + 5, p, 20);
            await ctx.FillAsync();
        }
    }
}


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
            var p = Max/100 * Progress * 2;

            await ctx.BeginPathAsync();
            await ctx.SetFillStyleAsync("#8b95a3");
            await ctx.FillRectAsync(Pos.X, Pos.Y, 280, 30);
            await ctx.FillAsync();

            await ctx.BeginPathAsync();
            await ctx.SetFillStyleAsync("#1f824c");
            await ctx.FillRectAsync(Pos.X + 5, Pos.Y + 5, p, 20);
            await ctx.FillAsync();
        }
    }
}


using Snake_Game_OOP.Contracts;

namespace Snake_Game_OOP
{
    public class Highscore
    {
        private IRenderer renderer;
        public Highscore(IRenderer renderer)
        {
            this.renderer = renderer;
        }

       public void Render()
        {

            renderer.Render(this);
        }


    }
}
using System;

namespace GJExperience.PingPongGame.V1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PingPongPlay game = new PingPongPlay())
            {
                game.Run();
            }
        }
    }
#endif
}


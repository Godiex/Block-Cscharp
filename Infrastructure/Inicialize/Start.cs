using Infrastructure.Context;

namespace Infrastructure.Inicialize
{
    public class Start
    {
        private readonly PersistenceContext _context;
        public Start(PersistenceContext context)
        {
            _context = context;
        }

        public void Inicializar()
        {
            Console.WriteLine("inicializando");
        }
    }
}
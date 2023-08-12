using Base.Infrastructure.Context;

namespace Base.Infrastructure.Inicialize
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
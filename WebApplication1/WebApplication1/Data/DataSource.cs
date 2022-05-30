/* sing WebApplication1.Data.DTOs;
namespace WebApplication1.Data
{
    //Singleton - в однопоточном приложении будет единственный экземпляр некоторого класса,
    //и предоставляющий глобальную точку доступа к этому экземпляру.

    //мы создаем объект хранения списка авторов. Антипаттерн Singleton позволяет каждый раз возвращать один и тот же экземпляр класса DataSource,
    //то есть мы имеем один экземпляр источника данных.
    public class DataSource
    {
        private static DataSource instance;
        private DataSource()
        {
        }
        public static DataSource GetInstance()
        {
            if (instance == null)
            {
                instance = new DataSource();
            }
            return instance;
        }
        //создаём списки с нашими данными
        public List<Tour> _tours = new List<Tour>();

        public List<Schedule> _schedules = new List<Schedule>();

        public List<Order> _orders = new List<Order>();

    }
}
*/
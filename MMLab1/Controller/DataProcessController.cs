using System;
using System.Collections.Generic;
using MMLab1.Model;
using MMLab1.View.Plot;

namespace MMLab1.Controller
{
    /// <summary>
    /// Класс DataProcessController
    /// 
    /// Класс отвечающий за контроль расчетов процесса математической модели.
    /// Имеет единую точку входа, для доступа извне и возвращает результаты моделирования
    /// в едином формате
    /// </summary>
    public class DataProcessController
    {
        /// <summary>
        /// Функция ProcessData
        /// 
        /// Функция расчитывает модель процесса и возвращает результаты в одном объекте
        /// В качестве входных параметров подается входной объект с данными состояния математической модели.
        /// </summary>
        private const int MinutesInOneHour = 60;                                                        // Количество минут в часе
/*
        private const int SecondsInMinute = 60;                                                         // Количество секунд в минуте
*/
        /*
        * Функция ProcessData(InputData inputData) - функция обработки входных данных по математической модели
        * Функция получает входные значения от пользователя и обрабатывает их, возвращая результат в виде списка объектов
        */
        public List<ConcentrationCell> ProcessData(InputData inputData)
        {
            if (inputData==null)                                                                        // Если полученный входной объект NULL
                return null;                                                                            // Выходим, вернув NULL
            var resultCollectionOfCellConcentrations = new List<ConcentrationCell>();                   // Создаем результирующую коллекцию
            for (var i = 0; i < inputData.N; i++)                                                       // В цикле по всем ячейкам
            {       
                var cellValues = new List<ConcentrationInfo>();                                         // Создаем коллекцию значений результатов для ячейки
                var cell = new ConcentrationCell                                                        // Создаем объект описания результатов для каждой ячейки
                {
                    ConcentrationInfos = cellValues,                                                    // Коллекцию записываем в объект
                    ConcentrationName = "Ячейка" + (i + 1)                                              // Инициализируем название объекта
                };
                resultCollectionOfCellConcentrations.Add(cell);                                         // Добавляем объект в результирующую коллекцию
            }
            var tau = (inputData.V / inputData.G) * MinutesInOneHour;                                   // Тау - отношение объема аппарата к объемному расходу
                                                                                                        // объемный расход м^3/час, поэтому домножаем на minutesInOneHour
            var stepTime = inputData.k*tau;                                                             // Шаг времени, после которого происходит возобновление компонента
            var numberOfSteps = (int)stepTime/inputData.h;                                              // Количество шагов = общее время делить на шаг
            var currentTime = 0.0;                                                                      // Текущее время  - 0
            for (var i = 0; i < numberOfSteps; i++)                                                     // В цикле по всем шагам
            {
                for (var j = 0; j < inputData.N; j++)                                                   // В цикле по всем ячейкам
                {
                    var concentration = StepSumm(j + 1, tau, currentTime) *                             // Концентрация по формуле 1 из отчета
                        Math.Exp((-currentTime) / tau) * inputData.C_an;
                    resultCollectionOfCellConcentrations[j].ConcentrationInfos.Add(new ConcentrationInfo// Добавляем в соответствующую коллекцию
                    {
                        Time = currentTime,                                                             // Время
                        Concentration = Math.Round(concentration,1)                                     // Концентрация - 1 знак после запятой
                    });
                }
                currentTime += inputData.h;                                                             // Делаем шаг по времени
            }

            currentTime = 0.0;                                                                          // Снова зануляем время, для использования формул
            var concentrationInput = inputData.C_an;                                                    // Входная концентрация - начальная концентрация
            for (var i = 0; i < numberOfSteps; i++)                                                     // В цикле по всем шагам
            {
                for (var j = 0; j < inputData.N; j++)                                                   // В цикле по всем ячейкам
                {
                    var concentrationOutput = concentrationInput - StepSumm(j + 1, tau, currentTime)    // Концентрация по формуле 2 из отчета
                        * Math.Exp((-currentTime) / tau) * concentrationInput;
                    resultCollectionOfCellConcentrations[j].ConcentrationInfos.Add(new ConcentrationInfo// Добавляем в соответствующую коллекцию
                    {
                        Time = currentTime + stepTime,                                                  // Время, смещенное на первый шаг
                        Concentration = Math.Round(concentrationOutput, 1)                              // Концентрация - 1 знак после запятой
                    });
                    concentrationInput = concentrationOutput;                                           // Входное значение след. ячейки - выходное из текущей
                }
                concentrationInput = inputData.C_an;                                                    // На след. шаге Входную концентрацию присваиваем снова
                                                                                                        // как начальную
                currentTime += inputData.h;                                                             // Делаем шаг по времени
            }
            return resultCollectionOfCellConcentrations;                                                // Возвращаем результат
        }

        /*
        * Функция StepSumm(int currentCell, double tau, double currentTime) - функция обработки входных данных по математической модели
        * Функция возвращает часть формулы, отвечающей, за ряд от 1 до N (1/(N-1)!)*(-t/tau)^N
        */
        private double StepSumm(int currentCell, double tau, double currentTime)
        {
            double summ = 0;                                                                            // Сумма равна изначально 0                   
            for (int i = 0; i < currentCell; i++)                                                       // В цикле до N считаем ряд
            {
                summ += (1 / (double)Factorial(i)) * Math.Pow((currentTime / tau), i);                  // Прибавляем (1/(N-1)!)*(-t/tau)^N
            }
            return summ;                                                                                // Возвращаем сумму
        }
        /*
        * Функция Factorial(int fact) - функция обработки входных данных по математической модели
        * Возращает факториал введенного числа
        */
        private int Factorial(int fact)
        {
            if (fact > 0)                                                                               // Если больше нуля
                return fact*Factorial(fact - 1);                                                        // Домножаем на вычисленное рекурсивно значение
            return 1;                                                                                   // Если 0, факториал = 1
        }
    }
}

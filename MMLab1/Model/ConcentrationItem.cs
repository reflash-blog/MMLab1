using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMLab1.Model
{
    /// <summary>
    /// Класс ConcentrationItem
    /// 
    /// Отвечает за хранение значений концентрации. Учитывает проверку концентрации на заданные значения диапазона
    /// </summary>
    public class ConcentrationItem
    {
        /// <summary>
        /// Переменная m_C_an - хранит начальное значение концентраций в ячейке
        /// </summary>
        private double m_C_an;

        /// <summary>
        /// Свойство Title - хранит название объекта с заданным значением концентрации
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Свойство Value - хранит значение концентрации заданной ячейки, производит проверку на вхождение в диапазон
        /// </summary>
        [Required]
        [Range(3000, 10000)]
        public double Value
        {
            get { return m_C_an; }                                                          // Получение значения концентрации
            set
            {
                Validator.ValidateProperty(value,                                          // Проверка концентрации
                    new ValidationContext(this, null, null) { MemberName = "Value" });     // на вхождение в заданный диапазон
                m_C_an = value;                                                            // Присваиваем значение концентрации
            }
        }
    }
}

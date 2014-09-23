using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;

namespace MMLab1.Model
{
    /// <summary>
    /// Класс InputData
    /// 
    /// Хранит значения параметров математической модели для моделирования процесса.
    /// </summary>
    public class InputData
    {
        private int m_N;                                                                 // Количество ячеек
        /*DEL@00001
        private List<ConcentrationItem> concentrationItems;                              // Начальные концентрации в каждой ячейке
         */
        private double m_C_an;                                                           // Начальные концентрации ячеек
        private double m_V;                                                              // Объем аппарата
        private double m_G;                                                              // Объемный расход
        private double m_k;                                                              // Коэффициэнт кратности времени возобновления подачи компонента
        private double m_h;                                                              // Шаг дискретизации

        [Required]
        [Range(1, 5)]
        public int N
        {
            get { return m_N; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) {MemberName = "N"});
                m_N = value;
            }
        }

        /*DEL@00001
        [Required]
        public List<ConcentrationItem> ConcentrationItems
        {
            get { return concentrationItems; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) {MemberName = "ConcentrationItems"});
                concentrationItems = value;
            }
        }
         */

        [Required]
        [Range(3000, 10000)]
        public double C_an
        {
            get { return m_C_an; }                                                          // Получение значения концентрации
            set
            {
                Validator.ValidateProperty(value,                                          // Проверка концентрации
                    new ValidationContext(this, null, null) { MemberName = "C_an" });      // на вхождение в заданный диапазон
                m_C_an = value;                                                            // Присваиваем значение концентрации
            }
        }

        [Required]
        [Range(0.05,0.1)]
        public double V
        {
            get { return m_V; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "V" });
                m_V = value;
            }
        }

        [Required]
        [Range(0.1, 0.5)]
        public double G
        {
            get { return m_G; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "G" });
                m_G = value;
            }
        }

        [Required]
        public double k
        {
            get { return m_k; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "k" });
                m_k = value;
            }
        }
        [Required]
        public double h
        {
            get { return m_h; }
            set
            {
                Validator.ValidateProperty(value,
                    new ValidationContext(this, null, null) { MemberName = "h" });
                m_h = value;
            }
        }


    }
}

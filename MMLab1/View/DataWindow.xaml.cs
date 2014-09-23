using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MMLab1.Model;
using MMLab1.View.MessageBox;
using MMLab1.View.Plot;

namespace MMLab1.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        /*
         * Класс DataWindow наследуется от Window
         * 
         * Назначение: Окно отображения состояния данных. Используется для ввода данных пользователем
         * Описание: Принцип работы - взаимодействуя с окном отображения результатов производит вычисления.
         * Переменные: canClose - флаг проверки на необходимость закрытия окна. Выставляется в true, когда закрывается окно VisualWindow,
         * таким образом закрытие будет происходить только при закрытии основного окна.
         * visualWindow - указатель на окно отображения, необходимо для взаимодействия с его объектами и состояниями.
         *          
         * @00001 - Замена ввода начальной концентрации из таблицы, на ввод из текстового поля. Изменение по заданию
         */
        private bool canClose = false;                                                       // Флаг закрытия
        private VisualWindow visualWindow;                                                   // Указатель на окно отображения

        enum TooltipImageType : int                                                          // Перечисление для определения типа необходимой картинки
        {
            CELL=1,DOUBLECELL=2,TRIPLECELL=3,FOURCELLS=4,FIVECELLS=5
        }
        /*
         * Функция DataWindow(VisualWindow window) - основной конструктор.
         * Обязательный параметр - указатель на окно отображения
         * Инициализирует объект окна данных
         */
        public DataWindow(VisualWindow window)
        {
            InitializeComponent();                                                        // Инициализируем компоненты окна
            visualWindow = window;                                                        // Сохраняем указатель на окно отображения
            ResizeMode = ResizeMode.NoResize;                                             // Выставляем запрет на изменение размеров
                                                                                          // Изменение размеров остается включенным, однако
                                                                                          // теряется возможность использовать кнопку Maximize
            DataContext = new DataWindowViewModel();
        }

        /*
        * Функция DataWindow_OnClosing(object sender, CancelEventArgs e) - обработчик события закрытия окна.
        * Замещает закрытие скрыванием окна, в случае, если закрытие вызвано нажатием клавиши закрыть окна данных.
        */
        private void DataWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!canClose)                                                            // Если нельзя закрыть
            {
                e.Cancel = true;                                                      // Отмена закрытия
                this.Visibility = Visibility.Hidden;                                  // Скрытие окна
                visualWindow.ShowDataButton.Content = "»";                            // Установка значка кнопки на возможность открыть
            }
        }

        /*
        * Функция AdjustToVisualWindow() - функция закрепления окна данных.
        * Закрепляет окно данных к окну отображения. Закрепление происходит справа от окна отображения.
        */
        internal void AdjustToVisualWindow()                        
        {
            this.Left = visualWindow.Left + visualWindow.Width                        // Левая координата окна данных - правая координата окна отображения
                - VisualWindow.ShadowBorderWidth;                                     //
            this.Top = visualWindow.Top;                                              // Верхняя координата окна данных - верхняя координата окна отображения
        }

        /*
        * Функция Quit() - функция закрытия окна данных.
        * Используется для закрытия окна данных из окна отображения. При вызове выставляет флаг возможности закрытия окна данных
        * в значение true.
        */

        public void Quit()
        {
            canClose = true;                                                          // Выставление флага возможности закрытия в true
            Close();                                                                  // Закрыть окно
        }
        /*
        * Функция DataWindow_OnStateChanged(object sender, EventArgs e) - обработчик изменения состояния
        * Используется для обработки событий сворачивания и разворачивания окна. Функция необходима для изменения отображения
        * знака на кнопке, показывающей пользователю, можно ли закрыть/открыть окно данных
        */
        private void DataWindow_OnStateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)                                                // Просматриваем состояние окна
            {
                case WindowState.Minimized:                                          // Если свернуто
                    visualWindow.ShowDataButton.Content = "»";                       // Выставляем значок в состояние - можно открыть
                    break;
                case WindowState.Normal:                                             // Если развернуто
                    visualWindow.ShowDataButton.Content = "«";                       // Выставляем значок в состояние - можно скрыть
                    break;
            }
        }
        /*
        * Функция DataWindow_OnActivated(object sender, EventArgs e) - обработчик фокусировки
        * Используется для обработки событий разворачивания окна после Alt+TAB. Разворачивает основное окно, помимо окна данных
        */
        private void DataWindow_OnActivated(object sender, EventArgs e)
        {
            visualWindow.WindowState = WindowState.Normal;                          // Разворачиваем основное окно
            AdjustToVisualWindow();                                                 // Выравниваем окно
            visualWindow.Focus();                                                   // Фокусируемся на нем
        }

        /*
        * Функция GetInputData() - получение данных из ввода пользователя
        * Возвращает InputData объект
        * Функция считывает данные, введенные пользователем и формирует из них объект
        */
        public InputData GetInputData()
        {
            InputData inputData = null;                                            // Объект входных данных
            try
            {
                /*DEL@00001
                var concentrationList = (from object concentrationItem in          // Создаем список из объектов 
                                             InitialConcentrationDataGrid.Items    // из значений таблицы начальных концентраций
                                         select concentrationItem as               // Приняв их как объекты типа ConcentrationItem
                                         ConcentrationItem).ToList();              // Приводим к списку
                 */
                inputData = new InputData                                          // Инициализируем объект входных данных
                {
                    N = Convert.ToInt32(NumberOfCellsTextBox.Text),                // Получаем количество ячеек
                    /*DEL@00001
                    ConcentrationItems = concentrationList,                        // Присваиваем список начальных концентраций
                     */
                    C_an = Convert.ToDouble(InitialConcentrationTextBox.Text),     // Получаем начальную концентрацию ячеек @00001
                    V = Convert.ToDouble(UnitVolumeTextBox.Text),                  // Получаем объем аппарата
                    G = Convert.ToDouble(VolumeFlowRateTextBox.Text),              // Объемный расход
                    k = Convert.ToDouble(KTextBox.Text),                           // Коэффициэнт кратности времени возобновления подачи компонента
                    h = Convert.ToDouble(DiscretizationTextBox.Text)               // Шаг дискретизации
                    
                };
            }
            catch (FormatException)                                                // Отлавливаем ошибку приведения типов
            {
                CustomMessageBox.Show(                                             // Выводим сообщение об ошибке
                    this,
                    "Ошибка ввода данных. Вы ввели не числовое значение",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (ValidationException)                                            // Отлавливаем ошибку валидации параметров
            {
                CustomMessageBox.Show(                                             // Выводим сообщение об ошибке
                    this,
                    "Ошибка ввода данных. Вы ввели значение," +
                    " выходящее за заданные ограничения\nДля " +
                    "получения более подробной информации об " +
                    "ограничениях, воспользуйтесь всплывающими " +
                    "\nподсказками у вводимых значений",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            return inputData;                                                     // Возвращаем полученный объект
        }
        /*
        * Функция SetInputData(InputData inputData) - установка данных в текстовые поля
        * Принимает InputData объект
        * Функция считывает данные из объекта и выводит их в текстовые поля интерфейса
        */
        public void SetInputData(InputData inputData)
        {
            if(inputData==null) return;                                          // Если NULL, то выходим из функции            
            NumberOfCellsTextBox.Text = inputData.N.ToString();                  // Устанавливаем количество ячеек
            /*DEL@00001
            InitialConcentrationDataGrid.ItemsSource =                           // Устанавливаем список начальных концентраций
                inputData.ConcentrationItems;
             */
            InitialConcentrationTextBox.Text = inputData.C_an.ToString();        // Устанавливаем начальную концентрацию @00001
            UnitVolumeTextBox.Text = inputData.V.ToString();                     // Устанавливаем объем аппарата
            VolumeFlowRateTextBox.Text = inputData.G.ToString();                 // Объемный расход
            KTextBox.Text = inputData.k.ToString();                              // Коэффициэнт кратности времени возобновления подачи компонента
            DiscretizationTextBox.Text = inputData.h.ToString();                 // Шаг дискретизации
        }

        /*DEL@00001
        * Функция InitialConcentrationTableButton_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия клавиши "Открыть таблицу"
        * Функция устанавливаем видимость таблицы в зависимости от ее текущего состояния, при нажатии клавиши "Открыть таблицу"
        */
        /*DEL@00001
        private void InitialConcentrationTableButton_OnClick(object sender, RoutedEventArgs e)
        {
            InitialConcentrationDataGrid.Visibility =                            // Присвоить свойству видимости
                InitialConcentrationDataGrid.Visibility == Visibility.Visible    // Если таблица видима, то
                ? Visibility.Hidden :                                            // Сделать невидимым, иначе
                Visibility.Visible;                                              // Сделать видимым
        }
         */

        /*
        * Функция NumberOfCellsTextBox_OnTextChanged(object sender, TextChangedEventArgs e) - обработчик события изменения текстового поля количества ячеек
        * Функция изменяет параметры таблицы InitialConcentrationDataGrid и изображения в tooltip к Labelу количества ячеек
        */
        private void NumberOfCellsTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var datacontext = DataContext as DataWindowViewModel;               // Контекст данных - DataWindowViewModel                                   
            if(datacontext==null) return;                                       // Если тип контекста отличается, выходим из функции
            ObservableCollection<ConcentrationItem> collection =                // Создаем коллекцию концентраций
                new ObservableCollection<ConcentrationItem>();                  // И инициализируем
            try
            {
                var inputData = new InputData                                   // Пробуем создать объект
                {
                    N = Convert.ToInt32(NumberOfCellsTextBox.Text)              // Таким образом проверяем входимость введенных данных в ограничения
                };


                for (int i = 0; i < inputData.N; i++)                           // В цикле по количеству ячеек
                {
                    collection.Add(new ConcentrationItem                        // Добавляем в коллекцию объекты
                    {
                        Title = "Ячейка " + (i + 1), Value = 3550               // С именами ячеек по номерам и значением по умолчанию
                    });
                }
                switch ((TooltipImageType)inputData.N)                          // Выбираем по количеству ячеек соответствующее значение перечисления
                {
                    case TooltipImageType.CELL:                                 // 1 ячейка
                        datacontext.SetImageData(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/../..//View//images//cell1.png"));
                        break;                                                  // Изображение одной ячейки
                    case TooltipImageType.DOUBLECELL:                           // 2 ячейки
                        datacontext.SetImageData(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/../..//View//images//cell2.png"));
                        break;                                                  // Изображение двух ячеек
                    case TooltipImageType.TRIPLECELL:                           // 3 ячейки
                        datacontext.SetImageData(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/../..//View//images//cell3.png"));
                        break;                                                  // Изображение трех ячеек
                    case TooltipImageType.FOURCELLS:                            // 4 ячейки
                        datacontext.SetImageData(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/../..//View//images//cell4.png"));
                        break;                                                  // Изображение четырех ячеек
                    case TooltipImageType.FIVECELLS:                            // 5 ячеек
                        datacontext.SetImageData(File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/../..//View//images//cell5.png"));
                        break;                                                  // Изображение пяти ячеек
                    default:                                                    // Если ни один из вариантов
                        break;
                }
                
            }
            catch (FormatException exception) { }                               // Отлавливаем исключение приведения типов
            catch (ValidationException)                                         // Отлавливаем исключение валидации
            {                                       
                TooltipImage.Source = null;                                     // Убираем изображение 
            }                                                                   
            //DEL@00001 InitialConcentrationDataGrid.ItemsSource = collection;  // Источник значений для таблицы начальных концентраций
        }


    }
}

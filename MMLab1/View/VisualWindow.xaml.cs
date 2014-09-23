using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using MMLab1.Controller;
using MMLab1.View.MessageBox;
using MMLab1.View.Plot;

namespace MMLab1.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class VisualWindow
    {

        /*
        * Класс VisualWindow наследуется от Window
        * 
        * Назначение: Окно отображения результатов и промежуточных значений. Используется для вывода данных пользователю
        * Описание: Принцип работы - взаимодействуя с окном отображения данных производит вычисления.
        * Переменные: shadowBorderWidth - константа, ширина границы тени окна, используется для прикрепления окна данных
        * dataWindow - указатель на окно данных, необходимо для взаимодействия с его объектами и состояниями.
        */
        private readonly DataWindow _dataWindow;                                                     // Указатель на окно данных
        public const int ShadowBorderWidth = 15;                                                     // Ширина границы тени

        /*
        * Функция VisualWindow() - основной конструктор.
        * Инициализирует объект окна отображения
        */
        public VisualWindow()
        {
            Visibility = Visibility.Hidden;                                                          // Скрываем окно до его полной отрисовки
            _dataWindow = new DataWindow(this) { Left = Left + Width                                 // Создаем окно данных
                - ShadowBorderWidth, Top = Top };                                                    // С параметрами для прикрепления
            _dataWindow.Show();                                                                      // Активируем отображение окна данных
            _dataWindow.Visibility = Visibility.Hidden;
            InitializeComponent();                                                                   // Инициализируем объекты окна отображения
            Visibility = Visibility.Visible;                                                         // Включаем отображения основного окна
            ResizeMode = ResizeMode.NoResize;                                                        // Выставляем запрет на изменение размеров
                                                                                                     // Изменение размеров остается включенным, однако
                                                                                                     // теряется возможность использовать кнопку Maximize
            SizeChanged += VisualWindow_OnLocationChanged;                                           // Выставляем обработчик по изменению размера
            LocationChanged += VisualWindow_OnLocationChanged;                                       // Выставляем обработчик по изменению положения
                                                                                                     // Оба обработчика нужны для отслеживания необходимости
                                                                                                     // прикрепления окна данных
            Focus();                                                                                 // Фокусируемся на окне отображения
            DataContext = new MainWindowViewModel();
            _dataWindow.Visibility = Visibility.Visible;                                             // Включаем отображение окна данных
            UpdateVisualDataGrid();
        }
        /*
        * Функция VisualWindow_OnClosing(object sender, CancelEventArgs e) - обработчик события по закрыванию окна.
        * Вызываем программное закрытие окна данных
        */
        private void VisualWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _dataWindow.Quit();                                                                     // Закрываем окно данных
        }
        /*
        * Функция ShowDataButton_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия кнопки открытия окна данных.
        * Изменяем состояние окна данных и состояние кнопки (изменяем значок на кнопке, для улучшения понимания пользователем)
        */
        private void ShowDataButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_dataWindow.Visibility == Visibility.Visible && _dataWindow.WindowState == WindowState.Normal)      // Если Видно и в состоянии Normal
            {
                _dataWindow.Visibility = Visibility.Hidden;                                                         // Скрываем окно
                ShowDataButton.Content = "»";                                                                       // Показываем возможность открыть окно данных значком
                return;                                                                                             // Выходим из функции
            }
            if (_dataWindow.Visibility == Visibility.Hidden || _dataWindow.WindowState == WindowState.Minimized)    // Если Скрыт или в свернутом состоянии
            {
                _dataWindow.Visibility = Visibility.Visible;                                                        // Делаем видимым
                _dataWindow.WindowState = WindowState.Normal;                                                       // Разворачиваем до нормального состояния
                ShowDataButton.Content = "«";                                                                       // Показываем возможность закрыть окно данных значком
                Focus();                                                                                            // Фокусируемся на окне отображения
            }
            
            
        }
        /*
        * Функция UpdateVisualDataGrid() - обновляем таблицу окна отображения, хранящую результирующие данные
        */
        private void UpdateVisualDataGrid()
        {
            var datacontext = DataContext as MainWindowViewModel;                           // Контекст данных вида MainWindowViewModel
            if (datacontext == null)                                                        // Если не таковой, то выходим из функции
                return;
            var collectionOfCellsData = datacontext.ConcentrationData;                      // В качестве коллекции для получения результатов, используем
                                                                                            // данные из ConcentrationData, объекта контекста данных
            var dataTable = new DataTable();                                                // Создаем объект таблицы
            var timeColumn = new DataColumn {ColumnName = "Время, мин"};                    // Создаем колонку времени
            dataTable.Columns.Add(timeColumn);                                              // Добавляем колонку времени
            foreach (var concentrationCell in collectionOfCellsData)                        // Для всех ячеек из коллекции
            {
                var concentrationColumn = new DataColumn                                    // Создаем новую колонку
                {
                    ColumnName = concentrationCell.ConcentrationName                        // Присваиваем имя
                };
                dataTable.Columns.Add(concentrationColumn);                                 // Добавляем в таблицу
            }
            if (collectionOfCellsData.Count > 0)                                            // Если ячеек больше 0
            {
                for (int lineNumber = 0; lineNumber <                                       // В цикле по линиям
                    collectionOfCellsData[0].ConcentrationInfos.Count; lineNumber++)        // Вплоть до количества значений в 1ой ячейке
                {
                    DataRow newRow = dataTable.NewRow();                                    // Создаем новую строку
                    for (int column = 0; column < collectionOfCellsData.Count; column++)    // В цикле по колонкам до количества ячеек
                    {
                        if (column == 0)                                                    // Если первая итерация, то
                            newRow[column] = collectionOfCellsData[column].                 // В колонку новой строки добавляем
                                ConcentrationInfos[lineNumber].Time;                        // значение времени
                        newRow[column + 1] = collectionOfCellsData[column].                 // В следующую колонку добавляем
                            ConcentrationInfos[lineNumber].Concentration;                   // значение концентрации
                    }
                    dataTable.Rows.Add(newRow);                                             // Добавляем новую строку в таблицу
                }
            }
            VisualDataGrid.ItemsSource = dataTable.DefaultView;                             // Таблице отображения в качестве источника предоставляем
                                                                                            // созданную и заполненную таблицу
        }

        /*
        * Функция VisualWindow_OnLocationChanged(object sender, EventArgs e) - обработчик события изменения положения окна
        * Вызываем прилипание окна данных к окну отображения
        */
        private void VisualWindow_OnLocationChanged(object sender, EventArgs e)
        {
            _dataWindow.AdjustToVisualWindow();                                              // Закрепляем окно
        }
        /*
        * Функция VisualWindow_OnStateChanged(object sender, EventArgs e) - обработчик события изменения состояния окна
        * Обрабатываем изменение состояний окна отображает и задаем поведение для окна данных
        */
        private void VisualWindow_OnStateChanged(object sender, EventArgs e)
        {
            switch (WindowState)                                                           // Состояние окна
            {
                case WindowState.Minimized:                                                // Если свернуто
                    if(_dataWindow.Visibility == Visibility.Visible)                       // Если окно данных не скрыто
                        _dataWindow.WindowState = WindowState.Minimized;                   // Сворачиваем окно данных
                    break;
                case WindowState.Normal:                                                   // Если не свернуто
                    if (_dataWindow.Visibility == Visibility.Visible)                      // Если окно данных не скрыто
                        _dataWindow.WindowState = WindowState.Normal;                      // Возвращаем окно данных в обычное состояние
                    break;
            }
        }

        /*
        * Функция VisualWindow_OnActivated(object sender, EventArgs e) - обработчик события вызова окна
        * Обрабатываем ситуацию, когда окно было вновь вызвано при помощи клавиш alt+tab. В таком случае окно данных тоже разворачивается
        */
        private void VisualWindow_OnActivated(object sender, EventArgs e)
        {
            _dataWindow.Focus();                                                           // Фокусируемся на окне данных
        }

        /*
        * Функция SaveMenuItem_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия кнопки меню сохранить
        */
        private async void SaveMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            await FileSystemInteraction.SaveFile(_dataWindow.GetInputData());              // Вызываем сохранения файла
        }
        /*
        * Функция OpenMenuItem_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия кнопки меню открыть
        */
        private async void OpenMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var inputData = FileSystemInteraction.OpenFile();                              // Получаем объект через открытие файла
            _dataWindow.SetInputData(await inputData);                                     // Записываем его в текстовые поля интерфейса
        }
        /*
        * Функция CalculateMenuItem_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия кнопки меню вычислить
        */

        private void CalculateMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            VisualWindowStatusBarTextBlock.Text = "Производится расчет...";                // Изменяем статус бар, для оповещения пользователя о вычислении
            Stopwatch stopWatch = new Stopwatch();                                         // Объект таймера
            stopWatch.Start();                                                             // Запускаем таймер


            var dataProcessController = new DataProcessController();                       // Создаем контроллер обработки данных
            var resultCollection = dataProcessController.ProcessData(
                _dataWindow.GetInputData());                                               // Запускаем обработку данных
            if(resultCollection==null) return;                                             // Если результат не получен - выходим
            var datacontext = DataContext as MainWindowViewModel;                          // Контекст данных - MainWindowViewModel

            stopWatch.Stop();                                                              // Останавливаем таймер
            TimeSpan ts = stopWatch.Elapsed;                                               // Получаем прошедшее время
            if (datacontext != null)                                                       // Если контекст не отличен от MainWindowViewModel
                datacontext.UpdateData(resultCollection);                                  // Обновляем данные в коллекциях
            UpdateVisualDataGrid();                                                        // Обновляем таблицу данных
            //  Format and display the TimeSpan value.
            //  string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            var millisecondsElapsed = ts.Milliseconds + ts.Seconds*1000 +                  // Записываем прошедшее время в миллисекундах
                                      ts.Minutes*60*1000
                                      + ts.Hours*60*60*1000;
            VisualWindowStatusBarTextBlock.Text = "Готов. Время расчета : " +              // Выводим сообщение в статус бар
                                                  millisecondsElapsed + " мс";
        }

        /*
        * Функция SaveAsMenuItem_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия кнопки меню сохранить как
        */
        private async void SaveAsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            await FileSystemInteraction.SaveAsFile(_dataWindow.GetInputData());            // Вызываем сохранения файла
        }
        /*
        * Функция QuitMenuItem_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия кнопки меню закрыть
        */
        private void QuitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();                                                                        // Выходим из приложения
        }
        /*
        * Функция HelpMenuItem_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия кнопки меню помощь
        */
        private void HelpMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(@"mm.chm");                                                      // Запускаем файл инструкции
        }
        /*
        * Функция AboutMenuItem_OnClick(object sender, RoutedEventArgs e) - обработчик события нажатия кнопки меню о программе
        */
        private void AboutMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.Show(                                                         // Открываем окно с информацией о программе
                this,
                "О программе\n" +
                            "Программа выполнения расчета работы математической модели" +
                            " ячеечного типа.",
                "Информация",
                MessageBoxButton.OK,
                MessageBoxImage.Information)
            ;
        }
    }
}

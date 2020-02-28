namespace PostfixCalculator.Domain
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Интерфейс для обработчиков подключений плагинов
    /// </summary>
    public interface IPluginReader
    {
        /// <summary>
        /// Считывает плагины операций из dll файлов в указанной директории
        /// </summary>
        /// <param name="directory">директория плагинов относительно корневого каталога программы</param>
        /// <returns>список операций</returns>
        List<IOperation> ReadPluginsFrom(string directory);
    }
}
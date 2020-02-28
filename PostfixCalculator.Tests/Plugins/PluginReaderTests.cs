namespace CalculatorSolution.Test.Plugins
{
    using System.IO;
    using NUnit.Framework;
    using PostfixCalculator.Application;
    using PostfixCalculator.Domain;

    [TestFixture]
    public class PluginReaderTests
    {
        private readonly IPluginReader pluginReader;
        private readonly string pluginPath;
        private readonly string pluginPathIncorrect;
        public PluginReaderTests()
        {
            pluginReader = new OperationPluginReader();
            pluginPath = System.Environment.CurrentDirectory + "\\" + "Plugins";
            pluginPathIncorrect = System.Environment.CurrentDirectory + "\\" + "Plugins4545";
        }

        [Test]
        public void PluginReader_FindsAllOperationsInDLLFiles()
        {
            var result = pluginReader.ReadPluginsFrom(pluginPath);
            Assert.AreEqual(6,result.Count);
        }

        [Test]
        public void PluginReader_ThrowsDirectoryNotFoundException()
        {
            Assert.Throws<DirectoryNotFoundException>(
                                    () => pluginReader.ReadPluginsFrom(pluginPathIncorrect));
        }
    }
}
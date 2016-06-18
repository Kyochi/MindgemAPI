using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MindgemAPI.utils.Tests
{
    [TestClass]
    public class FileConfigReaderTests
    {
        private FileConfigReader fileReader;
        
        
        public void loadDataObjectsConfigTest()
        {
            String path = @"../../../dataObjects.cfg";
            var p = File.Create(path);
            p.Close();
            File.WriteAllText(path, @"krak = http://www.hgoog.com?d= " );
            File.AppendAllText(path, System.Environment.NewLine + @" test= http://www.blabla.glre/&liveChannel/118");

            Dictionary<string, string> mapingExpected = new Dictionary<string, string>();
            mapingExpected.Add("krak", @"http://www.hgoog.com?d=");
            mapingExpected.Add("test", @"http://www.blabla.glre/&liveChannel/118");
            
            // Flemme d'override toString ou equals, on fait à la mano
            String valExpected;
            String valReturn;
            this.fileReader = new FileConfigReader();
            Dictionary<string, string> mapLoad = fileReader.loadDataObjectsConfig(path);
            if (!mapingExpected.TryGetValue("krak", out valExpected)) return;
            if (!mapLoad.TryGetValue("krak", out valReturn)) return;
            Assert.AreEqual(valExpected, valReturn);

            if (!mapingExpected.TryGetValue("test", out valExpected)) return;
            if (!mapLoad.TryGetValue("krak", out valReturn)) return;
            Assert.AreEqual(valExpected, valReturn);

            // Attention ne marche pas, penser à supprimer le fichier avant de commit
            File.Delete(path);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException), @"Le fichier 'C:\ok.com' est introuvable.")]
        public void loadDataObjectConfigRaiseTestException()
        {
            this.fileReader = new FileConfigReader();
            Dictionary<string, string> mapLoad = fileReader.loadDataObjectsConfig(@"C:\ok.com");
        }
    }
}
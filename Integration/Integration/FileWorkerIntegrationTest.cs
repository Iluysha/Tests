using Xunit;
using IIG.FileWorker;
using IIG.BinaryFlag;

namespace Integration
{

    public class FileWorkerIntegrationTest
    {

        [Fact]
        public void createFileTest()
        {
            Assert.False(BaseFileWorker.Write("text", ""));
            Assert.False(BaseFileWorker.Write("text", "\test"));
            Assert.False(BaseFileWorker.Write("text", "*test"));
            Assert.False(BaseFileWorker.Write("text", "/test"));
            Assert.False(BaseFileWorker.Write("text", "//test"));
            Assert.False(BaseFileWorker.Write("text", "/../test"));
            Assert.False(BaseFileWorker.Write("text", ".../test"));
            Assert.False(BaseFileWorker.Write("text", "Test/test"));
            Assert.True(BaseFileWorker.Write("text", "../../../test1"));
            Assert.True(BaseFileWorker.Write("text", "../../.././test2"));
            Assert.True(BaseFileWorker.Write("text", "../../../test.txt"));
            Assert.Equal("text", BaseFileWorker.ReadAll("../../../test.txt"));
        }

        [Fact]
        public void createDirTest()
        {
            Assert.Equal("C:\\Users\\Ilya\\Desktop\\Labs\\Tests\\Tests\\Integration\\Integration\\Test", 
                BaseFileWorker.MkDir("../../../Test"));
            Assert.Equal("C:\\Users\\Ilya\\Desktop\\Labs\\Tests\\Tests\\Integration\\Integration\\",
                BaseFileWorker.MkDir("../../../"));
            Assert.False(BaseFileWorker.Write("text", "../../..//test"));
        }

        [Fact]
        public void emptyTest()
        {
            Assert.True(BaseFileWorker.Write("T", "../../../test1.txt"));
            Assert.True(BaseFileWorker.Write("", "../../../test1.txt"));
            Assert.Equal("", BaseFileWorker.ReadAll("../../../test2.txt"));
        }

        [Fact]
        public void doubleWriteTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            BaseFileWorker.Write(flag.ToString(), "../../../test2.txt");
            BaseFileWorker.Write(flag.ToString(), "../../../test2.txt");
            Assert.Equal(flag.ToString(), BaseFileWorker.ReadAll("../../../test2.txt"));
        }

        [Fact]
        public void lengthTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            BaseFileWorker.Write(flag.ToString(), "../../../test3.txt");
            Assert.Equal(flag.ToString(),
                BaseFileWorker.ReadAll("../../../test3.txt"));
        }

        [Fact]
        public void lengthMaxPossibleTest()
        {
            ulong length = 17179868704;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            BaseFileWorker.Write(flag.ToString(), "../../../test4.txt");
            Assert.Equal(flag.ToString(),
                BaseFileWorker.ReadAll("../../../test4.txt"));
        }

        [Fact]
        public void setTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length, false);
            flag.SetFlag(7);
            flag.SetFlag(0);
            BaseFileWorker.Write(flag.ToString(), "../../../test5.txt");
            Assert.Equal(flag.ToString(), BaseFileWorker.ReadAll("../../../test5.txt"));
        }

        [Fact]
        public void resetTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            flag.ResetFlag(7);
            flag.ResetFlag(0);
            BaseFileWorker.Write(flag.ToString(), "../../../test6.txt");
            Assert.Equal(flag.ToString(), BaseFileWorker.ReadAll("../../../test6.txt"));
        }
    }
}

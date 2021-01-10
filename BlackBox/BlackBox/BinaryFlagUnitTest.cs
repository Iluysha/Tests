using System;
using Xunit;
using IIG.BinaryFlag;

namespace BlackBox
{
    public class BinaryFlagUnitTest
    {

        [Fact]
        public void lengthTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            Assert.Equal((int)length, flag.ToString().Length);
        }

        [Fact]
        public void lengthMinTest()
        {
            ulong length = ulong.MinValue;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            Assert.Equal((int)length, flag.ToString().Length);
        }

        [Fact]
        public void length1Test()
        {
            ulong length = 1;
            MultipleBinaryFlag flag;
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                flag = new MultipleBinaryFlag(length));
        }

        [Fact]
        public void length2Test()
        {
            ulong length = 2;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            Assert.Equal((int)length, flag.ToString().Length);
        }

        [Fact]
        public void lengthMaxTest()
        {
            ulong length = ulong.MaxValue;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            Assert.Equal(true, flag.GetFlag());
        }

        [Fact]
        public void lengthMaxPossibleTest()
        {
            ulong length = 17179868704;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            Assert.Equal(true, flag.GetFlag());
        }

        [Fact]
        public void lengthMaxPossiblePlus1Test()
        {
            ulong length = 17179868705;
            MultipleBinaryFlag flag;
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                flag = new MultipleBinaryFlag(length));
        }

        [Fact]
        public void lengthMaxPossibleMinus1Test()
        {
            ulong length = 17179868703;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            Assert.Equal(true, flag.GetFlag());
        }

        [Fact]
        public void defaultStateTest()
        {
            ulong length = 100;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            Assert.Equal(true, flag.GetFlag());
        }

        [Fact]
        public void trueStateTest()
        {
            ulong length = 100;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length, true);
            Assert.Equal(true, flag.GetFlag());
        }

        [Fact]
        public void falseStateTest()
        {
            ulong length = 100;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length, false);
            Assert.Equal(false, flag.GetFlag());
        }

        [Fact]
        public void setTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length, false);
            flag.SetFlag(7);
            Assert.Equal("FFFFFFFTFF", flag.ToString());
        }

        [Fact]
        public void set0Test()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length, false);
            flag.SetFlag(0);
            Assert.Equal("TFFFFFFFFF", flag.ToString());
        }

        [Fact]
        public void setOutOfRangeTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length, false);
            Assert.Throws<ArgumentOutOfRangeException>(() => flag.SetFlag(100));
        }

        [Fact]
        public void resetTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            flag.ResetFlag(7);
            Assert.Equal("TTTTTTTFTT", flag.ToString());
        }

        [Fact]
        public void reset0Test()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            flag.ResetFlag(0);
            Assert.Equal("FTTTTTTTTT", flag.ToString());
        }

        [Fact]
        public void resetOutOfRangeTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            Assert.Throws<ArgumentOutOfRangeException>(() => flag.ResetFlag(100));
        }

        [Fact]
        public void disposeTest()
        {
            ulong length = 100;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            flag.Dispose();
            Assert.Throws<ArgumentNullException>(() => flag.GetFlag());
        }

        [Fact]
        public void disposeSmallTest()
        {
            ulong length = 10;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            flag.Dispose();
            Assert.NotEqual("TTTTTTTTTT", flag.ToString());
        }

        [Fact]
        public void disposeMultipleTest()
        {
            ulong length = 100;
            MultipleBinaryFlag flag = new MultipleBinaryFlag(length);
            flag.Dispose();
            flag.Dispose();
            Assert.Throws<ArgumentNullException>(() => flag.GetFlag());
        }
    }
}
